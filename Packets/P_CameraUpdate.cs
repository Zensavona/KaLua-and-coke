using System;

namespace Emulator.Packets
{
	public class CameraUpdate : PacketOut
	{
		public CameraUpdate() : base(0x16, 2)
		{
			writer.Write((byte)0);
			writer.Write((byte)0);
		}
	}
}
