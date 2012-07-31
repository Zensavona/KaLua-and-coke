using System;

namespace Emulator.Packets
{
	public class UnspawnDrop : PacketOut
	{
		public UnspawnDrop(Drop drop) : base(0x3b, 4)
		{
			writer.Write(drop.WorldId);
		}
	}
}