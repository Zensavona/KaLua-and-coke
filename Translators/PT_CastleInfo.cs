using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void CastleInfo(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_CastleInfo",packet);
		}
	}
}
