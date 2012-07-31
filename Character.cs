using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;
using Emulator.Maps;
using NHibernate;

namespace Emulator 
{
    public class Character : WorldPlayer 
    {
        /// <summary>
        /// Resting
        /// </summary>
        public bool Resting = false;                                             
        
        /// <summary>
        /// Player Object, Mapped database table
        /// </summary>
        private Player _player;
        
        /// <summary>
        /// Player Skills
        /// </summary>
        private Dictionary<byte,PlayerSkill> _skills = new Dictionary<byte,PlayerSkill>();
        
        /// <summary>
        /// The Client Owner of the Character
        /// </summary>
        private Client _owner;
        
        /// <summary>
        /// NHibernate ISession
        /// </summary>
        private ISession _session;
        
        /// <summary>
        /// Character Stats 
        /// </summary>
        private CharacterStats _stats;

        /// <summary>
        /// Players in View Range
        /// </summary>
        private List<Character> _players_iv;
        
        /// <summary>
        /// Character Mortal Status
        /// </summary>
        private bool _alive = true;
        
        /// <summary>
        /// Player's Guild, Defaults to NULL if the player is not in a guild.
        /// </summary>
        private Guild _guild = null;
        
        /// <summary>
        /// Creates a new Character , containing Character data and action methods
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="playerId"></param>
        public Character(Client owner,int playerId)
        {
            // Client (Connection) Owner
            _owner   = owner;
            
            // NHibernate session
            _session = Server.Factory.OpenSession();
            
            // Fetches Character information
            _player  = (Player)_session.Get(typeof(Player),playerId);
            
            // Loading Skills
            IQuery q = _session.CreateQuery("FROM PlayerSkill WHERE PlayerId = :playerId");
                   q.SetParameter("playerId",playerId);
            foreach(PlayerSkill skill in q.List<PlayerSkill>()) 
            {
                _skills[(byte)skill.SkillIndex] = skill;
            }
                        
            // Setting Player Stats
            _stats = new Character.CharacterStats();
            _stats.Strength     = (byte)_player.Strength;
            _stats.Health       = (byte)_player.Health;
            _stats.Intelligence = (byte)_player.Intelligence;
            _stats.Wisdom       = (byte)_player.Wisdom;
            _stats.Agility      = (byte)_player.Agility;  
            _stats.MaximumHP    = (ushort)_player.HP;
            _stats.MaximumMP    = (ushort)_player.MP;
            
            // fake
            _stats.CurrentHP    = (ushort)_player.HP;
            _stats.CurrentMP    = (ushort)_player.MP;
                        
            // Setting WorldPlayer Coordinates
            _x = _player.X;
            _y = _player.Y;
            _z = _player.Z;
            
            // Setting WorldId and Players in View Range
            _worldID    = World.NewId();
            _players_iv = new List<Character>();
            
            // Show current players in range
            Respawn();
        }
        
        public Guild Guild {
            get { return _guild; }
        }
        
        /// <summary>
        /// Character Stats
        /// </summary>
        public CharacterStats Stats {
            get { return _stats; }
        }
        
        /// <summary>
        /// Character Skills
        /// </summary>
        public Dictionary<byte,PlayerSkill> Skills {
            get { return _skills; }
        }
        
        /// <summary>
        /// Get the Player Object, Mapped database table
        /// </summary>
        public Player Player {
            get { return _player; }
        }

        /// <summary>
        /// Returns the mortal status of the character
        /// </summary>
        public bool IsAlive {
            get { return _alive; }
            set { _alive = value; }
        }
        
        /// <summary>
        /// CharacterStats class, inherit from Base Stats
        /// </summary>
        public class CharacterStats : BaseStats {
            public BonusStats Bonus;
        }
        
        /// <summary>
        /// BonusStats class, inherit from Base Stats
        /// </summary>
        public class BonusStats : BaseStats {}
        
        /// <summary>
        /// BaseStats class, contains player stats
        /// </summary>
        public class BaseStats {
            public byte Strength;
            public byte Health;
            public byte Intelligence;
            public byte Wisdom;
            public byte Agility;
            
            public ushort CurrentHP;
            public ushort CurrentMP;
            public ushort MaximumHP;
            public ushort MaximumMP;
            
            public ushort OTP;
            public ushort Evasion;
            public ushort Defense;
            public byte Absorb;
            
            public ushort MinDamage;
            public ushort MaxDamage;
            public ushort MinMagicalDamage;
            public ushort MaxMagialDamage;
            
            public ushort CriticalChance;
            public ushort CriticalPercent;
            
            public ushort FireResistance;
            public ushort IceResistance;
            public ushort LightningResistance;
            public ushort CurseResistance;
            public ushort ParalysisResistance;                        
        }

        /// <summary>
        /// Respawns the character, updating line of sight
        /// </summary>
        private void Respawn()
        {
            while(_players_iv.Count > 0) {
                DeleteView(_players_iv[0]);
                _players_iv[0].DeleteView(this);
            }
            
            List<Character> temp = World.GetPlayersInArea( this );
            foreach(Character player in temp) {
                AddView(player);
                player.AddView(this);
            }
            temp = null;
        }

        /// <summary>
        /// Begins moving the character to the location given.
        /// </summary>
        /// <param name="Dx">Place to move the character to on the X axis.</param>
        /// <param name="Dy">Place to move the character to on the Y axis.</param>
        /// <param name="Dz">Place to move the character to on the Z axis.</param>
        public void Move(sbyte Dx, sbyte Dy, sbyte Dz)
        {
            _x += Dx;
            _y += Dy;
            _z += Dz;

            _owner.Send(new Packets.PlayerMove(_worldID, Dx, Dy, Dz));
            World.UpdatePlayer(this, Dx, Dy);
            Move();
        }

        /// <summary>
        /// Ends movement at the location given.
        /// </summary>
        /// <param name="Dx">Place to move the character to on the X axis.</param>
        /// <param name="Dy">Place to move the character to on the Y axis.</param>
        /// <param name="Dz">Place to move the character to on the Z axis.</param>
        public void MoveEnd(sbyte Dx, sbyte Dy, sbyte Dz)
        {
            _x += Dx;
            _y += Dy;
            _z += Dz;

            World.UpdatePlayer(this, Dx, Dy);
            Move();
        }

        /// <summary>
        /// Checks views after movement.
        /// </summary>
        private void Move()
        {
            List<Character> piv = World.GetPlayersInArea(this);
            bool exists = false;

            ///
            /// Checks current players in view to see if they are still in range.
            ///
            foreach(Character OtherPlayer in _players_iv)
            {
                if(World.GetDistance(this,OtherPlayer) > World.PLAYER_SIGHT_RANGE) {
                    OtherPlayer.DeleteView(this);
                    DeleteView(OtherPlayer);
                }
            }

            ///
            /// Add the new players into view
            ///
            foreach(Character PlayerIV in piv)
            {
                exists = false;
                foreach(Character OtherPlayer in _players_iv) 
                {
                    if(OtherPlayer == PlayerIV){
                        exists = true;
                        break;
                    }
                }

                if(!exists) {
                    AddView(PlayerIV);
                }
            }
        }

        /// <summary>
        /// Adds a player into view.
        /// </summary>
        /// <param name="Player">Player to add into view.</param>
        public void AddView(Character Player)
        {
            _players_iv.Add(Player);
        }

        /// <summary>
        /// Removes a player from view.
        /// </summary>
        /// <param name="Player">Player to remove from view.</param>
        public void DeleteView(Character Player)
        {
            _players_iv.Remove(Player);
        }

    }
}
