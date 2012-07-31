using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void DiceSystem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_DiceSystem",packet);
		}
		
		public static void MasterOfPRS(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_MasterOfPRS",packet);
		}
		
		public static void GoldenLuckyPouch(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_GoldenLuckyPouch",packet);
		}
		
		public static void GoldenPotion(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_GoldenPotion",packet);
		}
		
		public static void BeadOfFire(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_BeadOfFire",packet);
		}
		
		public static void TeacherStudentSystem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_TeacherStudentSystem",packet);
		}
	}
}
