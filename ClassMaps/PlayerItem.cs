using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace Emulator.ClassMaps 
{
    public class PlayerItem 
    {
        private int itemId;
        private int playerId;
        private int itemIndex; 
        private int itemType;
        private int quantity;
        private int prefix;
        private int info;
        private int maxEndurance;
        private int curEndurance;
        private int attack;
        private int magic;
        private int defense;
        private int otp;
        private int evade;
        private int protect;
        private int ebLevel;
        private int ebRate;
        private uint worldId;
        
        public PlayerItem() {
            worldId = World.NewId();
        }
        
        public virtual uint WorldId {
            get { return worldId; }
        }
        
        public virtual int ItemId {
            get { return itemId; }
            set { itemId = value; }
        }

        public virtual int PlayerId {
            get { return playerId; }
            set { playerId = value; }
        }

        public virtual int ItemIndex {
            get { return itemIndex; }
            set { itemIndex = value; }
        }

        public virtual int ItemType {
            get { return itemType; }
            set { itemType = value; }
        }

        public virtual int Quantity {
            get { return quantity; }
            set { quantity = value; }
        }

        public virtual int Prefix {
            get { return prefix; }
            set { prefix = value; }
        }

        public virtual int Info {
            get { return info; }
            set { info = value; }
        }

        public virtual int MaxEndurance {
            get { return maxEndurance; }
            set { maxEndurance = value; }
        }

        public virtual int CurEndurance {
            get { return curEndurance; }
            set { curEndurance = value; }
        }

        public virtual int Attack {
            get { return attack; }
            set { attack = value; }
        }

        public virtual int Magic {
            get { return magic; }
            set { magic = value; }
        }

        public virtual int Defense {
            get { return defense; }
            set { defense = value; }
        }

        public virtual int OTP {
            get { return otp; }
            set { otp = value; }
        }
        
        public virtual int Evade {
            get { return evade; }
            set { evade = value; }
        }        

        public virtual int Protect {
            get { return protect; }
            set { protect = value; }
        }

        public virtual int EBRate {
            get { return ebRate; }
            set { ebRate = value; }
        }

        public virtual int EBLevel {
            get { return ebLevel; }
            set { ebLevel = value; }
        }
        
        public virtual bool Wearable {
            get 
            {
                bool wearable = false; 
                using(ISession session = Server.Factory.OpenSession())
                {
                    IQuery q = session.CreateQuery("FROM Item WHERE ItemIndex = :itemIndex");
                           q.SetParameter("itemIndex",this.itemIndex);
                    Item item = q.UniqueResult<Item>();
                    if(item != null) {
                        wearable = (item.Wear == 1);
                    }
                }
                return wearable;
            }       
        }
        
        public static void Equip(PlayerItem item)
        {
            using(ISession session = Server.Factory.OpenSession()) 
            using(ITransaction transaction = session.BeginTransaction()) {
                item.info |= 0x01;
                
                session.Update(item);
                transaction.Commit();
            }
        }
        
        public static void Unequip(PlayerItem item)
        {
            using(ISession session = Server.Factory.OpenSession())
            using(ITransaction transaction = session.BeginTransaction()) {
                item.info &= 0xFFFFFE;
                
                session.Update(item);
                transaction.Commit();
            }
        }
        
        public static PlayerItem Get(int itemId) 
        {
            using(ISession session = Server.Factory.OpenSession())
            {
                return session.Get<PlayerItem>(itemId);
            }
        }
    }
}
