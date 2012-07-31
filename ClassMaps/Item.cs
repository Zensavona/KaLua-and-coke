using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.ClassMaps
{
    public class Item
    {
        private int itemIndex;
        private int name;
        private string image;
        private int itemTypeId;
        private int wear;
        private int use;
        private int plural;
        private int limitClass;
        private int limitLevel;
        private int level;
        private int range;
        private int buy;
        private int sell;
        private int coolTime;
        private int maxProtect;
        private int endurance;
        private int buffIndex;
        private int buffValue;
        private int effect;

        public virtual int ItemIndex {
            get { return itemIndex; }
            set { itemIndex = value; }
        }

        public virtual int Name {
            get { return name; }
            set { name = value; }
        }

        public virtual string Image {
            get { return image; }
            set { image = value; }
        }

        public virtual int ItemTypeId {
            get { return itemTypeId; }
            set { itemTypeId = value; }
        }

        public virtual int Wear {
            get { return wear; }
            set { wear = value; }
        }

        public virtual int Use {
            get { return use; }
            set { use = value; }
        }

        public virtual int Plural {
            get { return plural; }
            set { plural = value; }
        }

        public virtual int LimitClass {
            get { return limitClass; }
            set { limitClass = value; }
        }

        public virtual int LimitLevel {
            get { return limitLevel; }
            set { limitLevel = value; }
        }

        public virtual int Level {
            get { return level; }
            set { level = value; }
        }

        public virtual int Range {
            get { return range; }
            set { range = value; }
        }

        public virtual int Buy {
            get { return buy; }
            set { buy = value; }
        }

        public virtual int Sell {
            get { return sell; }
            set { sell = value; }
        }

        public virtual int CoolTime {
            get { return coolTime; }
            set { coolTime = value; }
        }

        public virtual int MaxProtect {
            get { return maxProtect; }
            set { maxProtect = value; }
        }

        public virtual int Endurance {
            get { return endurance; }
            set { endurance = value; }
        }

        public virtual int BuffIndex {
            get { return buffIndex; }
            set { buffIndex = value; }
        }

        public virtual int BuffValue {
            get { return buffValue; }
            set { buffValue = value; }
        }

        public virtual int Effect {
            get { return effect; }
            set { effect = value; }
        }

    }
}
