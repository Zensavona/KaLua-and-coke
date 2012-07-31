using System;     
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Expression;
using Emulator.ClassMaps;
using Emulator.Translators;
using Emulator.Forms;

namespace Emulator
{
    public partial class Client
    {
        #region Properties
            
        /// <summary>
        /// Packet Buffersize
        /// </summary>
        public const int BUFFERSIZE = 1024;
        
        /// <summary>
        /// User Instance (Mapped Table)
        /// </summary>
        private User user;
        
        /// <summary>
        /// Current Played logged in
        /// </summary>
        private Character character;
        
        /// <summary>
        /// NHibernate ISession
        /// </summary>
        private ISession session;
        
        /// <summary>
        /// Socket used for sending data
        /// </summary>
        private Socket mainSocket;
        
        /// <summary>
        /// Packet Buffer
        /// </summary>
        private byte[] _buffer = new byte[BUFFERSIZE];
        
        /// <summary>
        /// Temp Buffer Size
        /// </summary>
        private int tempSize  = 0;
        
        /// <summary>
        /// Temporary Buffer
        /// </summary>
        private byte[] tempBuffer = new byte[BUFFERSIZE];
        
        /// <summary>
        /// Client Packet Key
        /// </summary>
        private uint packetKey = 56;
        
        /// <summary>
        /// Translators
        /// </summary>
        private TranslatorHandler handler = new TranslatorHandler();
        
        /// <summary>
        /// Skills
        /// </summary>
        private SkillHandler skills = new SkillHandler();
        
        /// <summary>
        /// Is User Autenticated
        /// </summary>
        private bool IsAuthenticated;
       
        /// <summary>
        /// Returns about a user is autenticated or not
        /// </summary>
        public bool Authenticated {
            get { return IsAuthenticated; }
        }
        
        /// <summary>
        /// Have the player been spawned?
        /// </summary>
        public bool Spawned = false;
        
        /// <summary>
        /// Return the User object with Username/Password informations
        /// </summary>
        public User User {
            get { return user; }
        }    
        
        /// <summary>
        /// Returns the current Packet Buffer
        /// </summary>
        public byte[] Buffer {
            get { return _buffer; }
        }
        
        /// <summary>
        /// Return the client IP
        /// </summary>
        public IPAddress IP {
            get { 
                return IPAddress.Parse(
                    ((IPEndPoint)mainSocket.RemoteEndPoint).Address.ToString()
                ); 
            }
        }
        
        public Character Character {
            get { return character; }
        }
        
        public SkillHandler SkillHandler {
            get { return skills; }
        }
        
        #endregion
             
        /// <summary>
        /// Starts the client, using the Server socket
        /// </summary>
        /// <param name="socket"></param>
        public Client(Socket socket)
        {
            RegisterPacketTranslators();
            RegisterSkills();
            mainSocket = socket;
        }

        /// <summary>
        /// Register Skills
        /// </summary>
        protected void RegisterSkills()
        {
            //skills.Add(0x08,new Skills.Heal());
        }

