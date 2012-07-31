using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;
using NHibernate;

namespace Emulator 
{
    public partial class Client 
    {
        /// <summary>
        /// Creates a Character
        /// </summary>
        /// <param name="name">Character Name</param>
        /// <param name="classId">Class</param>
        /// <param name="strength">Strength</param>
        /// <param name="health">Health</param>
        /// <param name="intelligence">Intelligence</param>
        /// <param name="wisdom">Wisdom</param>
        /// <param name="agility">Agility</param>
        /// <param name="face">Face Style</param>
        /// <param name="hair">Hair Style</param>
        public int CreateCharacter(string name, byte classId, byte strength, byte health, byte intelligence, 
                                   byte wisdom, byte agility, byte face, byte hair)
        {
            ServerConsole.WriteLine("Creating character called {0} for {1}.", name, user.Username);
            
            ///
            /// Configure the stats correctly foreach class
            /// 
            switch(classId)
            {
                case 0: 
                    strength     += 18;
                    health       += 16;
                    intelligence += 8;
                    wisdom       += 8;
                    agility      += 10;
                break;
                case 1:
                    strength     += 8;
                    health       += 10;
                    intelligence += 18;
                    wisdom       += 16;
                    agility      += 8;
                break;
                case 2: 
                    strength     += 14;
                    health       += 10;
                    intelligence += 8;
                    wisdom       += 10;
                    agility      += 18;
                break;
            }
            
            ///
            /// Add random extra stats
            /// 
            for(int i=0; i<5; i++) 
            {
                switch(Server.ServerRandom.Next(0, 4)) 
                {
                    case 0: strength     += 1; break;
                    case 1: health       += 1; break;
                    case 2: intelligence += 1; break;
                    case 3: wisdom       += 1; break;
                    case 4: agility      += 1; break;
                }
            }
            
            ///
            /// Character Creation
            /// 
            Player p;
            using(session = Server.Factory.OpenSession())
            using(ITransaction transaction = session.BeginTransaction()) 
            {
                ///
                /// Create Player
                /// 
                p   = new Player();
                p.UserId   = user.UserId;
                p.Status   = 1; 
                p.Name     = name;
                p.ClassId  = (int)classId;
                p.Level    = 1;
                p.Job      = 0;
                p.Strength = (int)strength;
                p.Health   = (int)health;
                p.Intelligence = (int)intelligence;
                p.Wisdom   = (int)wisdom;
                p.Agility  = (int)agility;
                p.Face     = (int)face;
                p.Hair     = (int)hair;
                p.X        = 257491;
                p.Y        = 258584;
                p.Z        = 16120;
                
                session.Save(p);
                transaction.Commit();
            }
            
            return p.PlayerId;
        }
    }
}
