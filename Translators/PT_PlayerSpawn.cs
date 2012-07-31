using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.Translators 
{
    public partial class PacketTranslator 
    {
        public static void SpawnPlayer(Client client, PacketIn packet)
        {
            if(client.Authenticated && (client.Spawned == false)) 
            {
                World.AddPlayer(client.Character);
                client.Send(new Packets.SendSkills(client.Character));
                if(client.Character.Player.Inventory.Items.Count > 0) {
                    client.Send(new Packets.SendInventory(client.Character.Player.Inventory));
                } else {
                    client.Send(new Packets.LoginAccepted());
                }
                client.Send(new Packets.SpawnPlayer(client.Character,true));
            }
        }
    }
}
