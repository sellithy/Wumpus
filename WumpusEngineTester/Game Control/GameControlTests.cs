using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WumpusEngine;

namespace WumpusEngineTester.Game_Control
{
    [TestClass]
    public class GameControlTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            GameControl gameControl = GameControl.GetMaintained();
        }

        /// <summary>
        /// Tests for no errors
        /// This will fail if there is no door in the south direction
        /// </summary>
        [TestMethod]
        public void GetDoorsTest()
        {
            GameControl gameControl = GameControl.GetMaintained();
            gameControl.ChooseLayout(1);
            gameControl.GetDoors();
            gameControl.Direction('3');
        }

        /// <summary>
        /// Tests that a trivia game can be run
        /// </summary>
        [TestMethod]
        public void TriviaGameTest()
        {
            GameControl gameControl = GameControl.GetMaintained();
            gameControl.ChooseLayout(1);
            gameControl.PurchaseSecret();
            QuestionCard question = gameControl.GetQuestion();
            Debug.WriteLine("question: " + gameControl.GetQuestion());
            Debug.WriteLine("correct? " + gameControl.AnswerQuestion(question.GiveCorrectAnswer()));
            Debug.WriteLine("State: " + gameControl.DidPlayerWin());
        }

        /// <summary>
        /// Tests if highscore is able to return the current highscores
        /// </summary>
        [TestMethod]
        public void HighScoreTest()
        {
            GameControl gameControl = GameControl.GetMaintained();
            gameControl.ChooseLayout(1);            
            gameControl.AddHighscore("McTavish", false);
            List<HighScoreInfo> higscores = gameControl.GetHighscores();
            foreach (HighScoreInfo h in higscores)
            {
                Debug.WriteLine(h);
            }
        }
    }
}