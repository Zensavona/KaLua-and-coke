using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void Exit(Client client, PacketIn packet)
		{
		    client.Send(new Packets.Exit(),"Client Exit");
		}
	}
}
