using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WumpusEngine
{
    public enum Layouts { Whiterun=1, DeapSpaceNine, Tatooine, MiddleEarth, DarkCastle };//cave names outside of cave
    /// <summary>
    /// cave is a whole file
    /// </summary>
    public class Cave
    {
        Room[] rooms;
        /// <summary>
        /// constructor
        /// </summary>
        public Cave(int whatCave)
        {
           
            StreamReader SelectedCave = new StreamReader("Cave\\CaveLayout"+whatCave+".txt");// to get the file
            SelectedCave.ReadLine(); // layout info for file - ignore.
            rooms = new Room[30];
           
            for (int i = 0; i < 30; i++)
            {
                rooms[i] = new Room(SelectedCave.ReadLine());
            }
           
        }
        /// <summary>
        /// gives other classes the info when asked
        /// </summary>
        public Room PassRoom(int whatRoom)
        {
            //when game control/map asks for room info give room info
            return this.rooms[whatRoom];
        }
    }
}
