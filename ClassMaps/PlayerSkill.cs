using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.ClassMaps 
{
    public class PlayerSkill 
    {
        private int playerSkillId;
        private int playerId;
        private int skillIndex;
        private int level;
        
        public PlayerSkill() {}
        
        public virtual int PlayerSkillId {
            get { return playerSkillId; }
            set { playerSkillId = value; }
        }
        
        public virtual int PlayerId {
            get { return playerId;  }
            set { playerId = value; }
        }
        
        public virtual int SkillIndex {
            get { return skillIndex;  }
            set { skillIndex = value; }
        }
        
        public virtual int Level {
            get { return level;  }
            set { level = value; }
        }
    }
}
