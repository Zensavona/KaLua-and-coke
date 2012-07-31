using System;

namespace Emulator.Packets
{
	public class Unknown : PacketOut
	{
		public Unknown(byte packetId, byte[] data) : base(packetId)
		{
			if(data != null && data.Length > 0) {
				SetCapacity((ushort)data.Length);
				writer.Write(data);
			} else {
				SetCapacity((ushort)0);
			}
		}
	}
}
