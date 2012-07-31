using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.Drawing;
using NHibernate;
using Emulator.Maps;
using Emulator.ClassMaps;

namespace Emulator
{
    public class World
    {
        public const int PLAYER_SIGHT_RANGE = 500;
        /// <summary>
        /// Pool of available World IDs
        /// </summary>
        private static Stack<UInt32> IdentPool;
        /// <summary>
        /// Loaded maps.
        /// </summary>
        private static Map[,] _maps;

        /// <summary>
        /// Signals whether the world has loaded successfully or not.
        /// </summary>
        public static bool done;

        /// <summary>
        /// Executes on startup.
        /// </summary>
        static World()
        {
            IdentPool = new Stack<UInt32>();

            for(ushort i = ushort.MaxValue; i > 0; i--) {
                IdentPool.Push(i);
            }
        }
        
        /// <summary>
        /// Load all NPCs
        /// </summary>
        public static void LoadNPCs()
        {
            done = false;
            using(ISession session = Server.Factory.OpenSession()) 
            {
                foreach(Npc npc in session.CreateCriteria(typeof(Npc)).List<Npc>()) 
                {
                    Server.WorldNPCs[npc.WorldId] = npc;
                } 
            }
            done = true;
        }

        /// <summary>
        /// Loads all the maps.
        /// </summary>
        public static void LoadMaps()
        {
            _maps = new Map[50, 50];
            using(XmlTextReader xtr = new XmlTextReader("Maps.xml"))
            {
                done = true;
                string mapName;
                int posX;
                int posY;

                while(xtr.Read())
                {
                    if(xtr.NodeType == XmlNodeType.Element)
                    if(xtr.Name == "Map")
                    {
                        mapName = xtr.GetAttribute("Src");
                        Utils.XmlNextElement(xtr);

                        if(xtr.Name == "Position")
                        {
                            try
                            {
                                posX = Int32.Parse(xtr.GetAttribute("PosX"));
                                posY = Int32.Parse(xtr.GetAttribute("PosY"));
                                _maps[posX, posY] = new Map( mapName );
                            }
                            catch(FormatException)
                            {
                                ServerConsole.WriteLine(
                                    System.Drawing.Color.Red,
                                    "Invalid position encountered while loading maps. [{0}]", 
                                    xtr.LineNumber);
                                done = false;
                            }
                            catch(FileNotFoundException)
                            {
                                ServerConsole.WriteLine(
                                    System.Drawing.Color.Red,
                                    "An error was encountered while loading a map. [{0}]", 
                                    xtr.LineNumber);
                                done = false;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds a player into the world.
        /// </summary>
        /// <param name="Player">Player object.</param>
        public static void AddPlayer(WorldPlayer player)
        {
            int mapX = player.PosX / 8192;
            int mapY = player.PosY / 8192;

            int tileX = ((player.PosX - (mapX * 8192)) / 32);
            int tileY = ((player.PosY - (mapY * 8192)) / 32);

            _maps[mapX, mapY].AddPlayer(tileX, tileY, player);
        }

        /// <summary>
        /// Updates a player's position on the map when moving.
        /// </summary>
        /// <param name="Player">Player object.</param>
        /// <param name="Dx">Movement on X axis</param>
        /// <param name="Dy">Movement on Y axis</param>
        public static void UpdatePlayer(WorldPlayer player, sbyte dx, sbyte dy)
        {
            int mapX = player.PosX / 8192;
            int mapY = player.PosY / 8192;

            int tileX = ((player.PosX - (mapX * 8192)) / 32);
            int tileY = ((player.PosY - (mapY * 8192)) / 32);

            int mapX_ = (player.PosX - dx) / 8192;
            int tapY_ = (player.PosY - dy) / 8192;

            int tileX_ = (((player.PosX - dx) - (mapX * 8192)) / 32);
            int tileY_ = (((player.PosY - dy) - (mapY * 8192)) / 32);

            if(mapX == mapX_ && mapY == tapY_)
            {
                if(tileX == tileX_ && tileY == tileY_) {
                    return;
                }

                byte zoneX = (byte)(tileX / 32);
                byte zoneY = (byte)(tileY / 32);

                byte zoneX_ = (byte)(tileX_ / 32);
                byte zoneY_ = (byte)(tileY_ / 32);

                if(zoneX == zoneX_ && zoneY == zoneY_) {
                    return;
                }

                _maps[mapX, mapY].RemovePlayer(tileX_, tileY_, player);
                _maps[mapX, mapY].AddPlayer(tileX, tileY, player);

                return;
            }

            _maps[mapX_, tapY_].RemovePlayer(tileX_, tileY_, player);
            _maps[mapX, mapY].AddPlayer(tileX, tileY, player);
        }

        /// <summary>
        /// Gets the distance between Object1 and Object2
        /// </summary>
        /// <param name="Object1">An object in the world.</param>
        /// <param name="Object2">An object in the world.</param>
        /// <returns>The distance between the two objects.</returns>
        public static double GetDistance(WorldObject object1, WorldObject object2)
        {
            double distX = (double)(object1.PosX - object2.PosX);
            double distY = (double)(object1.PosY - object2.PosY);

            distX *= distX;
            distY *= distY;
            return Math.Sqrt(distX + distY);
        }

        public static List<Character> GetPlayersInArea(WorldPlayer player)
        {
            int mapX = player.PosX / 8192;
            int mapY = player.PosY / 8192;

            int tileX = ((player.PosX - (mapX * 8192)) / 32);
            int tileY = ((player.PosY - (mapY * 8192)) / 32);

            return _maps[mapX, mapY].GetPlayers(tileX,tileY,player );
        }

        /// <summary>
        /// Retrieves an unused world ID.
        /// </summary>
        /// <returns>Returns an unused world ID.</returns>
        public static uint NewId()
        {
            return IdentPool.Pop();
        }

        /// <summary>
        /// Free a currently used ID.
        /// </summary>
        /// <param name="ID">The ID not needed anymore.</param>
        public static void FreeId(uint ID)
        {
            IdentPool.Push(ID);
        }
    }
}
