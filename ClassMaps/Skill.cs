using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.ClassMaps
{
    public class Skill
    {
        private int skillid;
        private int classId;
        private int redistribute;
        private int limitlevel;
        private int limitjob;
        private int limitskill;
        private int limitskilllevel;
        private int maxlevel;
        private int mp;
        private int lasttime;
        private int delayin;
        private int delayout;
        private int delayother;
        private int value1;
        private int value2;

        public virtual int SkillId {
            get { return skillid; }
            set { skillid = value; }
        }

        public virtual int ClassId {
            get { return classId; }
            set { classId = value; }
        }

        public virtual int Redistribute {
            get { return redistribute; }
            set { redistribute = value; }
        }

        public virtual int Limitlevel {
            get { return limitlevel; }
            set { limitlevel = value; }
        }

        public virtual int LimitJob {
            get { return limitjob; }
            set { limitjob = value; }
        }

        public virtual int LimitSkill {
            get { return limitskill; }
            set { limitskill = value; }
        }

        public virtual int LimitSkillLevel {
            get { return limitskilllevel; }
            set { limitskilllevel = value; }
        }

        public virtual int MaxLevel {
            get { return maxlevel; }
            set { maxlevel = value; }
        }

        public virtual int MP {
            get { return mp; }
            set { mp = value; }
        }

        public virtual int LastTime {
            get { return lasttime; }
            set { lasttime = value; }
        }

        public virtual int DelayIn {
            get { return delayin; }
            set { delayin = value; }
        }

        public virtual int DelayOut {
            get { return delayout; }
            set { delayout = value; }
        }

        public virtual int DelayOther {
            get { return delayother; }
            set { delayother = value; }
        }

        public virtual int Value1 {
            get { return value1; }
            set { value1 = value; }
        }

        public virtual int Value2 {
            get { return value2; }
            set { value2 = value; }
        }
    }
}