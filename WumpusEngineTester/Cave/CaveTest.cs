using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using WumpusEngine;
using System.Diagnostics;

namespace WumpusEngineTester.CaveTests
{
    [TestClass]
    public class Tester
    {
        [TestMethod]
        public void TestConstructor()
        {
            Cave CaveTest = new Cave(1);
            for (int i = 0; i < 30; i++)
            {
                Assert.AreEqual(CaveTest.PassRoom(i).RoomNumber(), i+1);//testing the roonnumber
            }
            Assert.AreEqual(CaveTest.PassRoom(0).GetTunnelInfo(2).GetToRoom(), 2);    //testing the tunnel info    
        }
    }
}
