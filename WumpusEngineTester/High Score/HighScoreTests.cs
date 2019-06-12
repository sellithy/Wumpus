using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WumpusEngine;

namespace WumpusEngineTester.High_Score
{
    [TestClass]
    class HighScoreTests
    {
        [TestMethod]
        public void TestConstructors()
        {
            HighScore highScore = new HighScore();
            highScore.EmptyList();
            highScore.Saving();

            HighScore myScore = new HighScore();
            List<HighScoreInfo> scores = myScore.GetHighScoreList();
            Assert.AreEqual(6, scores.Count);
            highScore.EmptyList();
        }

        [TestMethod]
        public void TestComparingTopTen()
        {
            HighScore HighScore = new HighScore();
            HighScore.EmptyList();

            List<HighScoreInfo> scores = HighScore.GetHighScoreList();
            Assert.AreEqual(0, scores.Count);

            for (int i = 0; i <= 12; i++)
            {
                HighScore.NewHighscore($"bob{i}", "Outer Space", 69, 76, 420);
            }
            scores = HighScore.GetHighScoreList();
            Assert.AreEqual( 10, scores.Count );
            HighScore.EmptyList();
        }

        [TestMethod]
        public void TestSorting()
        {
            HighScore highScore = new HighScore();
            highScore.EmptyList();

            highScore.NewHighscore("bob", "SpacePlace", 234, 2, 3);
            highScore.NewHighscore("jenifer", "spa", 521, 4, 6);
            highScore.NewHighscore("john", "HisHouse", 437, 3, 8);

           List<HighScoreInfo> NewList = highScore.GetHighScoreList();


            Assert.AreEqual("jenifer", NewList[0].Name);
            Assert.AreEqual("john", NewList[1].Name);
            Assert.AreEqual("bob", NewList[2].Name);
            highScore.EmptyList();
        }

        [TestMethod]
        public void TestSaveAndLoading()
        {
            HighScore highScore = new HighScore();
            highScore.EmptyList();

            highScore.NewHighscore("bob", "SpacePlace", 234, 2, 3);
            highScore.NewHighscore("jenifer", "spa", 521, 4, 6);
            highScore.NewHighscore("john", "HisHouse", 437, 3, 8);
            List<HighScoreInfo> OldList = highScore.GetHighScoreList();

            highScore.Saving();

            HighScore OtherScore = new HighScore();

            List<HighScoreInfo> NewList = OtherScore.GetHighScoreList();

            for(int i = 0; i < 3; i++)
            {
                Assert.AreEqual(NewList[i].Name, OldList[i].Name);
                Assert.AreEqual(NewList[i].CaveName, OldList[i].CaveName);
                Assert.AreEqual(NewList[i].Points, OldList[i].Points);
                Assert.AreEqual(NewList[i].Arrows, OldList[i].Arrows);
                Assert.AreEqual(NewList[i].Coins, OldList[i].Coins);
            }
            highScore.EmptyList();
        }
    }
}
