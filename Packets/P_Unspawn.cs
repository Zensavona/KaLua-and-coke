using System;

namespace Emulator.Packets
{
	public class Unspawn : PacketOut
	{
		public Unspawn(int worldId) : base(0x38, 4)
		{
			writer.Write(worldId);
		}
	}
}
