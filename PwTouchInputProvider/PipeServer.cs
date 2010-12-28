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
        static bool running;

        public delegate void ReceivedDelegate(string msg);
        public static ReceivedDelegate OnReceived;

        public static void Start()
        {
            if (pipeServer == null)
            {
                PipeSecurity ps = new PipeSecurity();
                PipeAccessRule par = new PipeAccessRule("Everyone", PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow);
                ps.AddAccessRule(par);

                pipeServer = new NamedPipeServerStream(SERVERNAME, 
                    PipeDirection.In, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous, 4096, 4096, ps);

                onReceive += Received;
            }

            Log.Write("NamedPipeServerStream object created.");

            // Wait for a client to connect
            Log.Write("Waiting for client connection...");
            pipeServer.BeginWaitForConnection(onReceive, new object());
        }

        public static void Stop()
        {
            if (pipeServer == null)
                return;

            pipeServer.EndWaitForConnection(null);
        }

        static void Received(IAsyncResult result)
        {
            Log.Write("Client connected.");

            if (result.AsyncWaitHandle.WaitOne(5000))
            {
                pipeServer.EndWaitForConnection(result);

                try
                {
                    using (StreamReader sr = new StreamReader(pipeServer))
                    {
                        // Display the read text to the console
                        string temp;
                        while ((temp = sr.ReadLine()) != null)
                        {
                            if (OnReceived != null)
                                OnReceived(temp);
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
        }
    }
}