        /// <summary>
        /// Register Packet Translators
        /// </summary>
        protected void RegisterPacketTranslators()
        {
            handler.Add(0xA3,PacketTranslator.Init);
            
            handler.Add(0x00,PacketTranslator.RestoreCharacter);
          //handler.Add(0x01,PacketTranslator.AntiCP);
            handler.Add(0x02,PacketTranslator.Login);
          //handler.Add(0x03,PacketTranslator.CheckCRC);
            handler.Add(0x04,PacketTranslator.CreateCharacter);
          //handler.Add(0x05,PacketTranslator.Ping);
          //handler.Add(0x06,PacketTranslator.DublicateCharacter);
            handler.Add(0x07,PacketTranslator.DeleteCharacter);
          //handler.Add(0x08,PacketTranslator.RandomConnectInfo);
            handler.Add(0x09,PacketTranslator.ClientVersion);
            handler.Add(0x0A,PacketTranslator.CharacterSelect);
            handler.Add(0x0B,PacketTranslator.SpawnPlayer);
            handler.Add(0x0C,PacketTranslator.Dummy);
            handler.Add(0x0D,PacketTranslator.Dummy);
            handler.Add(0x0E,PacketTranslator.Dummy);
            handler.Add(0x0F,PacketTranslator.Attack);
            handler.Add(0x10,PacketTranslator.SkillExecute);
            handler.Add(0x11,PacketTranslator.PlayerChat);
            handler.Add(0x12,PacketTranslator.Teleport);
            handler.Add(0x13,PacketTranslator.CharacterChange);
            handler.Add(0x14,PacketTranslator.PlayerMove);
            handler.Add(0x14,PacketTranslator.EndPlayerMove);
            handler.Add(0x15,PacketTranslator.PlayerStopped);
            handler.Add(0x16,PacketTranslator.NpcTalk);
            handler.Add(0x17,PacketTranslator.CastleInfo);
            handler.Add(0x18,PacketTranslator.BuyNpcItem);
            handler.Add(0x19,PacketTranslator.SellNpcItem);
            handler.Add(0x1A,PacketTranslator.DropItem);
            handler.Add(0x1B,PacketTranslator.Exit);
            handler.Add(0x1C,PacketTranslator.Trade);
            handler.Add(0x1D,PacketTranslator.PlayersInSight);
            handler.Add(0x1E,PacketTranslator.SetStats);
            handler.Add(0x1F,PacketTranslator.Rest);
            handler.Add(0x20,PacketTranslator.PickupDrop);
            handler.Add(0x21,PacketTranslator.UseItem);
            handler.Add(0x22,PacketTranslator.RequestTrade);
            handler.Add(0x23,PacketTranslator.OnTradeRequest);
            handler.Add(0x24,PacketTranslator.CancelTrade);
            handler.Add(0x25,PacketTranslator.Revive);
            handler.Add(0x26,PacketTranslator.SiegeGunDisable);
            handler.Add(0x27,PacketTranslator.SiegeGunEnable);
            handler.Add(0x28,PacketTranslator.SiegeGunControl);
            handler.Add(0x29,PacketTranslator.LearnSkill);
            handler.Add(0x2A,PacketTranslator.UpgradeSkill);
            handler.Add(0x2B,PacketTranslator.SkillRequest);
            handler.Add(0x2C,PacketTranslator.RequestParty);
            handler.Add(0x2E,PacketTranslator.GuildAction);
            handler.Add(0x2D,PacketTranslator.OnPartyRequest);
            handler.Add(0x2F,PacketTranslator.LeaveParty);
            handler.Add(0x30,PacketTranslator.KickPartyPlayer);
            handler.Add(0x31,PacketTranslator.BankAdd);
            handler.Add(0x32,PacketTranslator.BankRetrieve);
            handler.Add(0x33,PacketTranslator.CallProcess);
            handler.Add(0x34,PacketTranslator.BankInfo);
            handler.Add(0x35,PacketTranslator.Dummy);
            handler.Add(0x36,PacketTranslator.Dummy);
            handler.Add(0x37,PacketTranslator.Dummy); 
            handler.Add(0x38,PacketTranslator.SetRevivalPoint);
            handler.Add(0x39,PacketTranslator.EnchantItem);
            handler.Add(0x3A,PacketTranslator.CreateShop);
            handler.Add(0x3B,PacketTranslator.Dummy);
            handler.Add(0x3C,PacketTranslator.Dummy);
            handler.Add(0x3D,PacketTranslator.Dance);
            handler.Add(0x3E,PacketTranslator.AcceptTrade);
            handler.Add(0x3F,PacketTranslator.DestroyItem);
            handler.Add(0x40,PacketTranslator.FriendsList);
            handler.Add(0x41,PacketTranslator.AddShopItem);
            handler.Add(0x42,PacketTranslator.RemoveShopItem);
            handler.Add(0x43,PacketTranslator.ToggleShop);
            handler.Add(0x44,PacketTranslator.DiceSystem);
            handler.Add(0x45,PacketTranslator.StopCrafting);
            handler.Add(0x46,PacketTranslator.ViewShop);
            handler.Add(0x47,PacketTranslator.BuyShopItem);
            handler.Add(0x48,PacketTranslator.MasterOfPRS);
            handler.Add(0x49,PacketTranslator.IsCrafting);
            handler.Add(0x4A,PacketTranslator.MageRevival);
            handler.Add(0x4B,PacketTranslator.ResetSkills);
            handler.Add(0x4C,PacketTranslator.GoldenLuckyPouch);
            handler.Add(0x4D,PacketTranslator.GoldenPotion);
            handler.Add(0x4E,PacketTranslator.ResetStats);
            handler.Add(0x4F,PacketTranslator.ViewAssassinList);
            handler.Add(0x50,PacketTranslator.RequestPvP);
            handler.Add(0x51,PacketTranslator.OnRequestPvP);
            handler.Add(0x52,PacketTranslator.Transform);
            handler.Add(0x53,PacketTranslator.Buff);
            handler.Add(0x54,PacketTranslator.ExecuteTransform);
            handler.Add(0x55,PacketTranslator.TeacherStudentSystem);
            handler.Add(0x56,PacketTranslator.Dummy);
            handler.Add(0x57,PacketTranslator.Dummy);
            handler.Add(0x58,PacketTranslator.Dummy);
            handler.Add(0x59,PacketTranslator.SessionRequest);
            handler.Add(0x5A,PacketTranslator.SetPlayerTeleport);
            handler.Add(0x5B,PacketTranslator.UpgradeItem);
            handler.Add(0x5C,PacketTranslator.Mail);
            handler.Add(0x5D,PacketTranslator.CancelOpenSpecialItem);
            handler.Add(0x5E,PacketTranslator.OpenSpecialItem);
            handler.Add(0x5F,PacketTranslator.ChangeGuildName);
            handler.Add(0x60,PacketTranslator.ChangeCharacterName);
            handler.Add(0x61,PacketTranslator.NpcSpecial);
            handler.Add(0x62,PacketTranslator.MixItem);
            handler.Add(0x63,PacketTranslator.BeadOfFire);           
        }

