using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.Packets
{
    public class EndPlayerMove : PacketOut
    {
        public EndPlayerMove(uint WorldID, sbyte DX, sbyte DY, sbyte DZ) : base(0x23, 7)
        {
            writer.Write(WorldID);
            writer.Write(DX);
            writer.Write(DY);
            writer.Write(DZ);
        }
    }
}
