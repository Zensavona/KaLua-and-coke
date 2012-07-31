using System;
using Emulator.ClassMaps;

namespace Emulator.Translators
{
    public partial class PacketTranslator
    {
        public static void NpcTalk(Client client, PacketIn packet)
        {
            Npc npc = Server.WorldNPCs[packet.ReadUInt32()];
            if(npc != null)
            {
                if(npc.Talk != 0) {
                    //ServerConsole.WriteLine("Talking with npc #{0} returning Talk #{1}",npc.NpcId,npc.Talk);
                    client.Send(new Packets.OpenDialog(npc.Talk));
                }
            }
        }
        
        public static void BuyNpcItem(Client client, PacketIn packet)
        {
            int npcWorldId = (int)packet.ReadUInt32();
            byte v1     = packet.ReadByte();
            byte amount = packet.ReadByte();
            
            int totalPrice = 0;
            
            ushort[] items   = new ushort[amount];
            ushort[] amounts = new ushort[amount];
            for(int i=0;i<amount;i++)
            {
                items[i]   = packet.ReadUShort();
                amounts[i] = packet.ReadUShort();
                
                // TODO, calculate correct price
                totalPrice += 100 * amounts[i];
            }
            
            // TODO, add geon amount check
            for(int i=0;i<amount;i++)
            {
                // should be items, not player items
                PlayerItem item = PlayerItem.Get((int)items[i]);
                client.Character.Player.Inventory.Items.Add(item);
                // update AddToInventory with generic item-object
                client.Send(new Packets.AddToInventory((int)items[i],(int)amounts[i]),"Buy Item");
                // update amout of geons !
            }	    
        }
        
        public static void SellNpcItem(Client client, PacketIn packet)
        {
            Utils.DumpUnusedPacket("PT_SellNpcItem",packet);
        }
        
        public static void NpcSpecial(Client client, PacketIn packet)
        {
            Utils.DumpUnusedPacket("PT_NpcSpecial",packet);
        }
    }
}
