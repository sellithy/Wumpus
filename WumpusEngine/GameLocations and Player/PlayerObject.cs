using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Ryan Lawler
namespace WumpusEngine
{
    class PlayerObject
    {
        private int arrowNum;
        private int coinNum;
        private int turnNum;
        /// <summary>
        /// Constructor to initialize object
        /// </summary>
        public PlayerObject()
        {
            //Set fields to starting values
            arrowNum = 3;
            coinNum = 0;
            turnNum = 0;
        }
        /// <summary>
        /// Returns the score of the player
        /// </summary>
        /// <param name="wumpusDefeated">Whether the Wumnpus was defeated or not</param>
        /// <returns>Player score</returns>
        public int CalculateScore(bool wumpusDefeated)
        {
            //Calculate the score and add 50 pts if the wumpus was defeated (otherwise add nothing), then return it as an int
            if(wumpusDefeated == true)
            {
                return 150 - turnNum + coinNum + (10 * arrowNum);
            }
            else
            {
                return 100 - turnNum + coinNum + (10 * arrowNum);
            }
        }
        /// <summary>
        /// Accessor that returns the number of coins the player has
        /// </summary>
        /// <returns>Number of coins</returns>
        public int GetCoinNum()
        {
            //Return the field CoinNum as an int
            return coinNum;
        }
        /// <summary>
        /// Accessor that returns the number of turns the player has taken
        /// </summary>
        /// <returns>Number of turns</returns>
        public int GetTurnNum()
        {
            return turnNum;
        }
        /// <summary>
        /// Accessor that returns the number of arrows the player has
        /// </summary>
        /// <returns>Number of arrows</returns>
        public int GetArrowNum()
        {
            //Return the field ArrowNum as an int
            return arrowNum;
        }
        /// <summary>
        /// Adds two arrows to the player's inventory
        /// </summary>
        public void Add2Arrows ()
        {
            arrowNum += 2;
        }
        /// <summary>
        /// Changes the number of coins by the desired number
        /// </summary>
        /// <param name="num">Amount to change the number of coins</param>
        public void AddCoins(int num)
        {
            coinNum += num;
        }
        /// <summary>
        /// Subtracts an arrow from the player's inventory
        /// </summary>
        public void SubtractArrow ()
        {
            arrowNum -= 1;
        }
        /// <summary>
        /// Increases the player's turn number
        /// </summary>
        public void IncreaseTurns ()
        {
            turnNum ++;
        }
    }
}
