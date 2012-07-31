using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    public class Guild
    {
        private Castle _castle;
        
        public bool HasCastle {
            get { return _castle != null; }
        }
        
        public Castle Castle {
            get { return _castle; }
        }
        
        public void SetTrial(Client client,PacketIn packet)
        {
        }
        
        public void Dummy(Client client,PacketIn packet)
        {
            client.Send(new Packets.GuildDummy());            
        }
        
        public void SetContent(Client client,PacketIn packet)
        {
        }

        public void SetGuildName(Client client,PacketIn packet)
        {
        }

        public void AdminInfo(Client client,PacketIn packet)
        {
        }

        public void BasicInfo(Client client,PacketIn packet)
        {
        }

        public void KickPlayer(Client client,PacketIn packet)
        {
        }

        public void SetMOTD(Client client,PacketIn packet)
        {
        }

        public void SetExp(Client client,PacketIn packet)
        {
        }

        public void SetSkill(Client client,PacketIn packet)
        {
        }

        public void ConfluxAnswer(Client client,PacketIn packet)
        {
        }

        public void Disband(Client client,PacketIn packet)
        {
        }

        public void AllianceOfferAnswer(Client client,PacketIn packet)
        {
        }

        public void DeclareWar(Client client,PacketIn packet)
        {
        }

        public void DeclareWarList(Client client,PacketIn packet)
        {
        }

        public void TrialUnfurlStandard(Client client,PacketIn packet)
        {
        }

        public void CastleInfo(Client client,PacketIn packet)
        {
        }

        public void TaxRate(Client client,PacketIn packet)
        {
        }

        public void CastleGateNPC(Client client,PacketIn packet)
        {
        }

        public void SetGateLimit(Client client,PacketIn packet)
        {
        }

        public void CollectTax(Client client,PacketIn packet)
        {
        }

        public void CheckFlag(Client client,PacketIn packet)
        {
        }

        public void SetFlag(Client client,PacketIn packet)
        {
        }
    }
}
