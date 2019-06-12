using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WumpusEngine;

namespace WumpusEngineTester.GameLocations_and_Player
{
    [TestClass]
    public class PlayerObjectTest
    {
        [TestMethod]
        public void TestConstructer()
        {
            //Make sure nothing is horribly wrong with the constructer
            PlayerObject playerObject = new PlayerObject();
        }
        [TestMethod]
        public void TestInventoryManagement()
        {
            //Perform tests on the player's inventory and make sure the arrows and coins are the right value, and that the score calculates correctly
            PlayerObject playerObject = new PlayerObject();
            playerObject.Add2Arrows();
            playerObject.AddCoins(5);
            playerObject.IncreaseTurns();
            Assert.AreEqual(204, playerObject.CalculateScore(true));
            Assert.AreEqual(154, playerObject.CalculateScore(false));
            Assert.AreEqual(5, playerObject.GetArrowNum());
            Assert.AreEqual(5, playerObject.GetCoinNum());
            Assert.AreEqual(1, playerObject.GetTurnNum());
        }
    }
}
