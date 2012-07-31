using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void BankAdd(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_BankAdd",packet);
		}

		public static void BankRetrieve(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_BankRetrieve",packet);
		}

		public static void BankInfo(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_BankInfo",packet);
		}
	}
}
