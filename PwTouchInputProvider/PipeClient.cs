using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;

namespace PwTouchInputProvider
{
    public static class PipeClient
    {
        public static void SendMessage(string msg)
        {
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", PipeServer.SERVERNAME, PipeDirection.Out))
            {
                // Connect to the pipe or wait until the pipe is available.
                Log.Write("Attempting to connect to pipe...");
                pipeClient.Connect(1000);
                Log.Write("Connected to pipe.");

                using (StreamWriter sw = new StreamWriter(pipeClient))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine(msg);
                    pipeClient.WaitForPipeDrain();
                }

                Log.Write("Pipe Message Sent");
            }
        }
    }
}
