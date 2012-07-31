using System;
using System.Collections;
using System.Collections.Generic;
using Emulator.ClassMaps;

namespace Emulator.Translators
{
    public partial class PacketTranslator
    {
        private static void _UpdateStats(Client client,int start,byte value)
        {
            if(value < start) {
                client.Character.Player.StatPoints -= 1; 
            } else if(value >= start && value < (start+20)) {
                client.Character.Player.StatPoints -= 2;        
            } else if(value >= (start+20) && value < (start+40)) {
                client.Character.Player.StatPoints -= 3; 
            } else if(value >= (start+40) && value < (start+60)) {
                client.Character.Player.StatPoints -= 4; 
            } else if(value >= (start+60) && value < (start+80)) {
                client.Character.Player.StatPoints -= 5; 
            } else if(value >= (start+80) && value < (start+100)) {
                client.Character.Player.StatPoints -= 6; 
            } else if(value >= (start+100) && value < (start+120)) {
                client.Character.Player.StatPoints -= 7; 
            } else {
                client.Character.Player.StatPoints -= 8; 
            }
            
            client.Send(new Packets.ValueChange(0x17,(ushort)client.Character.Player.StatPoints));
        }
        
        public static void ResetStats(Client client, PacketIn packet)
        {
            Utils.DumpUnusedPacket("PT_ResetStats",packet);
        }
        
        public static void SetStats(Client client, PacketIn packet)
        {
            int start;
            byte value;
            
            switch(packet.ReadByte()) 
            {
                // Strength
                case 0:	
                {
                    start = (client.Character.Player.ClassId == 0) ? 60 : 50;
                    value = client.Character.Stats.Strength;
                    
                    _UpdateStats(client,start,value);

                    client.Character.Stats.Strength += 1;
                    client.Send(new Packets.StatPacket.Strength(client.Character.Stats));

                    break;
                }
                // Health
                case 1:	
                {
                    start = 50;
                    value = client.Character.Stats.Health;
                    
                    _UpdateStats(client,start,value);

                    client.Character.Stats.Health += 1;
                    client.Send(new Packets.StatPacket.Health(client.Character.Stats));
    
                    break;
                }
                // Intelligence
                case 2:	
                {
                    start = (client.Character.Player.ClassId == 1) ? 60 : 50;
                    value = client.Character.Stats.Intelligence;
                    
                    _UpdateStats(client,start,value);

                    client.Character.Stats.Intelligence += 1;
                    client.Send(new Packets.StatPacket.Intelligence(client.Character.Stats));
                    
                    break;
                }
                // Wisdom
                case 3:	
                {
                    start = 50;
                    value = client.Character.Stats.Wisdom;
                    
                    _UpdateStats(client,start,value);

                    client.Character.Stats.Wisdom += 1;
                    client.Send(new Packets.StatPacket.Wisdom(client.Character.Stats));
                    
                    break;
                }
                // Agility
                case 4:
                {
                    start = (client.Character.Player.ClassId == 2) ? 60 : 50;
                    value = client.Character.Stats.Agility;
                    
                    _UpdateStats(client,start,value);

                    client.Character.Stats.Agility += 1;
                    client.Send(new Packets.StatPacket.Agility(client.Character.Stats));
                    
                    break;
                }
            }
        }
    }
}
