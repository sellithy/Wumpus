using System;
using System.Collections.Generic;

namespace WumpusEngine
{
    public enum TriviaType { Arrows, Secrets, Pit, Wumpus };
    public enum States{GameStart, GameLoad, BatAbduction, PitFall, FightingWumpus, Won, Wandering, InRoom, InTrivia}
    /// <summary>
    /// The main controller for the Wumpus game
    /// </summary>
    public class GameControl
    {
        private static GameControl maintainedInstance;
        private States gameState;
        private TriviaType triviaType;
        private Layouts caveLayout;
        private GameLocations gameLocations;
        private Trivia trivia;
        private HighScore highScore;
        private Random random;
        private int moveCounter;

        #region At Launch

        #region Maintained Stuff        
        /// <summary>
        /// Makes sure only one GameControl is created
        /// </summary>
        /// <returns></returns>
        public static GameControl GetMaintained()
        {
            if (maintainedInstance == null)
                maintainedInstance = new GameControl();
            return maintainedInstance;
        }

        /// <summary>
        /// Resets the maintained GameController
        /// </summary>
        public static void ResetMaintained()
        {
            maintainedInstance = null;
        }

        /// <summary>
        /// Assumes a new game is starting
        /// </summary>
        internal GameControl()
        {
            gameState = States.Wandering;
            random = new Random();
            trivia = new Trivia(random);
            highScore = new HighScore();
        }
        #endregion

        /// <summary>
        /// Loads the layout of the cave
        /// </summary>
        /// <param name="layoutNum">1 to 5 to indicate wich layout the player chose</param>
        public void ChooseLayout(int layoutNum)
        {
            caveLayout = (Layouts) layoutNum;
            gameLocations = new GameLocations(layoutNum, random);
        }

        /// <summary>
        /// Load a game from the given string
        /// TODO: implement later
        /// </summary>
        /// <param name="save"> The string containing the save to be loaded</param>
        public void LoadGame(string save)
        {
            gameState = States.GameLoad;
            //TODO: Intialize the objects in a certain way
        }
        #endregion

        #region Hazards

        /// <summary>
        /// Gets the warning messages to be displayed relating to nearby hazards
        /// </summary>
        /// <returns></returns>
        public string[] IsHazardsNear()
        {
            //TODO: Get a list of warnings instead of one string from GameLocations
            string[] messages = gameLocations.GetWarningMessages().ToArray();
            if (messages.Length == 0)
                return new string[] { "" };
            return messages;
        }

        /// <summary>
        /// Gets a list of hazards in the current room of the player represented through an array of booleans
        /// </summary>
        /// <returns>Array with list of hazards. First element is bat. Second is Pits. Third is Wumpus</returns>
        public bool[] IsHazardsInRoom()
        {
            return gameLocations.GetHazardsInPlayersRoom();
        }

        #endregion

        #region Room/Movment
        /// <summary>
        /// Gets the direction that the player chooses and moves the player while updating the number of coins
        /// Keeps track of number of moves
        /// </summary>
        /// <param name="DoorNumber">0 to 5 indicating the direction chosen</param>
        public void Direction(char doorNumber)
        {
            int direction = int.Parse("" + doorNumber);
            moveCounter++;
            gameLocations.ChangeCoins(1);
            gameLocations.ChangePlayerLocation(direction);
        }

        /// <summary>
        /// Gets the doors in the current room
        /// </summary>
        /// <returns>Array of ints representing the dierection that a door is present</returns>
        public int[] GetDoors()
        {
            gameState = States.InRoom;
            return gameLocations.GetPlayerRoomInfo().TunnelLocations();
        }

        /// <summary>
        /// Returns the current room number. 
        /// Mainly for testing but might add a function in Unity that displays the room num
        /// </summary>
        /// <returns>The room number</returns>
        public int RoomNumber()
        {
            return gameLocations.GetLocationInfo()[0];
        }
        #endregion

        #region Trivia

        /// <summary>
        /// Gets the reason the trivia game was launched
        /// </summary>
        /// <returns>An enum representing the reason for a trivia type</returns>
        public TriviaType GetTriviaType()
        {
            return triviaType;
        }

        /// <summary>
        /// Starts a purchace process
        /// </summary>
        /// <returns>If the trivia game was won or not</returns>
        public void PurchaseSecret()
        {
            gameState = States.InTrivia;
            triviaType = TriviaType.Secrets;
            trivia.StartTriviaGame(2, 3);
        }

