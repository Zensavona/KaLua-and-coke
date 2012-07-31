using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void CreateShop(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_CreateShop",packet);
		}
		
		public static void AddShopItem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_AddShopItem",packet);
		}
		
		public static void RemoveShopItem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_RemoveShopItem",packet);
		}
		
		public static void ToggleShop(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_ToggleShop",packet);
		}
		
		public static void ViewShop(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_ViewShop",packet);
		}
		
		public static void BuyShopItem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_BuyShopItem",packet);
		}
	}
}
