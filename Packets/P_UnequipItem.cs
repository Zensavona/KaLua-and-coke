using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;

namespace Emulator.Packets 
{
    public class UnequipItem : PacketOut 
    {
        public UnequipItem(PlayerItem item) : base(0x06,10)
        {
            writer.Write(item.PlayerId);
            writer.Write(item.ItemId);
            writer.Write((ushort)item.ItemIndex);
        }
    }
}
