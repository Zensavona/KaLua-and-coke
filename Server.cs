using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Xml;
using System.Xml.XPath;
using NHibernate;
using NHibernate.Cfg;
using Emulator.ClassMaps;

namespace Emulator
{
    public class Server
    {
        /// <summary>
        /// Starts a Kalonline Server, with following actions
        /// - Configure NHibernate (ORM)
        /// - Load Maps
        /// - Start ClientHandling thread
        /// </summary>
        public Server()
        {
            ///
            /// Load Server Config
            /// 
            if(File.Exists("Config.xml")) 
            {
                XPathNavigator config = new XPathDocument("Config.xml").CreateNavigator();
                PORT = Int32.Parse(config.SelectSingleNode("config/port").Value);
            } 
            else 
            {
                ServerConsole.WriteLine(Color.Red,"Could not find Config.xml, aborting startup.");
                return;
            }
            
            ///
            /// Configure NHibernate
            /// 
            ServerConsole.WriteLine(Color.Blue,"- [Configuring Database]");
            Configuration cfg = new Configuration();
            cfg.AddAssembly("Emulator");
            Factory = cfg.BuildSessionFactory();

            ///
            /// Load Maps
            ///
            ServerConsole.WriteLine(Color.Blue,"- [Loading Maps]");
            World.LoadMaps();
            
            ///
            /// Load NPCs
            /// 
            ServerConsole.WriteLine(Color.Blue,"- [Loading NPCs]");
            World.LoadNPCs();

            if(World.done) 
            {
                ///
                /// Start the server
                /// 
                processConnectionsThread = new Thread(
                    new ThreadStart( Server.ProcessConnections )
                );
                processConnectionsThread.Start();
                ServerThinker.Start();
                ServerConsole.WriteLine("Server Started");
            }
        }

        /// <summary>
        /// Process Client connections, and calls a Async Callback
        /// </summary>
        private static void ProcessConnections()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any,Server.PORT);
            Socket listener = new Socket(
                AddressFamily.InterNetwork, 
                SocketType.Stream, 
                ProtocolType.Tcp
            );

            try 
            {
                listener.Bind(localEndPoint);
                listener.Listen(5);

                while(Server.Running) 
                {
                    resetEvent.Reset();
                    listener.BeginAccept(new AsyncCallback(Server.CallbackAccept), listener);
                    resetEvent.WaitOne();
                }
            } 
            catch(Exception e) 
            {
                ServerConsole.WriteLine(System.Drawing.Color.Red,e.ToString());
            }
        }

        /// <summary>
        /// Callback that handles client connections
        /// </summary>
        /// <param name="ar"></param>
        private static void CallbackAccept(IAsyncResult ar)
        {
            resetEvent.Set();
            
            ///
            /// Listen for connections
            /// 
            Socket listener = (Socket)ar.AsyncState;
            Socket handler  = listener.EndAccept(ar);
            
            ///
            /// Client connected ! Creating client object.
            /// 
            Client client = new Client(handler);
            clients.Add(client);
            ServerConsole.WriteLine(Color.Blue,"Client #{0} connected from {1}",client.IP);
            
            ///
            /// Starts to listen for client input
            /// 
            handler.BeginReceive(
                client.Buffer, 0, 
                Client.BUFFERSIZE, 0, 
                new AsyncCallback(client.OnReceive), 
                null
            );
        }

        /// <summary>
        /// Remove the client from the clients list on disconnect
        /// </summary>
        /// <param name="client"></param>
        public static void ClientDisconnected(Client client)
        {
            if(clients.Contains(client)) {
                clients.Remove(client);
            }
        }
        
        /// <summary>
        /// Closes the server, and disconnects all clients
        /// </summary>
        public void Close() {
            try {
                foreach(Client client in clients) {
                    client.Disconnect();
                }
            } catch(Exception) {}
            Server.Running = false;
            processConnectionsThread.Abort();
            Server.ServerThinker.Stop();
        }

        #region Properties
        
        /// <summary>
        /// Server Port Number
        /// </summary>
        public static int PORT = 30001;
        
        /// <summary>
        /// Active Clients 
        /// </summary>
        public static List<Client> clients = new List<Client>();
        
        /// <summary>
        /// Active Users
        /// </summary>
        public static Dictionary<int,bool> Users = new Dictionary<int,bool>();

        /// <summary>
        /// Connection Processing Thread
        /// </summary>
        public static Thread processConnectionsThread; 
        
        /// <summary>
        /// NHibernate Session Factory
        /// </summary>
        public static ISessionFactory Factory;
        
        /// <summary>
        /// Thread Communicator, handles event waiting
        /// </summary>
        public static ManualResetEvent resetEvent = new ManualResetEvent(false);
        
        /// <summary>
        /// Server Queue Timer
        /// </summary>
        public static Thinker ServerThinker = new Thinker();
        
        /// <summary>
        /// Random seed generator used for Attacks and similiar events
        /// </summary>
        public static Random ServerRandom = new Random();
        
        /// <summary>
        /// Sets about the server is running, and should listen for clients
        /// </summary>
        public static bool Running = true;
        
        /// <summary>
        /// Current World Drops
        /// </summary>
        public static Dictionary<uint,Drop> WorldDrops = new Dictionary<uint,Drop>();
        
        /// <summary>
        /// Current World NPCs
        /// </summary>
        public static Dictionary<uint,Npc> WorldNPCs = new Dictionary<uint,Npc>();
        
        #endregion		
    }
}
