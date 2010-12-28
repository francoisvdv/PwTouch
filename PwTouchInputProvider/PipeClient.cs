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
        public enum Command { Stop, Start }

        public static bool SendMessage(Command cmd)
        {
            try
            {
                using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", PipeServer.SERVERNAME, PipeDirection.InOut))
                {
                    // Connect to the pipe or wait until the pipe is available.
                    Log.Write("Attempting to connect to pipe: " + PipeServer.SERVERNAME);
                    pipeClient.Connect(1000);
                    Log.Write("Connected to pipe.");

                    StreamWriter sw = new StreamWriter(pipeClient);
                    {
                        sw.AutoFlush = true;

                        sw.WriteLine(cmd.ToString());
                        pipeClient.WaitForPipeDrain();

                        Log.Write("Pipe Message Sent: " + cmd.ToString());
                    }

                    StreamReader sr = new StreamReader(pipeClient);
                    {
                        string temp;
                        while ((temp = sr.ReadLine()) != null)
                        {
                            Log.Write("Pipe Message Received: " + temp);

                            if (temp == "done")
                                return true;
                            else if (temp == "failed")
                                return false;
                        }
                    }

                    sw.Close();
                    sr.Close();
                }
            }
            catch (Exception exc)
            {
                Log.Write(exc.ToString(), true);
                return false;
            }

            return false;
        }
    }
}
