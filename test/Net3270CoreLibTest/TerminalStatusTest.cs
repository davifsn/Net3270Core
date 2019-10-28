using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net3270CoreLib;
using System;

namespace Net3270CoreLibTest
{
    [TestClass]
    public class TerminalStatusTest
    {
        private TerminalStatus st;

        public TerminalStatusTest()
        {
            st  = new TerminalStatus();
            st.updateStatus("U F U C(10.118.164.25) I 2 24 80 16 37 0x0 0.018");
        }
        
        [TestMethod]
        public void TestTerminalStatus()
        {
            Assert.IsTrue(st.KeyboardState == 'U');
            Assert.IsTrue(st.NumberOfRows == 24);
            Assert.IsTrue(st.NumberOfColumns == 80);
            Assert.IsTrue(st.CursorRow == 17);
            Assert.IsTrue(st.CursorColumn == 38);
            Assert.IsTrue(st.CommandExecutionTime > 0);
        }
    }
}