        /// <summary>
        /// Process a packet input
        /// </summary>
        /// <param name="buffer"></param>
        public void Process(byte[] buffer)
        {
            PacketIn packet = new PacketIn(buffer,packetKey);
            handler.Translate(this,packet.PacketType,packet);

            if(packet.PacketType == 0xA3) {
                return;
            }

            if(packetKey == 63) {
                packetKey = 0;
            } else {
                packetKey++;
            }
        }

        /// <summary>
        /// Send a Command (Packet) asyncron
        /// </summary>
        /// <param name="packet"></param>
        public void Send(PacketOut packet)
        {
            byte[] buffer = packet.Compile();
            int length    = buffer.Length;

            Utils.ServerDump(buffer,length);
            
            mainSocket.BeginSend(
                buffer, 0, 
                length, 0, 
                new AsyncCallback(CallbackSend), 
                mainSocket
            );
        }
        
        public void Send(PacketOut packet,string title) {
            //ServerConsole.WriteLine("Sending [{0}]",title);
            this.Send(packet);
        }

        /// <summary>
        /// Asyncron command sender callback
        /// </summary>
        /// <param name="result"></param>
        private void CallbackSend(IAsyncResult result)
        {
            Socket socket = (Socket)result.AsyncState;
            socket.EndSend(result);
        }

        /// <summary>
        /// Handles packet receiving
        /// </summary>
        /// <param name="result"></param>
        public void OnReceive(IAsyncResult result)
        {
            try
            {
                int bytesRead = mainSocket.EndReceive(result);
                if(bytesRead > 0) {
                    Array.Copy(_buffer, 0, tempBuffer, tempSize, bytesRead);
                    tempSize += bytesRead;
                } else {
                    Disconnect();
                    return;
                }
            }
            catch(SocketException e)
            {
                if(e.ErrorCode == 10054) {
                    Disconnect();
                    return;
                }
                ServerConsole.WriteLine(Color.Red,"Socket Error: {0}", e.Message);
                return;
            } catch(ObjectDisposedException) {
            } catch(NullReferenceException) {
            }
    
            while(tempSize >= 2  &&  tempSize >= ((int)tempBuffer[0] + (((int)tempBuffer[1])<<8)))
            {
                int chunkSize = (int)tempBuffer[0] + (((int)tempBuffer[1])<<8);

                Process(tempBuffer);

                Array.Copy(tempBuffer, chunkSize, tempBuffer, 0, tempSize-chunkSize);
                tempSize -= chunkSize;
            }
            
            try {
                mainSocket.BeginReceive(_buffer, 0, BUFFERSIZE, 0, new AsyncCallback(OnReceive), null);
            } catch(Exception) {}
        }

