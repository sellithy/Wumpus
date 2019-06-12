using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WumpusEngine
{
    /// <summary>
    /// Keeps track of the status of a trivia game
    /// </summary>
    public enum TriviaState {InProgress, Lost, Won}
    /// <summary>
    /// Manages trivia contests
    /// </summary>
    public class Trivia
    {
        private List<QuestionCard> PosibleQuestions = new List<QuestionCard>(); //Complete list of all questions that could possibly be asked. Does not change throughout a game.
        private List<QuestionCard> AvailableQuestions = new List<QuestionCard>(); //List of questions that are available to ask at any given point. Changes throughout the game.
        private QuestionCard currentQuestion; //Question that is currently being asked
        private int correctAnswers = 0; //How many questions have been answered correctly
        private int correctAnswersNeeded = 0; //The number of correct answers needed to win a game
        Random randyRandom; //The best Rimworld storyteller

        /// <summary>
        /// Keeps track of the status of a trivia game
        /// </summary>
        public TriviaState triviaState;
        private int turnsLeft = 0;
        

        /// <summary>
        /// Creates a triva object and
        /// loads a document full of Questions/answers and puts the data into question objects.
        /// </summary>
        internal Trivia(Random randyRandom)
        {
            string questionData = (string)TriviaResources.ResourceManager.GetObject("Questions");
            this.randyRandom =  randyRandom;
            //Create questioncards and put them into a list 
            using (StringReader reader = new StringReader(questionData))
            {
                this.PosibleQuestions = LoadQuestions(reader);
            }
            //set available questions to be equal to posible questions
            ResetQuestionList();
            this.currentQuestion = null;
            //creates a random object


        }

        private List<QuestionCard> LoadQuestions(TextReader reader)
        {
            List<QuestionCard> questions = new List<QuestionCard>();
            while (reader.ReadLine() != null)
            {
                questions.Add(new QuestionCard(reader, randyRandom));
            }
            return questions;
        }

        //START BATTLE (reset)
        /// <summary>
        /// Sets up for a new trivia game
        /// </summary>
        /// <param name="pointsNeeded">The number of questions needed to win a trivia game.</param>
        /// <param name="turns">The number of turns in a trivia game</param>
        internal void StartTriviaGame(int pointsNeeded, int turns)
        {
            correctAnswersNeeded = pointsNeeded;
            correctAnswers = 0;
            currentQuestion = null;

            turnsLeft = turns;
            triviaState = TriviaState.InProgress;
        }

        //PASS QUESTION
        /// <summary>
        /// Gives the current question. Use when a question needs to be asked.
        /// </summary>
        /// <returns>The current QuestionCard to be asked.</returns>
        public QuestionCard AskQuestion()
        {
            GetNewCurrentQuestion();
            return currentQuestion;
        }

        //CHECK TO SEE IF THE ANSWER IS CORRECT
        /// <summary>
        /// Checks to see if the player answered the question correctly
        /// </summary>
        /// <param name="playerResponse">How the player responded to the question.</param>
        /// <returns>If the player answered the question correctly (Use this info to give visual feedback, not to keep track of a running total (As I will be doing that job))</returns>
        public bool AnswerQuestion(string playerResponse)
        {
            turnsLeft--;
            if (currentQuestion.WasThePlayerCorrect(playerResponse))
            {
                correctAnswers++;
                return true;
            }
            return false;
        }

        //CHECK TO SEE IF THEY WON
        /// <summary>
        /// Checks to see if the player has won the trivia game.
        /// It does nothing more and nothing less than that.
        /// </summary>
        /// <returns>If the player won the game (Priority given to winning), is still playing the game or lost the game.</returns>
        public TriviaState DidTheyWin()
        {
            if (correctAnswers >= correctAnswersNeeded)
            {
                return triviaState = TriviaState.Won;
            }
            else if (turnsLeft != 0)
                return (triviaState = TriviaState.InProgress);
            else return (triviaState = TriviaState.Lost);
        }

        /// <summary>
        /// Set the current question to a new question
        /// and remove the last current question from the available question list.
        /// Used within trivia exclusivly.
        /// </summary>
        private void GetNewCurrentQuestion()
        {
            //If there is a current question, it is removed from the list of available questions
            if (currentQuestion != null)
                AvailableQuestions.Remove(currentQuestion);
            //Check to see if the AvailableQuestions is empty, and resets it if it is
            if (AvailableQuestions.Count <= 0)
                ResetQuestionList();
            //Gets a new current question
            int chosen = randyRandom.Next(AvailableQuestions.Count);
            currentQuestion = AvailableQuestions[chosen];
        }

        /// <summary>
        /// Gives a random (not the current) QuestionCard's hint.
        /// </summary>
        /// <returns>A random question's hint</returns>
        public string GiveRandomHint()
        {
            return PosibleQuestions[randyRandom.Next(PosibleQuestions.Count)].Givehint();
        }

        //Reset the list of available or already asked trivia questions (Used at the start of the game or when out of trivia questions)
        /// <summary>
        /// Resets the list of available questions (Used at the start of a game or when out of trivia questions).
        /// </summary>
        private void ResetQuestionList()
        {
            AvailableQuestions = new List<QuestionCard>(PosibleQuestions);
        }


        //TESTING METHODS:

        /// <summary>
        /// Prints all of the information of a question object. FOR TESTING ONLY.
        /// </summary>
        /// <param name="number">The number of the question to be printed.</param>
        internal void PrintQuestion(int number)
        {
            Console.WriteLine(PosibleQuestions[number]);
        }

    }
}
