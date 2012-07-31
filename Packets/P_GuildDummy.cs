using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.Packets
{
    class GuildDummy : PacketOut
    {
        public GuildDummy() : base(0x5E)
        {
        }
    }
}
