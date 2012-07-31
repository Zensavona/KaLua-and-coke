using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;

namespace Emulator.Packets 
{
    class DeletedPlayerList : PacketOut 
    {
        const int STORE_DAYS = 14;
        
        public DeletedPlayerList(IList<DeletedPlayer> dPlayers,IList<Player> players) : base(0x19)
        {
            ushort packetSize = 1;

            foreach(Player player in players)
            foreach(DeletedPlayer dPlayer in dPlayers) 
            {
                if(player.PlayerId == dPlayer.PlayerId) {
                    dPlayer.Player = player;
                }    
            }
            
            foreach(DeletedPlayer player in dPlayers) {
                if(player == null) { 
                    continue;
                }
                packetSize += 8;
                packetSize += (ushort)player.Player.Name.Length;
            }
            
            this.SetCapacity(packetSize);            

            writer.Write((byte)dPlayers.Count);
            foreach(DeletedPlayer player in dPlayers)
            {
                if(player == null) { 
                    continue;
                }
                writer.Write(player.PlayerId);
                writer.Write(player.Player.Name.ToCharArray());
                writer.Write((byte)0);
                writer.Write((byte)player.Player.Level);
                writer.Write((byte)player.Player.ClassId);

                TimeSpan diff = DateTime.Today - player.DeletedDate;
                byte daysLeft = (byte)(STORE_DAYS - diff.Days);
                
                writer.Write(daysLeft);   
            }
        }
    }
}
