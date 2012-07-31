using System;

namespace Emulator.Translators
{
    public partial class PacketTranslator
    {
        public static void GuildAction(Client client, PacketIn packet)
        {
            byte guildActionId = packet.ReadByte();
           
            Guild guild = client.Character.Guild;

            switch(guildActionId)
            {
                case 0x01: 
                    guild.SetTrial(client,packet);
                break;
                case 0x02: 
                    guild.Dummy(client,packet); 
                break;
                case 0x03:
                    guild.SetContent(client,packet);
                break;
                case 0x04:
                    guild.Dummy(client,packet);
                break;
                case 0x05:
                    guild.SetGuildName(client,packet);
                break;
                case 0x06:
                    guild.Dummy(client,packet); 
                break;
                case 0x07: 
                    guild.AdminInfo(client,packet); 
                break;
                case 0x08: 
                    guild.BasicInfo(client,packet); 
                break;
                case 0x09: 
                    guild.Dummy(client,packet); 
                break;
                case 0x0A: 
                    guild.KickPlayer(client,packet); 
                break;
                case 0x0B: 
                    guild.SetMOTD(client,packet); 
                break;
                case 0x0C: 
                    guild.SetExp(client,packet); 
                break;
                case 0x0D: 
                    guild.SetSkill(client,packet); 
                break;
                case 0x0E: 
                    guild.Dummy(client,packet); 
                break;
                case 0x0F: 
                    guild.ConfluxAnswer(client,packet); 
                break;
                case 0x10: 
                    guild.Disband(client,packet); 
                break;
                case 0x11: 
                    guild.Dummy(client,packet); 
                break;
                case 0x12: 
                    guild.AllianceOfferAnswer(client,packet); 
                break;
                case 0x13: 
                    guild.DeclareWar(client,packet); 
                break;
                case 0x14: 
                    guild.DeclareWarList(client,packet); 
                break;
                case 0x15: 
                    guild.TrialUnfurlStandard(client,packet); 
                break;
                case 0x16: 
                    if(guild.HasCastle) {
                        guild.Castle.UnfurlStandardCancel(client,packet); 
                    }
                break;
                case 0x17: 
                    guild.CastleInfo(client,packet); 
                break;
                case 0x18: 
                    guild.TaxRate(client,packet); 
                break;
                case 0x19: 
                    guild.CastleGateNPC(client,packet); 
                break;
                case 0x1A:
                    guild.SetGateLimit(client,packet); 
                break;
                case 0x1B:
                    guild.CollectTax(client,packet); 
                break;
                case 0x1C:
                    guild.CheckFlag(client,packet); 
                break;
                case 0x1D:
                    guild.SetFlag(client,packet);
                break;
                default:   
                break;
            }
        }
        
        public static void ChangeGuildName(Client client, PacketIn packet)
        {
            Utils.DumpUnusedPacket("PT_ChangeGuildName",packet);
        }
    }
}
