using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WumpusEngine
{
    /// <summary>
    /// for single string or line of file that is a room
    /// </summary>
    public class Room
    {
        private Tunnel[] tunnels;
        private int whatroom;

        /// <summary>
        /// constructor.
        /// </summary>
        public Room(string roomDetails)
        {
            string[] tokens = roomDetails.Split();
            int HowManyTunnels = int.Parse(tokens[1]);
            this.tunnels = new Tunnel[HowManyTunnels];
            for (int i = 0; i < HowManyTunnels; i++)
            {
                tunnels[i] = new Tunnel(int.Parse(tokens[i + 2]), int.Parse(tokens[i + 5]));
            }
            whatroom = int.Parse(tokens[0]);

        }

        //needs to have a arary to store direction of the room and what room it connects to.
        //can be anywhere from 1 to 3 tunnels.
        //stubs needed are where are the tunnels room.tunnellocations
        /// <summary>
        /// returns tunnel locations for room
        /// </summary>
        public int[] TunnelLocations()
        {
            int[] tunnellocation = new int[tunnels.Length];
            for (int i = 0; i < tunnels.Length; i++)
            {
                tunnellocation[i] = this.tunnels[i].GetDirection();
            }
            return tunnellocation;
        }
        /// <summary>
        /// makes an array for tunnel locations and returns the toroomlocations
        /// </summary>
        /// <returns></returns>
        public int[] ToRoomLocations()
        {
            int[] toroomlocations = new int[tunnels.Length];
            for (int i = 0; i < tunnels.Length; i++)
            {
                toroomlocations[i] = this.tunnels[i].GetToRoom();
            }
            return toroomlocations;
        }
        /// <summary>
        /// returns room number for this room
        /// </summary>
        /// <returns>room number</returns>
        public int RoomNumber()
        {
            return this.whatroom;
        }
        /// <summary>
        /// an array ro tunnels returns the array
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Tunnel GetTunnelInfo(int direction)
        {
            
            for (int i = 0; i < tunnels.Length; i++)
            {
                if (tunnels[i].GetDirection()==direction)
                {
                    return tunnels[i];
                }
            }
                return this.tunnels[0];
        }
    }
}
