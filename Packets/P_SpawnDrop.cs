using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;

namespace Emulator.Packets 
{
    class SpawnDrop : PacketOut
    {
        public SpawnDrop(Drop drop) : base(0x36,18)
        {
            writer.Write(drop.ItemId);
            writer.Write(drop.WorldId);
            writer.Write(drop.PositionX);
            writer.Write(drop.PositionY);
            writer.Write(drop.Quantity);
        }
    }
}
