using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Ryan Lawler

namespace WumpusEngine
{
    class GameLocations
    {
        private int bat1RoomNum;
        private int bat2RoomNum;
        private int playerRoomNum;
        private int wumpusRoomNum;
        private int pit1RoomNum;
        private int pit2RoomNum;
        private int startingPlayerRoom;
        private int coinsCollected;
        PlayerObject player;
        Cave cave;
        Random rand;
        /// <summary>
        /// Constructer for initialization of GameLocations object
        /// </summary>
        /// <param name="caveChosen">number of the cave the player chooses</param>
        /// <param name="random">Random object for good generation</param>
        public GameLocations(int caveChosen, Random random)
        {
            //initialize pertinant fields
            rand = random;
            player = new PlayerObject();
            cave = new Cave(caveChosen);
            coinsCollected = 0;
            //create a list of available spawning rooms
            List<int> numbers = new List<int>();
            for (int i = 0; i < 30; i++)
                numbers.Add(i);
            //For the player and every hazard, set their location to a random room and remove that room from the list
            playerRoomNum = numbers[rand.Next(0, numbers.Count)];
            startingPlayerRoom = playerRoomNum;
            numbers.Remove(playerRoomNum);
            //Put the wumpus in a random, non-player room
            wumpusRoomNum = numbers[rand.Next(0, numbers.Count)];
            bat1RoomNum = numbers[rand.Next(0, numbers.Count)];
            numbers.Remove(bat1RoomNum);
            bat2RoomNum = numbers[rand.Next(0, numbers.Count)];
            numbers.Remove(bat2RoomNum);
            pit1RoomNum = numbers[rand.Next(0, numbers.Count)];
            numbers.Remove(pit1RoomNum);
            pit2RoomNum = numbers[rand.Next(0, numbers.Count)];
            numbers.Remove(pit2RoomNum);
        }

