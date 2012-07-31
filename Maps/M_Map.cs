using System;
using System.IO;
using System.Collections.Generic;

namespace Emulator.Maps
{
    public class Map
    {
        /// <summary>
        /// Map file version.
        /// </summary>
        private int _version;

        /// <summary>
        /// Gets the map file version.
        /// </summary>
        public int Version
        {
            get { return _version; }
        }
        
        /// <summary>
        /// Grid of tiles.
        /// </summary>
        private Zone[,] _grid;

        /// <summary>
        /// Gets the tile at the specified X and Y axis.
        /// </summary>
        /// <param name="X">Position on the X axis</param>
        /// <param name="Y">Position on the Y axis.</param>
        /// <returns></returns>
        public Zone this[byte X, byte Y]
        {
            get
            {
                return _grid[X, Y];
            }
        }

        /// <summary>
        /// Creates a Map object from file.
        /// </summary>
        /// <param name="File">The map file to create the object from.</param>
        public Map(string filename)
        {
            using(FileStream stream = new FileStream(filename,FileMode.Open)) 
            using(BinaryReader reader = new BinaryReader(stream)) 
            {
                short type;
                _grid = new Zone[8, 8];

                _version = reader.ReadInt32();

                for(int zy = 0; zy < 8; zy++)
                for(int zx = 0; zx < 8; zx++)
                for(int y = 0; y < 32; y++)
                for(int x = 0; x < 32; x++)
                {
                    type = 0;

                    if(reader.ReadInt16() > 0) {
                        type &= 0x20;
                    }
                    type &= reader.ReadInt16();

                    _grid[zx, zy] = new Zone();
                    _grid[zx, zy][x, y] = (byte)type;
                }
            }
        }

        /// <summary>
        /// Registers a player on the map.
        /// </summary>
        /// <param name="TileX">Position on X axis</param>
        /// <param name="TileY">Position on Y axis</param>
        /// <param name="Player">Player object.</param>
        public void AddPlayer(int tileX, int tileY, WorldPlayer player)
        {
            byte zoneX = (byte)(tileX / 32);
            byte zoneY = (byte)(tileY / 32);

            _grid[zoneX,zoneY].Players[player.WorldID] = player;
        }

        /// <summary>
        /// Removes a plaer on the map.
        /// </summary>
        /// <param name="TileX">Position on X axis</param>
        /// <param name="TileY">Position on Y axis</param>
        /// <param name="Player">Player object.</param>
        public void RemovePlayer( int tileX, int tileY, WorldPlayer player )
        {
            byte zoneX = (byte)(tileX / 32);
            byte zoneY = (byte)(tileY / 32);

            _grid[zoneX,zoneY].Players.Remove(player.WorldID);
        }

        /// <summary>
        /// Gets all the players in surrounding zones.
        /// </summary>
        /// <param name="TileX">Position on X axis</param>
        /// <param name="TileY">Position on Y axis</param>
        /// <param name="Player">Player to base search on.</param>
        /// <returns>A list of players around the base player.</returns>
        public List<Character> GetPlayers(int tileX, int tileY, WorldPlayer basePlayer)
        {
            byte zoneX = (byte)(tileX / 32);
            byte zoneY = (byte)(tileY / 32);

            List<Character> playersInRange = new List<Character>();
            
            foreach(WorldPlayer playerObj in _grid[zoneX, zoneY].Players.Values)
            {
                if (playerObj!= basePlayer)
                {
                    if(World.GetDistance(playerObj, basePlayer) < World.PLAYER_SIGHT_RANGE) {
                        playersInRange.Add((Character)playerObj);
                    }
                }
            }

            foreach(WorldPlayer playerObj in _grid[zoneX + 1, zoneY].Players.Values)
            {
                if (playerObj!= basePlayer)
                {
                    if(World.GetDistance(playerObj, basePlayer) < World.PLAYER_SIGHT_RANGE) {
                        playersInRange.Add((Character)playerObj);
                    }
                }
            }

            foreach(WorldPlayer playerObj in _grid[zoneX, zoneY + 1].Players.Values)
            {
                if (playerObj!= basePlayer)
                {
                    if(World.GetDistance(playerObj, basePlayer) < World.PLAYER_SIGHT_RANGE) {
                        playersInRange.Add((Character)playerObj);
                    }
                }
            }

            foreach(WorldPlayer playerObj in _grid[zoneX -1 , zoneY].Players.Values)
            {
                if (playerObj!= basePlayer)
                {
                    if(World.GetDistance(playerObj, basePlayer) < World.PLAYER_SIGHT_RANGE) {
                        playersInRange.Add((Character)playerObj);
                    }
                }
            }

            foreach(WorldPlayer playerObj in _grid[zoneX, zoneY - 1].Players.Values)
            {
                if (playerObj!= basePlayer)
                {
                    if(World.GetDistance(playerObj, basePlayer) < World.PLAYER_SIGHT_RANGE) {
                        playersInRange.Add((Character)playerObj);
                    }
                }
            }

            return playersInRange;
        }

        /// <summary>
        /// Checks if the tile is the specified type.
        /// </summary>
        /// <param name="TileType">Type of tile to check for.</param>
        /// <returns>Whether or not the tile is the specified type.</returns>
        public bool TileIsType(int tileX, int tileY, TileType tileType)
        {
            byte zoneX = (byte)(tileX / 32);
            byte zoneY = (byte)(tileY / 32);
            
            tileX = (tileX - (zoneX * 32));
            tileY = (tileY - (zoneY * 32));

            switch(tileType)
            {
                case TileType.Assasin:
                    return !((_grid[zoneX, zoneY][tileX, tileY] & 0x02) > 0 ||
                             (_grid[zoneX, zoneY][tileX, tileY] & 0x04) > 0);
                case TileType.Field:
                    return !((_grid[zoneX, zoneY][tileX, tileY] & 0x20) > 0);
                case TileType.War:
                    return ((_grid[zoneX, zoneY][tileX, tileY] & 0x10) > 0);
                case TileType.Portal:
                    return ((_grid[zoneX, zoneY][tileX, tileY] & 0x01) > 0);
                default:
                    return false;
            }
        }
    }
}
