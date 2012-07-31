using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void OnRequestPvP(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_OnRequestPvP",packet);
		}
		
		public static void RequestPvP(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_RequestPvP",packet);
		}
		
		public static void ViewAssassinList(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_ViewAssassinList",packet);
		}
	}
}
