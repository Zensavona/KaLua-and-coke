using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void Attack(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_Attack",packet);
		}
	}
}
