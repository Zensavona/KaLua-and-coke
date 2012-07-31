using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Emulator.ClassMaps;

namespace Emulator {
    public class Inventory {
        private IList<PlayerItem> items;
        private List<PlayerItem> equippedItems = new List<PlayerItem>(); 
        private Gear gear = new Gear();
                                               
        public IList<PlayerItem> Items {
            get { return items; }
        }
        
        public List<PlayerItem> EquippedItems {
            get { return equippedItems; }
        } 
        
        public Gear Gear {
            get { return gear; }
        }
        
        /// <summary>
        /// Initializes a new Inventory from a Player Id
        /// </summary>
        /// <param name="playerId"></param>
        public Inventory(int playerId) 
        {
             using(ISession session = Server.Factory.OpenSession())
             {
                 IQuery q = session.CreateQuery("FROM PlayerItem WHERE PlayerId = :playerId");
                        q.SetParameter("playerId",playerId);
                 items = q.List<PlayerItem>();
                 foreach(PlayerItem item in items) {
                    if((item.Info & 1) == 1) {
                        equippedItems.Add(item);
                        switch(item.ItemType) 
                        {
                            case (int)EquipSlots.Boots:
                                gear.Boots = (ushort)item.ItemIndex;
                            break;
                            case (int)EquipSlots.Gauntlet:
                                gear.Gauntlet = (ushort)item.ItemIndex;
                            break;
                            case (int)EquipSlots.Helmet:
                                gear.Helmet = (ushort)item.ItemIndex;
                            break;
                            case (int)EquipSlots.LowerArmor:
                                gear.LowerArmor = (ushort)item.ItemIndex;
                            break;
                            case (int)EquipSlots.UpperArmor:
                                gear.UpperArmor = (ushort)item.ItemIndex;
                            break;
                            case (int)EquipSlots.Shield:
                                gear.Shield = (ushort)item.ItemIndex;
                            break;
                            case (int)EquipSlots.Weapon:
                                gear.Weapon = (ushort)item.ItemIndex;
                            break;
                        }
                    }
                 }
             }
        }
        
        /// <summary>
        /// EquipSlots, Indexes are defined by the ItemTypes table in the database
        /// </summary>
        public enum EquipSlots {
            Boots      = 5,
            Gauntlet   = 6,
            Helmet     = 7,
            LowerArmor = 8,
            UpperArmor = 9,
            Shield     = 10,
            Weapon     = 11,
        }
    }
}
