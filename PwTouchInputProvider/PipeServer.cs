using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
using System.IO;

namespace PwTouchInputProvider
{
    public static class PipeServer
    {
        public const string SERVERNAME = "PwTouchAppInputProviderPIPESERVER";

        static AsyncCallback onReceive;
        static NamedPipeServerStream pipeServer;

        public delegate void ReceivedDelegate(PipeClient.Command cmd);
        public static ReceivedDelegate OnReceived;

        static PipeServer()
        {
            onReceive += Received;
        }

        public static void Start()
        {
            if (pipeServer != null)
                throw new InvalidOperationException("Server already started. First call Close.");

            PipeSecurity ps = new PipeSecurity();
            PipeAccessRule par = new PipeAccessRule("Everyone", PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow);
            ps.AddAccessRule(par);

            pipeServer = new NamedPipeServerStream(SERVERNAME, 
                PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous, 4096, 4096, ps);

            Log.Write("NamedPipeServerStream object created, waiting for client connection...");
            pipeServer.BeginWaitForConnection(onReceive, new object());
        }

        public static void Stop()
        {
            if (pipeServer == null)
                return;

            pipeServer.Dispose();
            pipeServer = null;
        }

        static void Received(IAsyncResult result)
        {
            Log.Write("Client connected.");

            if (result.AsyncWaitHandle.WaitOne(5000))
            {
                pipeServer.EndWaitForConnection(result);

                try
                {
                    StreamReader sr = new StreamReader(pipeServer);
                    {
                        string temp;
                        while (pipeServer.IsConnected && (temp = sr.ReadLine()) != null)
                        {
                            Log.Write("Received: " + temp);

                            PipeClient.Command cmd = PipeClient.Command.Stop;
                            if (temp == "Start")
                                cmd = PipeClient.Command.Start;
                            else if (temp == "Stop")
                                cmd = PipeClient.Command.Stop;

                            if (OnReceived != null)
                                OnReceived(cmd);
                        }
                    }
                }
                // Catch the IOException that is raised if the pipe is broken
                // or disconnected.
                catch (IOException e)
                {
                    Log.Write("NAMED PIPE ERROR: " + e.ToString(), true);
                }
            }
            else
            {
                Log.Write("NAMED PIPE ERROR: AsyncWaitHandle timed out", true);
            }

            Stop();
            Start();
        }

        public static void SendConfirmation()
        {
            if (pipeServer == null || !pipeServer.IsConnected)
                return;

            StreamWriter sw = new StreamWriter(pipeServer);
            {
                sw.AutoFlush = true;
                sw.WriteLine("done");
                pipeServer.WaitForPipeDrain();
            }
        }
        public static void SendFailure()
        {
            if (pipeServer == null || !pipeServer.IsConnected)
                return;

            StreamWriter sw = new StreamWriter(pipeServer);
            {
                sw.AutoFlush = true;
                sw.WriteLine("failed");
                pipeServer.WaitForPipeDrain();
            }
        }
    }
}
