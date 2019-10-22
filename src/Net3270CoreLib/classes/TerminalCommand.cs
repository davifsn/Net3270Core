using System;
using System.IO;

namespace Net3270CoreLib
{
    /// <summary>Class <c>Command</c> representa os comandos enviados</summary>
    /// <remarks>Engloba os métodos de interação com o terminal</remarks>
    class TerminalCommand
    {

        protected StreamReader CommandOutput { get; set; }
        protected StreamWriter CommandInput { get; set; }

        public TerminalCommand(StreamReader commandOutput, StreamWriter commandInput)
        {

        }

        protected bool SendCommand(String command)
        {
            CommandInput.WriteLine(command);
            
            return false;
        }

        public void SendKeys() { }

        public void SendEnter()
        {
            SendCommand("Enter");
        }

        public void SendTab()
        {
            SendCommand("Tab");
        }

        public void SendHome()
        {
            SendCommand("Home");
        }
        
        public void SendEraseEoF()
        {
            SendCommand("EraseEOF");
        }

        public void SendPF(int programFunction)
        {
            if (programFunction >= 1 && programFunction <= 24)
            {
                SendCommand("PF(" + programFunction.ToString() + ")");
            }
        }

        public void SendPF1()
        {
            SendCommand("EraseEOF");
        }
    }
}