using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WumpusEngine;

namespace WumpusEngineTester.GameLocations_and_Player
{
    [TestClass]
    public class GameLocationTest
    {
        [TestMethod]
        public void TestConstructer()
        {
            //Make a new gameLocations for every map and make sure it works
            for(int i = 1; i <= 5; i++)
            {
                GameLocations gameLocations = new GameLocations(i, new Random());
            }
        }
        [TestMethod]
        public void TestHazardLocations()
        {
            for (int i = 0; i < 1000; i++)
            {
                GameLocations gameLocations = new GameLocations(1, new Random(i));
                for(int j = 0; j < 6; j++)
                {
                    //Make sure everything is in a valid room
                    Assert.IsTrue(gameLocations.GetLocationInfo()[j] <= 29 && gameLocations.GetLocationInfo()[j] >= 0);
                    if(j == 0)
                    {
                        //Make sure nothing is in the player's room
                        for(int k = 1; k < 6; k++)
                        {
                            Assert.IsFalse(gameLocations.GetLocationInfo()[j] == gameLocations.GetLocationInfo()[k]);
                        }
                    }
                    else
                    {
                        if (j != 1)
                        {
                            //Make sure all hazards are in a different room
                            for (int k = 2; k < 6; k++)
                            {
                                if(k != j)
                                {
                                    Assert.IsFalse(gameLocations.GetLocationInfo()[j] == gameLocations.GetLocationInfo()[k]);
                                }
                            }
                        }
                    }
                }
            }
        }
        [TestMethod]
        public void TestWarningMessages ()
        {
            GameLocations gameLocations = new GameLocations(1, new Random(1));
            //Move to every hazard in the cave and the wumpus, move one room off, and make sure the warning message contains what it should
            for (int i = 1; i < 6; i++)
            {
                gameLocations.Teleport(gameLocations.GetLocationInfo()[i]);
                gameLocations.ChangePlayerLocation(gameLocations.GetPlayerRoomInfo().TunnelLocations()[0]);
                if (i == 1)
                {
                    Assert.IsTrue(gameLocations.GetWarningMessages().Contains("I smell a wumpus"));
                    Debug.WriteLine(1);
                }
                else
                {
                    if (i <= 3 && i >= 2)
                    {
                        Assert.IsTrue(gameLocations.GetWarningMessages().Contains("Bats Nearby"));
                    }
                    else
                    {
                        Assert.IsTrue(gameLocations.GetWarningMessages().Contains("I feel a draft"));
                        Debug.WriteLine(1);
                    }

                }
            }
        }
        [TestMethod]
        public void TestMovement ()
        {
            GameLocations gameLocations = new GameLocations(1, new Random());
            //In every room, try to walk through every direction, and make sure it leads to the right place
            for (int i = 0; i < 30; i++)
            {
                gameLocations.Teleport(i);
                for (int j = 0; j < 5; j++)
                {
                    int expected = expected = gameLocations.GetLocationInfo()[0];
                    for (int k = 0; k < gameLocations.GetPlayerRoomInfo().TunnelLocations().Length; k++)
                    {
                        if (gameLocations.GetPlayerRoomInfo().TunnelLocations()[k] == j)
                        {
                            expected = gameLocations.GetPlayerRoomInfo().GetTunnelInfo(j).GetToRoom() - 1;
                        }
                    }
                    gameLocations.ChangePlayerLocation(j);
                    Assert.AreEqual(expected, gameLocations.GetLocationInfo()[0]);
                    gameLocations.Teleport(i);
                }
            }
        }
        [TestMethod]
        public void TestShootArrow()
        {
            GameLocations gameLocations = new GameLocations(1, new Random());
            //Move to a room adjacent to the wumpus and machine-gun a non-wumpus room until it moves
            gameLocations.Teleport(gameLocations.GetLocationInfo()[1]);
            int oldWumpusRoom = gameLocations.GetLocationInfo()[1];
            gameLocations.ChangePlayerLocation(gameLocations.GetPlayerRoomInfo().TunnelLocations()[0]);
            for (int k = 0; k < gameLocations.GetPlayerRoomInfo().ToRoomLocations().Length; k++)
            {
                if (gameLocations.GetPlayerRoomInfo().ToRoomLocations()[k] - 1 != gameLocations.GetLocationInfo()[1])
                {
                    while (oldWumpusRoom == gameLocations.GetLocationInfo()[1])
                    {
                        gameLocations.ShootArrow(gameLocations.GetPlayerRoomInfo().TunnelLocations()[k]);
                    }
                    k = gameLocations.GetPlayerRoomInfo().ToRoomLocations().Length;
                    Assert.AreNotEqual(oldWumpusRoom, gameLocations.GetLocationInfo()[1]);
                }
            }
            //Machine-gun the wumpus and make sure it dosen't move
            gameLocations.Teleport(gameLocations.GetLocationInfo()[1]);
            oldWumpusRoom = gameLocations.GetLocationInfo()[1];
            gameLocations.ChangePlayerLocation(gameLocations.GetPlayerRoomInfo().GetTunnelInfo(gameLocations.GetPlayerRoomInfo().TunnelLocations()[0]).GetDirection());
            for (int k = 0; k < gameLocations.GetPlayerRoomInfo().ToRoomLocations().Length; k++)
            {
                if (gameLocations.GetPlayerRoomInfo().ToRoomLocations()[k] - 1 == gameLocations.GetLocationInfo()[1])
                {
                    for (int i = 0; i < 1000; i++)
                        gameLocations.ShootArrow(gameLocations.GetPlayerRoomInfo().TunnelLocations()[k]);
                    k = gameLocations.GetPlayerRoomInfo().ToRoomLocations().Length;
                    Assert.AreEqual(oldWumpusRoom, gameLocations.GetLocationInfo()[1]);
                }
            }
        }
        [TestMethod]
        public void TestWumpusMovement()
        {
            //Call the moveWumpusFromPlayer command and make sure the Wumpus isn't in the same room
            for (int i = 0; i < 10000; i++)
            {
                GameLocations gameLocations = new GameLocations(1, new Random(i));
                int oldWumpusLocation = gameLocations.GetLocationInfo()[1];
                gameLocations.MoveWumpusFromPlayer();
                Assert.AreNotEqual(oldWumpusLocation, gameLocations.GetLocationInfo()[1]);
            }
        }
        [TestMethod]
        public void TestBats()
        {
            //For each bat swarm, move the player to it, resolve the encounter, and make sure the ending positions are within the correct bounds
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    GameLocations gameLocations = new GameLocations(1, new Random());
                    gameLocations.Teleport(gameLocations.GetLocationInfo()[2 + i]);
                    int oldPlayerLocation = gameLocations.GetLocationInfo()[0];
                    int oldBatLocation = gameLocations.GetLocationInfo()[2 + i];
                    gameLocations.ResolveBats();
                    Assert.AreNotEqual(oldPlayerLocation, gameLocations.GetLocationInfo()[0]);
                    Assert.AreNotEqual(gameLocations.GetLocationInfo()[0], gameLocations.GetLocationInfo()[2 + i]);
                    Assert.AreNotEqual(gameLocations.GetLocationInfo()[2 + i], oldBatLocation);
                    Assert.IsTrue(gameLocations.GetLocationInfo()[0] <= 29 && gameLocations.GetLocationInfo()[0] >= 0);
                    Assert.IsTrue(gameLocations.GetLocationInfo()[2 + i] <= 29 && gameLocations.GetLocationInfo()[2 + i] >= 0);
                }
            }
        }
        [TestMethod]
        public void TestPits()
        {
            //For each pit, move the player to it, resolve the encounter, and make sure the ending positions are within the correct bounds
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    GameLocations gameLocations = new GameLocations(1, new Random());
                    int startPlayerLocation = gameLocations.GetLocationInfo()[0];
                    gameLocations.Teleport(gameLocations.GetLocationInfo()[4 + i]);
                    gameLocations.ResolvePits();
                    Assert.AreEqual(startPlayerLocation, gameLocations.GetLocationInfo()[0]);
                    Assert.AreNotEqual(gameLocations.GetLocationInfo()[0], gameLocations.GetLocationInfo()[4 + i]);
                    Assert.IsTrue(gameLocations.GetLocationInfo()[0] <= 29 && gameLocations.GetLocationInfo()[0] >= 0);
                }
            }
        }
    }
}
