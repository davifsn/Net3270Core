using System;
using System.IO;

namespace Net3270CoreLib
{
    /// <summary>Class <c>Status</c> representa a barra de Status do terminal</summary>
    /// <remarks>Engloba os métodos de interação com o terminal</remarks>
    public class TerminalStatus
    {

        public String StatusLine { get; private set; }

        public char KeyboardState { get; private set; }
        public char ScreenFormatting { get; private set; }
        public char FieldProtection { get; private set; }
        public String ConnectionState { get; private set; }
        public char EmulatorMode { get; private set; }
        public int ModelNumber { get; private set; }
        public int NumberOfRows { get; private set; }
        public int NumberOfColumns { get; private set; }
        public int CursorRow { get; private set; }
        public int CursorColumn { get; private set; }
        public String WindowID { get; private set; }
        public float CommandExecutionTime { get; private set; }

        public void updateStatus(String status)
        {
            StatusLine = status;
            String[] fields = status.Split(' ');

            try
            {
                KeyboardState = fields[0].ToCharArray()[0];
                ScreenFormatting = fields[1].ToCharArray()[0];
                FieldProtection = fields[2].ToCharArray()[0];
                ConnectionState = fields[3];
                EmulatorMode = fields[4].ToCharArray()[0];
                ModelNumber = int.Parse(fields[5]);
                NumberOfRows = int.Parse(fields[6]);
                NumberOfColumns = int.Parse(fields[7]);
                CursorRow = int.Parse(fields[8]) + 1;
                CursorColumn = int.Parse(fields[9]) + 1;
                WindowID = fields[10];
                CommandExecutionTime = float.Parse(fields[11]);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }


        }
    }
}