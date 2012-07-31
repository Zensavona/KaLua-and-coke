using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.ClassMaps 
{
    public class Player 
    {
        private int playerId;
        private int status;
        private int userId;
        private string name;
        private int classId;
        private int level;
        private int job;
        private int strength;
        private int health;
        private int intelligence;
        private int wisdom;
        private int agility;
        private int face;
        private int hair;
        private long experience;
        private int hp;
        private int mp;
        private int statpoints;
        private int skillpoints;
        private int contribution;
        private int rage;
        private int x;
        private int y;
        private int z;
        private Inventory inventory;
        
        public Player() {}
        
        public virtual int PlayerId {
            get { return playerId;  }
            set { playerId = value; }     
        }
        
        public virtual int Status {
            get { return status;  }
            set { status = value; }     
        }        
        
        public virtual int UserId {
            get { return userId;  }
            set { userId = value; }
        }
        
        public virtual string Name {
            get { return name;  }
            set { name = value; }
        }
        
        public virtual int ClassId {
            get { return classId;  }
            set { classId = value; }
        }

        public virtual int Level {
            get { return level;  }
            set { level = value; }
        }
        
        public virtual int Job {
            get { return job;  }
            set { job = value; }
        }
                
        public virtual int Strength {
            get { return strength;  }
            set { strength = value; }
        }
        
        public virtual int Health {
            get { return health;  }
            set { health = value; }
        }
        
        public virtual int Intelligence {
            get { return intelligence;  }
            set { intelligence = value; }
        }
        
        public virtual int Wisdom {
            get { return wisdom;  }
            set { wisdom = value; }
        }
        
        public virtual int Agility {
            get { return agility;  }
            set { agility = value; }
        }
        
        public virtual int Face {
            get { return face;  }
            set { face = value; }
        }
        
        public virtual int Hair {
            get { return hair;  }
            set { hair = value; }
        }
        
        public virtual long Experience {
            get { return experience;  }
            set { experience = value; }
        }
        
        public virtual int HP {
            get { return hp;  }
            set { hp = value; }
        }
        
        public virtual int MP {
            get { return mp;  }
            set { mp = value; }
        }
        
        public virtual int SkillPoints {
            get { return (skillpoints >= 0) ? skillpoints : 0; }
            set { skillpoints = value; }
        }
        
        public virtual int StatPoints {
            get { return (statpoints >= 0) ? statpoints : 0;  }
            set { statpoints = value; }
        }
        
        public virtual int Contribution {
            get { return contribution;  }
            set { contribution = value; }
        }
        
        public virtual int Rage {
            get { return rage;  }
            set { rage = value; }
        }

        public virtual int X {
            get { return x;  }
            set { x = value; }
        }
        
        public virtual int Y {
            get { return y;  }
            set { y = value; }
        }
        
        public virtual int Z {
            get { return z;  }
            set { z = value; }
        }

        public virtual Inventory Inventory {
            get { 
                if(inventory == null) {
                    inventory = new Inventory(this.playerId);
                }
                return inventory; 
            }
            set { inventory = value; }
        }
    }
}
