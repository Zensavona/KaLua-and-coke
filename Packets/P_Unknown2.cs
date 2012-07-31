using System;

namespace Emulator.Packets
{
	public class Unknown2 : PacketOut
	{
		public Unknown2(byte data) : base(0x1C, 1)
		{
			writer.Write(data);
		}
	}
}
