using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void RequestTrade(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_RequestTrade",packet);
		}

		public static void OnTradeRequest(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_OnTradeRequest",packet);
		}
		
		public static void Trade(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_Trade",packet);
		}
		
		public static void CancelTrade(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_CancelTrade",packet);
		}
		
		public static void AcceptTrade(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_AcceptTrade",packet);
		}
	}
}
