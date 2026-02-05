using System.Threading.Tasks.Dataflow;
using ProtoBuf;
using SteamKit.Client.Model;

namespace SteamKit.Client.Internal
{
    internal class MsgCallbackManager
    {
        private readonly BaseClient client;
        private readonly BufferBlock<IServerMsg> msgQueue;
        private readonly object locker;

        private Thread? thread;
        private CancellationTokenSource msgCallbackTokenSource;

        public MsgCallbackManager(BaseClient baseClient)
        {
            client = baseClient;
            msgQueue = new BufferBlock<IServerMsg>();
            locker = new object();

            thread = null;
            msgCallbackTokenSource = new CancellationTokenSource();
        }

        public void PostMsg(IServerMsg msg)
        {
            msgQueue.Post(msg);
            Run();
        }

        private void Run()
        {
            lock (locker)
            {
                if (thread?.IsAlive ?? false)
                {
                    return;
                }

                msgCallbackTokenSource.Cancel();
                msgCallbackTokenSource = new CancellationTokenSource();

                thread = new Thread(() =>
                {
                    while (!msgCallbackTokenSource.IsCancellationRequested)
                    {
                        try
                        {
                            if (!msgQueue.TryReceive(out var packetMsg))
                            {
                                break;
                            }

                            var callback = client.GetMessageCallback(packetMsg.MsgType);
                            if (callback != null)
                            {
                                callback.InvokeAsync(this, new MessageCallback
                                {
                                    PacketResult = packetMsg
                                }, client.Logger).ConfigureAwait(false);
                            }

                            var handlers = client.GetMessageHandlers();
                            foreach (var handler in handlers)
                            {
                                try
                                {
                                    handler.HandleMsgAsync(packetMsg).ConfigureAwait(false);
                                }
                                catch (ProtoException ex)
                                {
                                    client.Logger?.LogException(ex, $"'{handler.GetType().Name}' handler failed to (de)serialize a protobuf: {ex}");
                                }
                                catch (Exception ex)
                                {
                                    client.Logger?.LogException(ex, $"Unhandled exception from '{handler.GetType().Name}' handler: {ex}");
                                }
                            }
                        }
                        catch (OperationCanceledException)
                        {

                        }
                        catch (Exception ex)
                        {
                            client.Logger?.LogException(ex, $"msgcallback exception: {ex}");
                        }
                    }
                })
                {
                    Name = "Steamkit-MsgCallback",
                    IsBackground = true
                };

                thread.Start();
            }
        }
    }
}
