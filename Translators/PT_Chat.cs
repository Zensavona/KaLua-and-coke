using System;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void PlayerChat(Client client, PacketIn packet)
		{
		    string message = packet.ReadString();
		    client.Send(new Packets.Chat(client.Character.Player.Name,message));
		}
	}
}
