using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;

namespace Emulator.Packets
{
    class SpawnNpc : PacketOut
    {
        public SpawnNpc(Npc npc) : base(0x34,29)
        {            
            writer.Write(npc.WorldId);
            writer.Write((ushort)npc.NpcId);
            writer.Write((byte)npc.Shape);
            writer.Write(npc.X);
            writer.Write(npc.Y);
            writer.Write(npc.Z);
            writer.Write(npc.DX);
            writer.Write(npc.DY);
            
            for(int i=0;i<8;i++) {
                writer.Write((byte)0);
            }
        }        
    }
}
