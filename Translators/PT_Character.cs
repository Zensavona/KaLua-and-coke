using System;
using NHibernate;
using NHibernate.Cfg;
using Emulator.ClassMaps;

namespace Emulator.Translators
{
    public partial class PacketTranslator
    {
        public static void CharacterChange(Client client, PacketIn packet)
        {
            byte b = packet.ReadByte();
            if(b == 1) {
                client.SaveCharacterLocation();
                client.Send(new Packets.AcceptChange(),"Change Character");
                client.UnspawnPlayer();
                client.SendPlayerList();
                return;
            }
        }
        
        public static void CharacterSelect(Client client, PacketIn packet)
        {
            client.PlayerSelect((int)packet.ReadUInt32());
        }
        
        public static void CreateCharacter(Client client, PacketIn packet)
        {
            string name       = packet.ReadString();
            byte type         = packet.ReadByte();
            byte strength     = packet.ReadByte();
            byte health       = packet.ReadByte();
            byte intelligence = packet.ReadByte();
            byte wisdom       = packet.ReadByte();
            byte agility      = packet.ReadByte();
            byte face         = packet.ReadByte();
            byte hair         = packet.ReadByte();

            if((strength + health + intelligence + wisdom + agility) != 5) {
                ServerConsole.WriteLine(System.Drawing.Color.Red,
                    "Invalid character creation request from {0}.", 
                    client.User.Username
                );
                return;
            }

            int playerId = client.CreateCharacter(name, type, strength, health, intelligence, wisdom, agility, face, hair);
            client.SendPlayerList();
        }
        
        public static void DeleteCharacter(Client client, PacketIn packet)
        {
            byte playerId = packet.ReadByte();  
            
            using(ISession session = Server.Factory.OpenSession())
            {
                Player player = (Player)session.Get(typeof(Player),(int)playerId);
                if(player == null) {
                    ServerConsole.WriteLine(
                        System.Drawing.Color.Red,
                        "Tried to delete unexisting character with PlayerId ({0)",
                        playerId
                    );
                    return;
                }
                
                using(ITransaction transaction = session.BeginTransaction()) 
                {
                    DeletedPlayer dPlayer = new DeletedPlayer();
                    
                    dPlayer.DeletedDate = DateTime.Now;
                    dPlayer.PlayerId    = player.PlayerId;
                    dPlayer.UserId      = player.UserId;
                    
                    session.Save(dPlayer);
                    transaction.Commit();
                }
                
                using(ITransaction transaction = session.BeginTransaction()) 
                {
                    player.Status = 0;
                    session.Update(player);
                    transaction.Commit();
                }
            }
            
            client.SendPlayerList();
        }
        
        public static void RestoreCharacter(Client client, PacketIn packet)
        {
            int playerId = (int)packet.ReadUInt32();
            
            using(ISession session = Server.Factory.OpenSession())
            {
                Player player = (Player)session.Get(typeof(Player),playerId);
                if(player == null) {
                    ServerConsole.WriteLine(
                        System.Drawing.Color.Red,
                        "Tried to restore unexisting character with PlayerId ({0)",
                        playerId
                    );
                    return;
                }
                
                using(ITransaction transaction = session.BeginTransaction()) 
                {
                    IQuery q = session.CreateQuery("FROM DeletedPlayer WHERE PlayerId = :playerId");
                           q.SetParameter("playerId",playerId);
                    DeletedPlayer dPlayer = q.UniqueResult<DeletedPlayer>();
                    
                    session.Delete(dPlayer);
                    transaction.Commit();
                }
                
                using(ITransaction transaction = session.BeginTransaction()) 
                {
                    player.Status = 1;
                    session.Update(player);
                    transaction.Commit();
                }
            }			
            
            client.SendPlayerList();
        }
        
        public static void SetRevivalPoint(Client client, PacketIn packet)
        {
            Utils.DumpUnusedPacket("PT_SetRevivalPoint",packet);
        }
        
        public static void ChangeCharacterName(Client client, PacketIn packet)
        {
            Utils.DumpUnusedPacket("PT_ChangeCharacterName",packet);
        }
    }
}