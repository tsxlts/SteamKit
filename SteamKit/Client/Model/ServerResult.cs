using ProtoBuf;
using SteamKit.Client.Internal.Msg;

namespace SteamKit.Client.Model
{
    public class ServerResult
    {
        private ServerProtoBufMsg _serverProtoBufMsg;

        internal ServerResult(ServerProtoBufMsg serverProtoBufMsg)
        {
            _serverProtoBufMsg = serverProtoBufMsg;
        }

        public EMsg MsgType => _serverProtoBufMsg.Header.Msg;

        public EResult EResult => (EResult)_serverProtoBufMsg.Header.Proto.eresult;

        public string ErrorMessage => _serverProtoBufMsg.Header.Proto.error_message;

        public T? GetResult<T>() where T : IExtensible, new()
        {
            var result = new ServerProtoBufMsg<T>(_serverProtoBufMsg);

            return result.Body;
        }
    }
}
