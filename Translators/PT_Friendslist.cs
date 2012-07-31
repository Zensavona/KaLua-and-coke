using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void FriendsList(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_FriendsList",packet);
		}
	}
}