        /// <summary>
        /// Decrements arrow count and returns whether the arrow hits
        /// </summary>
        /// <param name="direction">The room number the player is shooting at</param>
        /// <returns>Whether the arrow hits</returns>
        public bool ShootArrow(int direction)
        {
            //Make sure direction is valid
            bool invalidDirection = true;
            for(int i = 0; i < cave.PassRoom(playerRoomNum).TunnelLocations().Length; i++)
            {
                if(cave.PassRoom(playerRoomNum).TunnelLocations()[i] == direction)
                {
                    invalidDirection = false;
                }
            }
            if(invalidDirection)
            {
                return false;
            }
            //Tell the player object to decrement the arrow count
            //Test if the room number being shot is equal to the room number of the wumpus and return true if it is (return false otherwise)
            player.SubtractArrow();
            if(cave.PassRoom(playerRoomNum).GetTunnelInfo(direction).GetToRoom() - 1 == wumpusRoomNum)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < cave.PassRoom(playerRoomNum).ToRoomLocations().Length; i++)
                {
                    if (cave.PassRoom(playerRoomNum).ToRoomLocations()[i] - 1 == wumpusRoomNum)
                    {
                        int wumpusDirection = rand.Next(0, 6);
                        int[] doors = cave.PassRoom(wumpusRoomNum).TunnelLocations();
                        if (new List<int>(doors).Contains(wumpusDirection))
                        {
                            wumpusRoomNum = cave.PassRoom(wumpusRoomNum).GetTunnelInfo(wumpusDirection).GetToRoom() - 1;
                        }
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// Moves wumpus from 2 to 4 rooms away from player (used when it is beaten in a fight)
        /// </summary>
        public void MoveWumpusFromPlayer ()
        {
            int roomsRun = rand.Next(2,4);
            int nextDirection = 0;
            int startingRoom = wumpusRoomNum;
            //make the first move into a room that isn't a dead end
            nextDirection = cave.PassRoom(wumpusRoomNum).TunnelLocations()[rand.Next(0, cave.PassRoom(wumpusRoomNum).TunnelLocations().Length)];
            while (cave.PassRoom(cave.PassRoom(wumpusRoomNum).GetTunnelInfo(nextDirection).GetToRoom() - 1).TunnelLocations().Length == 1)
            {
                nextDirection = cave.PassRoom(wumpusRoomNum).TunnelLocations()[rand.Next(0, cave.PassRoom(wumpusRoomNum).TunnelLocations().Length)];
            }
            wumpusRoomNum = cave.PassRoom(wumpusRoomNum).GetTunnelInfo(nextDirection).GetToRoom() - 1;
            //keep moving until the wumpus reaches the length required, and make sure not to run back into the starting room
            for (int i = 1; i < roomsRun; i++)
            {
                nextDirection = cave.PassRoom(wumpusRoomNum).TunnelLocations()[rand.Next(0, cave.PassRoom(wumpusRoomNum).TunnelLocations().Length)];
                while (cave.PassRoom(wumpusRoomNum).GetTunnelInfo(nextDirection).GetToRoom() - 1 == startingRoom)
                {
                    nextDirection = cave.PassRoom(wumpusRoomNum).TunnelLocations()[rand.Next(0, cave.PassRoom(wumpusRoomNum).TunnelLocations().Length)];
                }
                wumpusRoomNum = cave.PassRoom(wumpusRoomNum).GetTunnelInfo(nextDirection).GetToRoom() - 1;
            }

        }
        /// <summary>
        /// Adds 2 arrows
        /// </summary>
        public void AddArrows()
        {
            //Pretty darn obvious
            player.Add2Arrows();
        }

        /// <summary>
        /// Returns the hazard messages needed to display based on hazards nearby the player
        /// </summary>
        /// <returns>The warning messages</returns>
        public List<string> GetWarningMessages ()
        {
            //Checks the proximity of the player to various hazards and adds to the return list accordingly
            List<string> message = new List<string>();
            for(int i = 0; i < cave.PassRoom(playerRoomNum).ToRoomLocations().Length; i++)
            {
                if (cave.PassRoom(playerRoomNum).ToRoomLocations()[i] - 1 == bat1RoomNum || cave.PassRoom(playerRoomNum).ToRoomLocations()[i] - 1 == bat2RoomNum)
                {
                    message.Add("Bats Nearby");
                }
                if (cave.PassRoom(playerRoomNum).ToRoomLocations()[i] - 1 == pit1RoomNum || cave.PassRoom(playerRoomNum).ToRoomLocations()[i] - 1 == pit2RoomNum)
                {
                    message.Add("I feel a draft");
                }
                if (cave.PassRoom(playerRoomNum).ToRoomLocations()[i] - 1 == wumpusRoomNum)
                {
                    message.Add("I smell a wumpus");
                }
            }
            return message;
        }

        /// <summary>
        /// Moves the player through the given direction number if it is valid, and adds a coin if there are any left to be found
        /// </summary>
        /// <param name="directionNum">Number of direction to move through</param>
        public void ChangePlayerLocation (int directionNum)
        {
            for (int i = 0; i < cave.PassRoom(playerRoomNum).TunnelLocations().Length; i++)
            {
                //Make sure the direction is valid
                if (cave.PassRoom(playerRoomNum).TunnelLocations()[i] == directionNum)
                {
                    //If the player hasn't found all the coins, give them one
                    if (coinsCollected < 100)
                    {
                        player.AddCoins(1);
                        coinsCollected++;
                    }
                    //Move the player in the direction
                    playerRoomNum = cave.PassRoom(playerRoomNum).GetTunnelInfo(directionNum).GetToRoom() - 1;
                    return;
                }
            }
        }
        /// <summary>
        /// Get the hazards in the player's current room, and moves the according bat swarm if there is one
        /// </summary>
        /// <returns>a bool array of whether or not hazards are in the room[bat, pit, wumpus]</returns>
        public bool[] GetHazardsInPlayersRoom()
        {
            bool[] hazards = new bool[3];
            if (playerRoomNum == wumpusRoomNum)
            {
                hazards[2] = true;
            }
            if (playerRoomNum == bat1RoomNum || playerRoomNum == bat2RoomNum)
            {
                hazards[0] = true;
            }
            if (playerRoomNum == pit1RoomNum || playerRoomNum == pit2RoomNum)
            {
                hazards[1] = true;
            }
            return hazards;
        }
        /// <summary>
        /// Moves bats and player to random room
        /// </summary>
        public void ResolveBats()
        {
            //Create a list of available rooms
            List<int> numbers = new List<int>();
            for (int i = 0; i < 30; i++)
                numbers.Add(i);
            //Move the player to a new random space, and make the new space invalid
            numbers.Remove(playerRoomNum);
            int oldPlayerRoomNum = playerRoomNum;
            playerRoomNum = numbers[rand.Next(0, numbers.Count)];
            numbers.Remove(playerRoomNum);
            numbers.Remove(pit1RoomNum);
            numbers.Remove(pit2RoomNum);
            numbers.Remove(bat2RoomNum);
            numbers.Remove(bat1RoomNum);
            //Move the appropriate bat swarm to a random location not already occupied
            if (bat1RoomNum == oldPlayerRoomNum)
            {

                bat1RoomNum = numbers[rand.Next(0, numbers.Count)];
            }
            else
            {
                numbers.Remove(bat1RoomNum);
                bat2RoomNum = numbers[rand.Next(0, numbers.Count)];
            }
        }
        /// <summary>
        /// Moves player to starting room
        /// </summary>
        public void ResolvePits()
        {
            playerRoomNum = startingPlayerRoom;
        }
        /// <summary>
        /// Returns player score from player object (under construction)
        /// </summary>
        /// <param name="wumpusKilled"></param>
        /// <returns>The score</returns>
        public int GetScore (bool wumpusKilled)
        {
            return player.CalculateScore(wumpusKilled);
        }
        /// <summary>
        /// Changes the player to the desired room number
        /// </summary>
        /// <param name="roomNum">roon number to move to</param>
        public void Teleport(int roomNum)
        {
            playerRoomNum = roomNum;
        }

        /// <summary>
        /// Changes the number of coins by the desired number
        /// </summary>
        /// <param name="num"></param>
        public void ChangeCoins(int num)
        {
            player.AddCoins(num);
        }

        /// <summary>
        /// Returns a room object for the player's room
        /// </summary>
        /// <returns>Room object for the player's room</returns>
        public Room GetPlayerRoomInfo()
        {
            return cave.PassRoom(playerRoomNum);
        }

        /// <summary>
        /// Gets the player's turn number, arrow number, and coin number, and returns them in an int array
        /// </summary>
        /// <returns>Player info [turn num, arrow, coin num]</returns>
        public int[] GetPlayerInfo ()
        {
            int[] info = new int[3];
            info[0] = player.GetTurnNum();
            info[1] = player.GetArrowNum();
            info[2] = player.GetCoinNum();
            return info;
        }

        /// <summary>
        /// Gets the room numbers of the hazards, wumpus, and player in an int array
        /// </summary>
        /// <returns>Room numbers [player, wumpus, bat1, bat2, pit1, pit2]</returns>
        public int[] GetLocationInfo()
        {
            int[] info = new int[6];
            info[0] = playerRoomNum;
            info[1] = wumpusRoomNum;
            info[2] = bat1RoomNum;
            info[3] = bat2RoomNum;
            info[4] = pit1RoomNum;
            info[5] = pit2RoomNum;
            return info;
        }
    }
}
