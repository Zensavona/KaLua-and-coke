using System;
using Emulator.ClassMaps;

namespace Emulator.Packets
{
	public class SetCamera : PacketOut
	{
		public SetCamera(Character character, ushort mapId) : base(0x1b, 10)
		{
			writer.Write(mapId);
			writer.Write(character.Player.X);
			writer.Write(character.Player.Y);
		}
	}
}
