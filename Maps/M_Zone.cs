using System;
using System.Collections.Generic;

namespace Emulator.Maps
{
    public class Zone
    {
        /// <summary>
        /// Grid of squares on a KSM
        /// </summary>
        private byte[,] _grid;

        /// <summary>
        /// Get or set the value of a square
        /// </summary>
        /// <param name="x">Position on X axis</param>
        /// <param name="y">Position on Y axis</param>
        /// <returns></returns>
        public byte this[int x, int y]
        {
            get { return _grid[x, y];  }
            set { _grid[x, y] = value; }
        }

        /// <summary>
        /// List of players in this zone
        /// </summary>
        public Dictionary<uint,WorldPlayer> Players;
        
        /// <summary>
        /// List of monsters in this zone
        /// </summary>
        public Dictionary<uint,WorldObject> Monsters;

        /// <summary>
        /// List of people in this zone.
        /// </summary>
        public Dictionary<uint,WorldPerson>  People;
        
        /// <summary>
        /// Initializes a new zone, with list of people/monsters/objects in.
        /// </summary>
        public Zone()
        {
            _grid = new byte[32, 32];
            
            this.Players  = new Dictionary<uint,WorldPlayer>();
            this.People   = new Dictionary<uint,WorldPerson>();
            this.Monsters = new Dictionary<uint,WorldObject>();
        }
    }

    public enum TileType
    {
        Assasin = 1,
        War,
        Field,
        Portal
    }
}
