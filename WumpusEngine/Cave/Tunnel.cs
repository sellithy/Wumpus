using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WumpusEngine
{
    /// <summary>
    /// the direction of a single tunnel and what room it leads to.
    /// </summary>
    public class Tunnel
    {
        private int direction;
        private int toRoom;
        /// <summary>
        /// makes and gets the directions and what room they go to
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="toRoom"></param>
        public Tunnel(int direction, int toRoom)
        {
            this.direction = direction;
            this.toRoom = toRoom;
        }
        /// <summary>
        /// returns direction info
        /// </summary>
        /// <returns>directions</returns>
        public int GetDirection()
        {
            return this.direction;
        }
        /// <summary>
        /// what rooms are connected
        /// </summary>
        /// <returns>toRoom</returns>
        public int GetToRoom()
        {
            return this.toRoom;
        }
     }
}
