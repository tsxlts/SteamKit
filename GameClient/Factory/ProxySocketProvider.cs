using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using SteamKit.Factory;

namespace GameClient.Factory
{
    internal class ProxySocketProvider : ISocketProvider
    {
        public Socket GetSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, int timeout)
        {
            return new Socket(addressFamily, socketType, protocolType)
            {
                SendTimeout = timeout,
                ReceiveTimeout = timeout
            };

            return new Socks5SocketClient(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888), null, null, addressFamily, socketType, protocolType, timeout)
            {
                SendTimeout = timeout,
                ReceiveTimeout = timeout
            };
        }

        public ClientWebSocket GetWebSocket()
        {
            return new ClientWebSocket()
            {
                Options = { Proxy = null }
            };
        }

        public class Socks5SocketClient : Socket, IAsyncConnect
        {
            private readonly EndPoint _proxyEndPoint;
            private readonly string? _username;
            private readonly string? _password;

            public Socks5SocketClient(EndPoint proxyEndPoint, string? username, string? password, AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, int timeout)
                : base(addressFamily, socketType, protocolType)
            {
                _proxyEndPoint = proxyEndPoint;
                _username = username;
                _password = password;
            }

            async ValueTask IAsyncConnect.ConnectAsync(EndPoint endPoint, CancellationToken cancellationToken)
            {
                byte atyp = 0;
                byte[] addrBytes = [];

                int targetPort = 0;
                if (endPoint is IPEndPoint iPEndPoint)
                {
                    targetPort = iPEndPoint.Port;

                    if (iPEndPoint.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        atyp = 0x01;
                        addrBytes = iPEndPoint.Address.GetAddressBytes();
                    }
                    else
                    {
                        atyp = 0x04;
                        addrBytes = iPEndPoint.Address.GetAddressBytes();
                    }
                }
                if (endPoint is DnsEndPoint dnsEndPoint)
                {
                    targetPort = dnsEndPoint.Port;

                    atyp = 0x03;
                    byte[] domain = Encoding.UTF8.GetBytes(dnsEndPoint.Host);
                    addrBytes = new byte[1 + domain.Length];
                    addrBytes[0] = (byte)domain.Length;
                    Buffer.BlockCopy(domain, 0, addrBytes, 1, domain.Length);
                }

                byte[] portBytes = new byte[2];
                portBytes[0] = (byte)((targetPort >> 8) & 0xFF);
                portBytes[1] = (byte)(targetPort & 0xFF);

                await base.ConnectAsync(_proxyEndPoint, cancellationToken);

                using NetworkStream stream = new NetworkStream(this, ownsSocket: false);

                #region 1. 握手阶段
                // 仅支持无认证
                byte[] handshake = new byte[] { 0x05, 0x01, 0x00 };
                if (!string.IsNullOrEmpty(_username))
                {
                    // 支持无认证(0x00) + 用户名密码认证(0x02)
                    handshake = new byte[] { 0x05, 0x02, 0x00, 0x02 };
                }

                stream.Write(handshake, 0, handshake.Length);
                byte[] resp = ReadExactly(stream, 2);

                if (resp[0] != 0x05)
                {
                    throw new IOException($"Invalid SOCKS version {resp[0]}");
                }

                if (resp[1] == 0xFF)
                {
                    throw new IOException("SOCKS5: no acceptable authentication methods");
                }
                #endregion

                #region 2. 用户名密码认证 
                if (resp[1] == 0x02)
                {
                    if (string.IsNullOrEmpty(_username))
                    {
                        throw new IOException("Proxy requires username/password but none provided");
                    }

                    byte[] user = Encoding.UTF8.GetBytes(_username);
                    byte[] pass = Encoding.UTF8.GetBytes(_password ?? "");
                    if (user.Length > 255 || pass.Length > 255)
                    {
                        throw new IOException("Username or password too long");
                    }

                    byte[] auth = new byte[3 + user.Length + pass.Length];
                    auth[0] = 0x01;
                    auth[1] = (byte)user.Length;
                    Buffer.BlockCopy(user, 0, auth, 2, user.Length);
                    auth[2 + user.Length] = (byte)pass.Length;
                    Buffer.BlockCopy(pass, 0, auth, 3 + user.Length, pass.Length);

                    stream.Write(auth, 0, auth.Length);
                    byte[] authResp = ReadExactly(stream, 2);
                    if (authResp[1] != 0x00)
                    {
                        throw new IOException("SOCKS5 authentication failed");
                    }
                }
                #endregion

                #region 3. 发送 CONNECT 请求
                using (var ms = new MemoryStream())
                {
                    ms.WriteByte(0x05); // VER
                    ms.WriteByte(0x01); // CMD = CONNECT
                    ms.WriteByte(0x00); // RSV
                    ms.WriteByte(atyp); // ATYP
                    ms.Write(addrBytes, 0, addrBytes.Length);
                    ms.Write(portBytes, 0, 2);

                    byte[] req = ms.ToArray();
                    stream.Write(req, 0, req.Length);
                }
                #endregion

                #region 4. 读取代理响应
                byte[] header = ReadExactly(stream, 4);
                if (header[1] != 0x00)
                {
                    throw new IOException($"SOCKS5 connect failed: REP=0x{header[1]:X2}");
                }

                int addrLen;
                switch (header[3])
                {
                    case 0x01:
                        {
                            addrLen = 4;
                        }
                        break;   // IPv4
                    case 0x04:
                        {
                            addrLen = 16;
                        }
                        break;  // IPv6
                    case 0x03:
                        {
                            byte[] len = ReadExactly(stream, 1);
                            addrLen = len[0];
                        }
                        break;
                    default:
                        throw new IOException($"Invalid ATYP {header[3]}");
                }

                _ = ReadExactly(stream, addrLen + 2); // BND.ADDR + BND.PORT（可忽略）
                #endregion
            }

            private static byte[] ReadExactly(NetworkStream stream, int len)
            {
                byte[] buf = new byte[len];
                int read = 0;
                while (read < len)
                {
                    int n = stream.Read(buf, read, len - read);
                    if (n <= 0)
                    {
                        throw new EndOfStreamException("Unexpected EOF");
                    }
                    read += n;
                }
                return buf;
            }
        }

        /// <summary>
        /// AsyncConnect
        /// </summary>
        public interface IAsyncConnect
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="endPoint"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public ValueTask ConnectAsync(EndPoint endPoint, CancellationToken cancellationToken);
        }
    }
}
