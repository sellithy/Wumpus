using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusEngine
{
    /// <summary>
    /// creates a list of the top ten highscores and be able to save each high score to a sepret list.
    /// </summary>
    class HighScore
    {

        /// <summary>
        /// created a constuctor so that the first thing that is read through is the IncomingInfo.
        /// </summary>
        public HighScore()
        {
            IncomingInfo();
            if (scores.Count < 1)
            {
                scores.Add(new HighScoreInfo("Damon", "Middle Earth", 696969, 420, 42069));
                scores.Add(new HighScoreInfo("Chris", "Whiterun", 150, 1, 90));
                scores.Add(new HighScoreInfo("Rowen", "DeapSpaceNine", 100, 1, 56));
                scores.Add(new HighScoreInfo("Shehab", "Tatooine", 10, 1, 4));
                scores.Add(new HighScoreInfo("Ryan", "Whiterun", 300, 1, 100));
                scores.Add(new HighScoreInfo("Cameron", "MiddleEarth", 1, 1, 48));
                scores.Add(new HighScoreInfo("Alexander", "DarkCastle", 111, 1, 30));
            }
            SortList();

        }

        /// <summary>
        /// makes it so that no one can change the value of the scores list.
        /// </summary>
        /// <returns> returns a copy of the scores list </returns>
        public List<HighScoreInfo> GetHighScoreList()
        {
            // problem with taking what seems to be the "straightforward" way of returning
            // our list of scores: outside code can modify the list, and it gets changed
            // here too -- because we're handing over a REFERENCE to our List.
            // return this.scores;
            // It's better to return a COPY of our List
            return new List<HighScoreInfo>(this.scores);
        }

        /// <summary>
        /// is the main list that contains the top ten scores
        /// </summary>
        private List<HighScoreInfo> scores = new List<HighScoreInfo>();
        
        /// <summary>
        /// creates a unit that contants the filename
        /// </summary>
        public string filename = "HighScores.txt";

        /// <summary>
        /// Inserts a new score into the list of high scores. starts the highscore with team members at highest scores. calls the methode SortList.
        /// </summary>
        /// <param name="Names"></param>
        /// <param name="CaveName"></param>
        /// <param name="Points"></param>
        /// <param name="Arrows"></param>
        /// <param name="Coins"></param>
        public void NewHighscore(string Names, string CaveName, int Points, int Arrows, int Coins)
        {
            HighScoreInfo AddedScore = new HighScoreInfo(Names, CaveName, Points, Arrows, Coins);
            scores.Add(AddedScore);
            if (scores.Count < 1)
            {
                scores.Add(new HighScoreInfo("Chris", "Whiterun", 7, 1, 3));
                scores.Add(new HighScoreInfo("Rowen", "DeapSpaceNine", 5, 1, 3));
                scores.Add(new HighScoreInfo("Shehab", "Tatooine", 9, 1, 3));
                scores.Add(new HighScoreInfo("Ryan", "RivetCity", 4, 1, 3));
                scores.Add(new HighScoreInfo("Cameron", "MiddleEarth", 3, 1, 3));
                scores.Add(new HighScoreInfo("Alexander", "DarkCastle", 2, 1, 3));
            }
            SortList();
        }

        /// <summary>
        /// Retrives the current highest score from a list.
        /// </summary>
        /// <returns></returns>
        private int GetHighest()
        {
            int higher = 0;
            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i].Points > scores[higher].Points)
                {
                    higher = i;
                }
            }
            return higher;
        }

        /// <summary>
        /// clears the entier highscore top ten from scores.
        /// </summary>
        public void EmptyList()
        {
            scores.Clear();
        }


        /// <summary>
        /// creates a list and compaires the scores from one to ten and places the top ten list into the score list. 
        /// </summary>
        private void SortList()
        {
            List<HighScoreInfo> sorted = new List<HighScoreInfo>();

            while (scores.Count > 0 && sorted.Count < 10)
            {
                int highest = GetHighest();
                HighScoreInfo count = scores[highest];
                sorted.Add(count);
                scores.RemoveAt(highest);
            }
            scores = sorted;
        }


        /// <summary>
        /// takes information that someone else give to this class and reads it into the class 
        /// </summary>
        private void IncomingInfo()
        {
            if (File.Exists(filename))
            {
                scores.Clear();
                StreamReader sr = new StreamReader(filename);
                while (!sr.EndOfStream)
                {
                    string Line = sr.ReadLine();
                    HighScoreInfo hs = HighScoreInfo.Parse(Line);
                    this.scores.Add(hs);
                }
                sr.Close();
                SortList();
            }
        }

        /// <summary>
        /// //saves all scores read through highscore info to the scores file
        /// </summary>
        public void Saving()
        {
            StreamWriter sw = new StreamWriter(filename);
            for (int r = 0; r < scores.Count; r++)
            {
                sw.WriteLine(scores[r]);
            }
            sw.Close();
        }
    }
}
