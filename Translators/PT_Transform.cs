using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void Transform(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_Transform",packet);
		}
		
		public static void ExecuteTransform(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_ExecuteTransform",packet);
		}
	}
}
