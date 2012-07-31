using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;

namespace Emulator 
{
    public class Drop 
    {
        private int _worldId;
        private int posX;
        private int posY;
        private int quantity;
        private PlayerItem playerItem;
        
        public Drop(PlayerItem item,Character player,int amount,uint worldId)
        {
            playerItem = item;
            _worldId   = (int)worldId;
            posX       = player.PosX + Server.ServerRandom.Next(-32,32);
            posY       = player.PosY + Server.ServerRandom.Next(-32,32);
            quantity   = amount;
        }
        
        public PlayerItem PlayerItem {
            get { return playerItem; }
        }
        
        public ushort ItemId {
            get { return (ushort)playerItem.ItemIndex; }
        }
        
        public int WorldId {
            get { return _worldId; }
        }
        
        public int PositionX {
            get { return posX; }
        }
        
        public int PositionY {
            get { return posY; }
        }
        
        public int Quantity {
            get { return quantity; }
        }
    }
}
