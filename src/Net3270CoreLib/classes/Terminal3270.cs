using System;
using System.IO;
using System.Diagnostics;

namespace Net3270CoreLib
{
    /// <summary>Class <c>Terminal</c> representa um emulador de terminal 3270</summary>
    /// <remarks>Envelopa a utilização do s3270 para automação do emulador de terminal</remarks>
    public class Terminal3270
    {
        public String ModelNumber { get; set; }
        public String Address { get; set; }
        public int PortNumber { get; set; }
        public int ScriptPort { get; set; }
        public bool Visible { get; set; }
        public bool Connected { get; private set; }

        protected Process Emulator;
        protected Process Wrapper;

        protected StreamWriter TerminalInput;
        protected StreamReader TerminalOutput;
        protected StreamReader TerminalError;

        TerminalScreen Screen;
        TerminalCommand Command;
        TerminalStatus Status;

        public Terminal3270(String modelNumber, String address, int portNumber, bool visible = true, int scriptPort = 17938)
        {
            this.Address = address;
            this.ModelNumber = modelNumber;
            this.PortNumber = portNumber;
            this.ScriptPort = scriptPort;
            this.Visible = visible;

            this.Emulator = new Process();
            this.Wrapper = new Process();

            this.Screen = new TerminalScreen();
            this.Status = new TerminalStatus();
        }

        protected void setStartInfo()
        {
            Emulator.StartInfo.UseShellExecute = true;
            Emulator.StartInfo.CreateNoWindow = false;
            Emulator.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            Emulator.StartInfo.FileName = Visible ? "wc3270.exe" : "ws3270.exe";
            Emulator.StartInfo.Arguments =
                "-model " + ModelNumber +
                "-scriptport " + ScriptPort.ToString() +
                Address +
                (PortNumber != 0 ? (":" + PortNumber.ToString()) : "");

            Wrapper.StartInfo.UseShellExecute = false;
            Wrapper.StartInfo.RedirectStandardInput = true;
            Wrapper.StartInfo.RedirectStandardOutput = true;
            Wrapper.StartInfo.RedirectStandardError = true;
            Wrapper.StartInfo.CreateNoWindow = false;
            Wrapper.StartInfo.Arguments = "-i -t " + ScriptPort.ToString();
            Wrapper.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Wrapper.StartInfo.FileName = "x3270if.exe";
        }

        public void Connect()
        {
            if (!Connected)
            {
                try
                {
                    if (Emulator.Start())
                    {
                        Wrapper.Start();
                        TerminalInput = Wrapper.StandardInput;
                        TerminalOutput = Wrapper.StandardOutput;
                        TerminalError = Wrapper.StandardError;

                        if (Command == null)
                        {
                            Command = new TerminalCommand(TerminalOutput, TerminalInput);
                        }
                        Connected = true;
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
        }

        public void Diconnect()
        {
            if (Connected)
            {
                // Close Streams
                TerminalOutput.Close();
                TerminalError.Close();
                TerminalInput.Close();

                // Close x3270if
                Wrapper.WaitForExit();
                Wrapper.Close();

                // Close c3270/s3270
                Emulator.WaitForExit();
                Emulator.Close();

                Connected = false;
            }
        }
    }
}
