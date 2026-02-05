using System.Buffers;
using System.IO.Hashing;
using System.Net;
using System.Security.Cryptography;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;
using static SteamKit.Client.Internal.Model.DisconnectedEventArgs;

namespace SteamKit.Client.Internal.Connection
{
    internal class EncryptedConnection : IConnection
    {
        private readonly IConnection _inner;
        private INetFilterEncryption? encryption;
        private EncryptionState encryptionState = EncryptionState.Disconnected;

        public EncryptedConnection(IConnection inner)
        {
            _inner = inner;

            _inner.Connected += OnConnected;
            _inner.Disconnected += OnDisconnected;
            _inner.MsgReceived += OnNetMsgReceived;
        }

        /// <summary>
        /// CurrentEndPoint
        /// </summary>
        public EndPoint CurrentEndPoint => _inner.CurrentEndPoint;

        /// <summary>
        /// Connected
        /// </summary>
        public event EventHandler<ConnectedEventArgs>? Connected;

        /// <summary>
        /// Disconnected
        /// </summary>
        public event EventHandler<DisconnectedEventArgs>? Disconnected;

        /// <summary>
        /// MsgReceived
        /// </summary>
        public event EventHandler<MsgEventArgs>? MsgReceived;

        /// <summary>
        /// ProtocolTypes
        /// </summary>
        public ProtocolTypes Protocol => _inner.Protocol;

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        public Task ConnectAsync(CancellationToken cancellationToken = default) => _inner.ConnectAsync(cancellationToken);

        /// <summary>
        /// Disconnect
        /// </summary>
        /// <param name="disconnectType"></param>
        /// <param name="cancellationToken"></param>
        public Task DisconnectAsync(DisconnectType disconnectType, CancellationToken cancellationToken = default) => _inner.DisconnectAsync(disconnectType, cancellationToken);

        /// <summary>
        /// SendAsync
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        public async Task SendAsync(Memory<byte> data, CancellationToken cancellationToken = default)
        {
            if (encryptionState != EncryptionState.Encrypted)
            {
                await _inner.SendAsync(data, cancellationToken).ConfigureAwait(false);
                return;
            }

            var buffer = ArrayPool<byte>.Shared.Rent(encryption!.CalculateMaxEncryptedDataLength(data.Length));

            try
            {
                var length = encryption!.ProcessOutgoing(data.Span, buffer);
                var serialized = new Memory<byte>(buffer, 0, length);

                await _inner.SendAsync(serialized, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <param name="data"></param>
        public void Send(Memory<byte> data)
        {
            if (encryptionState != EncryptionState.Encrypted)
            {
                _inner.Send(data);
                return;
            }

            var buffer = ArrayPool<byte>.Shared.Rent(encryption!.CalculateMaxEncryptedDataLength(data.Length));

            try
            {
                var length = encryption!.ProcessOutgoing(data.Span, buffer);
                var serialized = new Memory<byte>(buffer, 0, length);

                _inner.Send(serialized);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// GetLocalIP
        /// </summary>
        /// <returns></returns>
        public IPAddress? GetLocalIP() => _inner.GetLocalIP();

        /// <summary>
        /// IsConnected
        /// </summary>
        public bool IsConnected => _inner.IsConnected;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _inner.Dispose();
        }

        private void OnConnected(object? sender, ConnectedEventArgs e)
        {
            encryptionState = EncryptionState.Connected;
        }

        private void OnDisconnected(object? sender, DisconnectedEventArgs e)
        {
            encryptionState = EncryptionState.Disconnected;
            encryption = null;

            Disconnected?.Invoke(this, e);
        }

        private void OnNetMsgReceived(object? sender, MsgEventArgs e)
        {
            IServerMsg? packetMsg;
            if (encryptionState == EncryptionState.Encrypted)
            {
                var plaintextData = encryption!.ProcessIncoming(e.Data);
                MsgReceived?.Invoke(sender, e.WithData(plaintextData));
                return;
            }

            packetMsg = MsgConvert.DeserializeServerMsg(e.Data);
            if (packetMsg == null)
            {
                DisconnectAsync(DisconnectType.ConnectionError);
                return;
            }

            if (!IsExpectedEMsg(packetMsg.MsgType))
            {
                return;
            }

            switch (packetMsg.MsgType)
            {
                case EMsg.ChannelEncryptRequest:
                    HandleEncryptRequest(packetMsg);
                    break;

                case EMsg.ChannelEncryptResult:
                    HandleEncryptResult(packetMsg);
                    break;
            }
        }

        private void HandleEncryptRequest(IServerMsg packetMsg)
        {
            var request = new ServerChannelMsg<MsgChannelEncryptRequest>(packetMsg);

            var connectedUniverse = request.Body.Universe;

            var randomChallenge = request.Payload.ToArray();

            var publicKey = KeyDictionary.GetPublicKey(connectedUniverse);
            if (publicKey == null)
            {
                DisconnectAsync(DisconnectType.ConnectionError);
                return;
            }

            var response = new ClientChannelMsg<MsgChannelEncryptResponse>(EMsg.ChannelEncryptResponse);
            var tempSessionKey = RandomNumberGenerator.GetBytes(32);
            byte[] encryptedHandshakeBlob;

            using (var rsa = RSA.Create())
            {
                rsa.ImportSubjectPublicKeyInfo(publicKey, out _);

                var blobToEncrypt = new byte[tempSessionKey.Length + randomChallenge.Length];
                Array.Copy(tempSessionKey, blobToEncrypt, tempSessionKey.Length);
                Array.Copy(randomChallenge, 0, blobToEncrypt, tempSessionKey.Length, randomChallenge.Length);

                encryptedHandshakeBlob = rsa.Encrypt(blobToEncrypt, RSAEncryptionPadding.OaepSHA1);
            }

            var keyCrc = Crc32.Hash(encryptedHandshakeBlob);
            response.Write(encryptedHandshakeBlob);
            response.Write(keyCrc);
            response.Write(0);

            encryption = new NetFilterEncryptionWithHMAC(tempSessionKey);
            encryptionState = EncryptionState.Challenged;

            SendAsync(response.Serialize()).GetAwaiter().GetResult();
        }

        private void HandleEncryptResult(IServerMsg packetMsg)
        {
            var result = new ServerChannelMsg<MsgChannelEncryptResult>(packetMsg);
            if (result.Body.Result != EResult.OK || encryption == null)
            {
                DisconnectAsync(DisconnectType.ConnectionError);
                return;
            }

            encryptionState = EncryptionState.Encrypted;

            Connected?.Invoke(this, new ConnectedEventArgs());
        }

        private bool IsExpectedEMsg(EMsg msg)
        {
            return encryptionState switch
            {
                EncryptionState.Disconnected => false,
                EncryptionState.Connected => msg == EMsg.ChannelEncryptRequest,
                EncryptionState.Challenged => msg == EMsg.ChannelEncryptResult,
                EncryptionState.Encrypted => true,
                _ => throw new InvalidOperationException("Unreachable - landed up in undefined state."),
            };
        }

        private enum EncryptionState
        {
            Disconnected,
            Connected,
            Challenged,
            Encrypted
        }
    }
}