        /// <summary>
        /// Starts a trivia game to purchace arrows
        /// </summary>
        /// <returns>If the trivia game was won or not</returns>
        public void PurchaseArrows()
        {
            triviaType = TriviaType.Arrows;
            gameState = States.InTrivia;
            trivia.StartTriviaGame(2, 3);
        }

        /// <summary>
        /// Starts a trivia game to battle the Wumpus 
        /// </summary>
        public void BattleWumpus()
        {
            triviaType = TriviaType.Wumpus;
            gameState = States.InTrivia;
            trivia.StartTriviaGame(3, 5);
        }

        /// <summary>
        /// Starts a trivia game to survive a pitfall
        /// </summary>
        public void Pitfall()
        {
            triviaType = TriviaType.Pit;
            gameState = States.InTrivia;
            trivia.StartTriviaGame(2, 3);
        }

        /// <summary>
        /// Called after sucessfully surviving bats
        /// </summary>
        public void BatTransport()
        {
            gameLocations.ResolveBats();
        }

        /// <summary>
        /// Gets the next question from trivia
        /// </summary>
        /// <returns>A question card holding information about the question to be asked</returns>
        public QuestionCard GetQuestion()
        {
            gameLocations.ChangeCoins(-1);
            return trivia.AskQuestion();
        }

        /// <summary>
        /// Recieves an answer from the player
        /// GUI is supposed to indicate to the player if the answer was right or wrong
        /// </summary>
        /// <param name="answer">The text of the answer</param>
        /// <returns>If the answer was right or wrong</returns>
        public bool AnswerQuestion(string answer)
        {
            return trivia.AnswerQuestion(answer);
        }

        /// <summary>
        /// Checks if the player won in a trivia game.
        /// If the player did then more internal methods would be called
        /// </summary>
        /// <returns></returns>
        public TriviaState DidPlayerWin()
        {
            if (trivia.DidTheyWin() == TriviaState.Won)
            {
                switch (triviaType)
                {
                    case TriviaType.Arrows:
                        gameLocations.AddArrows();
                        break;

                    case TriviaType.Pit:
                        gameLocations.ResolvePits();
                        break;

                    case TriviaType.Wumpus:
                        gameLocations.MoveWumpusFromPlayer();
                        break;
                }
                gameState = States.Wandering;
            }

            return trivia.DidTheyWin();
        }

        /// <summary>
        /// Gets a random hint
        /// </summary>
        /// <returns>Text of the hint</returns>
        public string GetHint()
        {
            return trivia.GiveRandomHint();
        }
        #endregion

        #region Combat
        /// <summary>
        /// Asks GameLocations to shoot an arrow in the selected direction
        /// </summary>
        /// <param name="doorNumber">The direction that the player chose to shoot through</param>
        /// <returns>If the arrow hit or not</returns>
        public bool ShootArrow(char doorNumber)
        {
            int direction = int.Parse("" + doorNumber);
            return gameLocations.ShootArrow(direction);
        }

        /// <summary>
        /// Gets inventory of the player
        /// </summary>
        /// <returns>List of ints representing the inventory</returns>
        public int[] GetPlayerInfo()
        {
            return gameLocations.GetPlayerInfo();
        }
        #endregion

        #region Highscore

        /// <summary>
        /// Gets the list of current highscores
        /// </summary>
        /// <returns>List of HighScoreInfos that represent each highscore</returns>
        public List<HighScoreInfo> GetHighscores()
        {
            return highScore.GetHighScoreList();
        }

        /// <summary>
        /// Adds a new Highscore to our data
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <param name="wumpusKilled">If the wumpus was killed or not</param>
        public void AddHighscore(string name, bool wumpusKilled)
        {
            int[] values = gameLocations.GetPlayerInfo();
            highScore.NewHighscore(name, caveLayout.ToString(), gameLocations.GetScore(wumpusKilled), values[1], values[2]);
        }
        #endregion

        /// <summary>
        /// Gets the current state of the player
        /// </summary>
        /// <returns>Enum representing the state</returns>
        public States GetState()
        {
            return gameState;
        }

        /// <summary>
        /// resets the hex game
        /// </summary>
        public void ClientDoneWithHexGame()
        {
            gameState = States.Wandering;
        }
    }
}