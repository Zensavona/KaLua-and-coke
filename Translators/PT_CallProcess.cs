using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void CallProcess(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_CallProcess",packet);
		}
	}
}
