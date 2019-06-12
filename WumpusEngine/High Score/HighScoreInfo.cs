using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WumpusEngine
{
    /// <summary>
    /// is responsible for all the info for one high score.
    /// </summary>
    public class HighScoreInfo
    {
        public string Name { get; }
        public int Points { get; }
        public int Arrows { get; }
        public int Coins { get; }
        public string CaveName { get; }

        /// <summary>
        /// is the constructor for HighScoreInfo.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="CaveName"></param>
        /// <param name="points"></param>
        /// <param name="arrows"></param>
        /// <param name="coins"></param>
        public HighScoreInfo(string name, string CaveName, int points, int arrows, int coins)
        {
            this.Name = name;
            this.Points = points;
            this.CaveName = CaveName;
            this.Arrows = arrows;
            this.Coins = coins;
        }

        /// <summary>
        /// truns all the info passed through into a string.
        /// </summary>
        /// <returns> retruns unit StringOfInfo </returns>
        public override string ToString()
        {
            string SrtingOfInfo = Name + "\t" + CaveName + "\t" + Points + "\t" + Arrows + "\t" + Coins;
            return SrtingOfInfo;
        }

        /// <summary>
        /// turns the passed string into a array
        /// </summary>
        /// <param name="Line"></param>
        /// <returns> returns array of info </returns>
        public static HighScoreInfo Parse(string Line)
        {
            string[] parts = Line.Split('\t');
            string name = parts[0];
            string CaveName = parts[1];
            int Points = int.Parse(parts[2]);
            int arrows = int.Parse(parts[3]);
            int coins = int.Parse(parts[4]);
            return new HighScoreInfo(name, CaveName, Points, arrows, coins);
        }

    }
}

