using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;

namespace Emulator.Packets 
{
    class AddToInventory : PacketOut
    {
        public AddToInventory(int itemId,int quantity) : base(0x3b,8)
        {
            writer.Write(itemId);
            writer.Write(quantity);
        }
    }
    
    class RemoveFromInventory : PacketOut
    {     
        public RemoveFromInventory(int itemId,int quantity) : base(0x08,8)
        {
            writer.Write(itemId);
            writer.Write(quantity);
        }
    }
}
