using System;   
using System.Collections.Generic;
using Emulator.ClassMaps;

namespace Emulator.Packets
{
    public class PlayerList : PacketOut
    {
        public PlayerList(IList<Player> playerList) : base(0x11)
        {
            int packetSize = 10;
            foreach(Player player in playerList) {
                packetSize += player.Name.Length;
                packetSize += 19;
                packetSize += (player.Inventory.EquippedItems.Count * 2);
            }
            SetCapacity((ushort)packetSize);

            writer.Write((int)0x0);
            writer.Write((byte)0x0);
            writer.Write((byte)playerList.Count);

            foreach(Player player in playerList) {
                // id and name
                writer.Write(player.PlayerId);
                writer.Write(player.Name.ToCharArray());
                writer.Write((byte)0);
                // class & level
                writer.Write((byte)player.ClassId);
                writer.Write(player.Level);
                writer.Write((byte)0);                
                // stats
                writer.Write((byte)player.Strength);
                writer.Write((byte)player.Health);
                writer.Write((byte)player.Intelligence);
                writer.Write((byte)player.Wisdom);
                writer.Write((byte)player.Agility);
                // look
                writer.Write((byte)player.Face);
                writer.Write((byte)player.Hair);
                // inventory
                writer.Write((byte)player.Inventory.EquippedItems.Count);
                foreach(PlayerItem item in player.Inventory.EquippedItems) {
                    writer.Write((ushort)item.ItemIndex);
                }                
            }
            writer.Write((int)0x03);
        }
    }
}
