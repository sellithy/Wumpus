using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WumpusEngine;

namespace WumpusEngineTester.TriviaTests
{
    [TestClass]
    public class TriviaTester
    {
        /// <summary>
        /// Makes sure that a trivia object can be constructed without issue.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            Trivia trivia = new Trivia(new Random());
            //trivia.PrintQuestion(0);
        }

        /// <summary>
        /// Tests individual question mechanics. Gives the correct answer and makes sure that the trivia card says it is the correct answer.
        /// </summary>
        [TestMethod]
        public void AskAQuestionRight()
        {           
            Trivia trivia = new Trivia(new Random());
            trivia.StartTriviaGame(1,0);
            QuestionCard currentQuestion = trivia.AskQuestion();

            Assert.IsTrue(currentQuestion.WasThePlayerCorrect(currentQuestion.GiveCorrectAnswer()));
        }

        /// <summary>
        /// Tests individual question mechanics. Intentionally gives the wrong answer and makes sure that the trivia card says it is the wrong answer.
        /// </summary>
        [TestMethod]
        public void AskAQuestionWrong()
        {
            Trivia trivia = new Trivia(new Random());
            trivia.StartTriviaGame(1,0);
            QuestionCard currentQuestion = trivia.AskQuestion();

            Assert.IsFalse(currentQuestion.WasThePlayerCorrect("THIS IS THE WRONG ANSWER YOU BLOODY IDIOTS!"));
        }

        /// <summary>
        /// Tests to see if the 'DidTheyWin' method will properly return 'Won' when needed.
        /// </summary>
        [TestMethod]
        public void PlayAGameWin()
        {
            Trivia trivia = new Trivia(new Random());
            trivia.StartTriviaGame(3,3);

            //Single question mechanics
            QuestionCard currentQuestion = trivia.AskQuestion();
            trivia.AnswerQuestion(currentQuestion.GiveCorrectAnswer());

            Assert.AreEqual(trivia.DidTheyWin(), TriviaState.InProgress);

            currentQuestion = trivia.AskQuestion();
            trivia.AnswerQuestion(currentQuestion.GiveCorrectAnswer());

            Assert.AreEqual(trivia.DidTheyWin(), TriviaState.InProgress);

            currentQuestion = trivia.AskQuestion();
            trivia.AnswerQuestion(currentQuestion.GiveCorrectAnswer());

            Assert.AreEqual(trivia.DidTheyWin(),TriviaState.Won);
        }

        /// <summary>
        /// Tests to see if the 'DidTheyWin' method will properly return 'Lost' when needed.
        /// </summary>
        [TestMethod]
        public void PlayAGameLoose()
        {
            Trivia trivia = new Trivia(new Random());
            trivia.StartTriviaGame(3,3);

            QuestionCard currentQuestion = trivia.AskQuestion();
            trivia.AnswerQuestion("THIS IS THE WRONG ANSWER YOU BLOODY IDIOTS!");

            Assert.AreEqual(trivia.DidTheyWin(), TriviaState.InProgress);

            currentQuestion = trivia.AskQuestion();
            trivia.AnswerQuestion("THIS IS THE WRONG ANSWER YOU BLOODY IDIOTS!");

            Assert.AreEqual(trivia.DidTheyWin(), TriviaState.InProgress);

            currentQuestion = trivia.AskQuestion();
            trivia.AnswerQuestion("THIS IS THE WRONG ANSWER YOU BLOODY IDIOTS!");

            Assert.AreEqual(trivia.DidTheyWin(),TriviaState.Lost);
        }

        /// <summary>
        /// Tests to see if the 'DidTheyWin' method will properly return 'InProgress' when needed.
        /// </summary>
        [TestMethod]
        public void PlayAGameInProgress()
        {
            Trivia trivia = new Trivia(new Random());
            trivia.StartTriviaGame(3, 5);

            QuestionCard currentQuestion = trivia.AskQuestion();
            trivia.AnswerQuestion("THIS IS THE WRONG ANSWER YOU BLOODY IDIOTS!");

            Assert.AreEqual(trivia.DidTheyWin(), TriviaState.InProgress);

            currentQuestion = trivia.AskQuestion();
            trivia.AnswerQuestion(currentQuestion.GiveCorrectAnswer());

            Assert.AreEqual(trivia.DidTheyWin(), TriviaState.InProgress);
        }

    }
}
