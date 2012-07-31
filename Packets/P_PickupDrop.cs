using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;

namespace Emulator.Packets 
{
    class PickupDrop : PacketOut
    {
        public PickupDrop(PlayerItem item) : base(0x07,26)
        {
            writer.Write(item.ItemId);
            writer.Write(item.ItemIndex);
            
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            
            writer.Write(item.Quantity);
            
            for(int i=0;i<11;i++) {
                writer.Write((byte)0); 
            }
        }
    }
}
