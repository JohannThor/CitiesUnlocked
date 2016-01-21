using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class Bluetooth
    {
        [TestMethod]
        public void TestPeerList()
        { 
            var connector = new WindowsPhoneUI.BluetoothConnector();
            var peers = connector.getPeers();
            peers.Wait();
            var result = peers.Result;
        }
    }
}
