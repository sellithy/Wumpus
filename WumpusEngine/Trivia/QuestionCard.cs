using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WumpusEngine
{
    /// <summary>
    /// Manages the information of a given question
    /// </summary>
    public class QuestionCard
    {
        private string question; //The question
        private string answer; //The correct answer to the question
        private string hint; //A hint leading the player towards the correct answer
        private string[] answers = new string[4]; //An array of 4 posible answers (Including the correct one)
        Random randyRandom; //The best Rimworld storyteller


        /// <summary>
        /// Creates a question object.
        /// </summary>
        /// <param name="reader">The StreamReader used to get information into a question card.</param>
        /// <param name="randyRandom">Random object used to randomise question order</param>
        public QuestionCard(TextReader reader, Random randyRandom)
        {
            question = reader.ReadLine();
            answer = reader.ReadLine();
            hint = reader.ReadLine();
            //Slightly redundant code (Consider using a for loop?)
            answers[0] = reader.ReadLine();
            answers[1] = reader.ReadLine();
            answers[2] = reader.ReadLine();
            answers[3] = answer;

            this.randyRandom = randyRandom;
            for (int current = answers.Length - 1; current >= 0; current--)
            {
                string currentCard = answers[current];
                int randyNumber = randyRandom.Next(current + 1);
                string swappingCard = answers[randyNumber];

                answers[randyNumber] = currentCard;
                answers[current] = swappingCard;
            }
        }


        /// <summary>
        /// Returns the hint for the given question.
        /// </summary>
        /// <returns>The hint for the given question</returns>
        internal string Givehint()
        {
            // ^ note: internal means "visible to everything within this assembly"
            // ...where "this assembly" is all the code that lives in the DLL
            // - i.e. this entire Wumpus Engine, excluding UI.
            return hint;
        }

        /// <summary>
        /// Returns the question from the given QuestionCard
        /// </summary>
        /// <returns>The question</returns>
        public string GiveQuestion()
        {
            return question;
        }

        /// <summary>
        /// Returns posible answers for a question that players can choose form.
        /// </summary>
        /// <returns>An array full of possible answers</returns>
        public string[] GiveAnswers()
        {
            return answers;
        }

        /// <summary>
        /// Returns if the question was answered correctly
        /// </summary>
        /// <param name="playerResponse">The players answer to a given question</param>
        /// <returns>The hasBeenCorectlyAnswered variable</returns>
        internal bool WasThePlayerCorrect(string playerResponse)
        {
            if (playerResponse.ToLower() == answer.ToLower())
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Changes what happens when a QuestionCard object is converted into a string.
        /// </summary>
        /// <returns>The question, answer and hint contained withing a given question card object.</returns>
        public override string ToString()
        {
            return $"QUESTION: {this.question}, ANSWER: {this.answer}, HINT: {this.hint}";
        }


        //TESTING METHODS:
        /// <summary>
        /// Gives the correct answer. FOR TESTING ONLY.
        /// </summary>
        /// <returns>The correct answer</returns>
        internal string GiveCorrectAnswer()
        {
            return answer;
        }
    }
}
