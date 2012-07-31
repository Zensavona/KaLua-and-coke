using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.Packets
{
    public class PlayerMove : PacketOut
    {
        public PlayerMove(uint WorldID, sbyte DX, sbyte DY, sbyte DZ) : base(0x22, 7)
        {
            writer.Write(WorldID);
            writer.Write(DX);
            writer.Write(DY);
            writer.Write(DZ);
        }
    }
}
