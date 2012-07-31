using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void SiegeGunDisable(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_SigeGunDisable",packet);
		}

		public static void SiegeGunEnable(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_SigeGunEnable",packet);
		}

		public static void SiegeGunControl(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_SigeGunControl",packet);
		}
	}
}
