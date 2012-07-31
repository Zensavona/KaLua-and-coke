using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void PlayersInSight(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_PlayersInSight",packet);
		}
	}
}
