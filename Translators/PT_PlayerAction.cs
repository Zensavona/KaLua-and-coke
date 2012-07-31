using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void Rest(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_Rest",packet);		    
		}
		
		public static void Dance(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_Dance",packet);
		}
		
		public static void IsCrafting(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_IsCrafting",packet);
		}
		
		public static void StopCrafting(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_StopCrafting",packet);
		}
		
		public static void MageRevival(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_MageRevival",packet);
		}
	}
}
