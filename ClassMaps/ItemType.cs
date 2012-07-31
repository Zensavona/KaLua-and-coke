using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.ClassMaps 
{
    public class ItemType 
    {
        private int itemindex;
        private string name;
        private string description;
        private string image;
        private string type;
        private string subtype;
        private int wear;
        private int use;
        private int plural;
        private int limitclass;
        private int limitlevel;
        private int level;
        private int range;
        private int buy;
        private int sell;
        private int cooltime;
        private int maxprotect;
        private int endurance;
        private int buffindex;
        private int buffvalue;
        private int effect;
        
        public ItemType() {}
        
        public virtual int ItemIndex {
            get { return itemindex; }
            set { itemindex = value; }
        }

        public virtual string Name {
            get { return name; }
            set { name = value; }
        }

        public virtual string Description {
            get { return description; }
            set { description = value; }
        }

        public virtual string Image {
            get { return image; }
            set { image = value; }
        }

        public virtual string Type {
            get { return type; }
            set { type = value; }
        }

        public virtual string SubType {
            get { return subtype; }
            set { subtype = value; }
        }
        
        public virtual string FullType {
            get { return type + ":" + subtype; }
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
            get { return limitclass; }
            set { limitclass = value; }
        }

        public virtual int LimitLevel {
            get { return limitlevel; }
            set { limitlevel = value; }
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
            get { return cooltime; }
            set { cooltime = value; }
        }

        public virtual int MaxProtect {
            get { return maxprotect; }
            set { maxprotect = value; }
        }

        public virtual int Endurance {
            get { return endurance; }
            set { endurance = value; }
        }

        public virtual int BuffIndex {
            get { return buffindex; }
            set { buffindex = value; }
        }

        public virtual int BuffValue {
            get { return buffvalue; }
            set { buffvalue = value; }
        }

        public virtual int Effect {
            get { return effect; }
            set { effect = value; }
        }
    }
}