        /// <summary>
        /// Selects a Player
        /// </summary>
        /// <param name="playerId"></param>
        public void PlayerSelect(int playerId)
        {
            if(Authenticated)
            {
                character = new Character(this,playerId);
                Send(new Packets.LoginAccepted());
                Send(new Packets.PlayerInfo(character));
                Send(new Packets.SetCamera(character,0));
                Send(new Packets.CameraUpdate());	
                
                foreach(Npc npc in Server.WorldNPCs.Values) {
                    Send(new Packets.SpawnNpc(npc));
                }					
            }
        }
        
        /// <summary>
        /// Performs a User Authentication
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        protected bool Authenticate(string username, string password)
        {
            using(session = Server.Factory.OpenSession()) {
                IQuery q = session.CreateQuery("FROM User WHERE Username = :user");
                       q.SetParameter("user",username);
                user = (User)q.UniqueResult();   
            }
            if(user == null) {
                return false;
            }
            if(user.Password != Utils.ConvertSha1(password)) {
                return false;
            }
            if(Server.Users.ContainsKey(user.UserId)) {
                return false;
            } else {
                Server.Users[user.UserId] = true;
                IsAuthenticated = true;
                
                MainForm.Instance.InvokeDelegate(delegate() {
                    MainForm.Instance.usersListBox.Items.Add(user.Username);
                });
            }
            return true;
        }

        /// <summary>
        /// Attempt a user login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void UserLogin(string username,string password)
        {
            if(Authenticate(username,password)){
                // If Autenticated, send player list
                SendPlayerList();
            } else {
                // Else send error
                Send(new Packets.LoginError(Packets.LOGIN_ERROR.WRONGID));
            }
        }
        
        /// <summary>
        /// Sends the list of players to the client
        /// </summary>
        public void SendPlayerList()
        {
            using(session = Server.Factory.OpenSession()) 
            {
                IQuery q = session.CreateQuery("FROM Player WHERE UserId = :userId AND Status != 0");
                       q.SetParameter("userId",user.UserId);
                Send(new Packets.PlayerList(q.List<Player>()));
            
                q = session.CreateQuery("FROM DeletedPlayer WHERE UserId = :userId");
                q.SetParameter("userId",user.UserId);
                IList<DeletedPlayer> dPlayers = q.List<DeletedPlayer>();

                q = session.CreateQuery("FROM Player WHERE UserId = :userId AND Status = 0");
                q.SetParameter("userId",user.UserId);
                IList<Player> players = q.List<Player>();
                
                Send(new Packets.DeletedPlayerList(dPlayers,players));
            }
        }

        /// <summary>
        /// Unspawn a player
        /// </summary>
        public void UnspawnPlayer()
        {
            World.FreeId(character.WorldID);
            character = null;
        }

        /// <summary>
        /// Sends Initialization packet when the user selects the server
        /// </summary>
        public void SendInit()
        {
            if(Authenticated) { 
                packetKey = 29;
            } else {
                packetKey = 56;
                Send(new Packets.Init((byte)packetKey, 0x00));
            }
        }
        
        public void SaveCharacterLocation() 
        {
            using(session = Server.Factory.OpenSession())
            using(ITransaction transaction = session.BeginTransaction()) {
                character.Player.X = character.PosX;
                character.Player.Y = character.PosY;
                character.Player.Z = character.PosZ;    
                
                session.Update(character.Player);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Handles client disconnecting
        /// </summary>
        public void Disconnect()
        {
            if(user != null) {
                Server.Users.Remove(user.UserId);
                MainForm.Instance.InvokeDelegate(delegate() {
                    MainForm.Instance.usersListBox.Items.Remove(user.Username);
                });
                ServerConsole.WriteLine(Color.Blue,"Connection for client #{0} terminated.", user.UserId);
            }

            if(mainSocket.Connected) { 
                mainSocket.Shutdown(SocketShutdown.Both);
            }    

            mainSocket.Close();
            mainSocket = null;
            
            try {
                World.FreeId(character.WorldID);
            } catch(Exception) {}
                
            if(character != null) {
                SaveCharacterLocation();
            }

            Server.ClientDisconnected(this);
        }
    }
}
