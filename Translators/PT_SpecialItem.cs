using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void OpenSpecialItem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_OpenSpecialItem",packet);
		}
		
		public static void CancelOpenSpecialItem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_CancelOpenSpecialItem",packet);
		}
	}
}
