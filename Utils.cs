using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography;

namespace Emulator 
{
    /// <summary>
    /// Emulator Utilities
    /// </summary>
    public class Utils 
    {
        /// <summary>
        /// Converts a string into a Sha1 hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertSha1(string input) {
            SHA1 shaProvider = new SHA1CryptoServiceProvider();
            byte[] hash   = shaProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
            string output = ""; 
            foreach(byte b in hash) { 
                output += b.ToString("x2");  
            }  
            return output; 
        }

        /// <summary>
        /// Dump a packet to a decimal output 		
        /// </summary> 		
        /// <param name="buffer"></param> 		
        /// <param name="length"></param>
        public static void ServerDump(byte[] buffer, int length)
        {
            byte serverKey = 56;
            buffer = Emulator.Crypter.DecodeString(buffer, serverKey);
            if(buffer[2] == 0x2a) {
                serverKey = buffer[7];
            }
            int dlength = buffer[0] + (buffer[1] << 8) - 3;
            byte type = buffer[2];

            //if(Utils.ServerFilter(type))
            //{
            //    ServerConsole.WriteLine("--[ SERVER:  Id: {0:x2}  Length: {1} ]--", type, length);
            //    ServerConsole.Write("Decimal: ");
            //    for(int i=0;i<length;i++) {
            //        ServerConsole.Write("{0} ", buffer[i]);
            //    }
            //    ServerConsole.WriteLine(System.Environment.NewLine);
            //}
        }		
        
        /// <summary>
        /// Check about the byte is a server sent byte
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ServerFilter(byte type)
        {
            switch(type)
            {
                case 0x24:
                case 0x25:
                case 0x33:
                case 0x34:
                case 0x38:
                case 0x39:
                    return false;
            }
            return true;
        }	        

        /// <summary>
        /// Forwards the XmlTextReader to the next element.
        /// </summary>
        /// <param name="xtr">XmlTextReader being used.</param>
        public static void XmlNextElement(XmlTextReader xtr)
        {
            while(true) 
            {
                xtr.Read();
                if(xtr.NodeType == XmlNodeType.Element) {
                    break;
                }
            }
        }
        
        public static void DumpUnknown(PacketIn packet)
        {
            ServerConsole.Write(System.Drawing.Color.Red,"Dumping unknown packet with Id {0:X2}: ", packet.PacketType);

            for(int i=0;i<(packet.PacketSize-3);i++) {
                ServerConsole.Write(System.Drawing.Color.Red,"{0} ",packet.ReadByte());
            }

            ServerConsole.WriteLine("");        
        }

        public static void DumpUnusedPacket(string name,PacketIn packet)
        {
            ServerConsole.Write(System.Drawing.Color.Red,"Dumping unused packet #{0} with Id {1:X2}: ",name,packet.PacketType);

            for(int i=0;i<(packet.PacketSize-3);i++) {
                ServerConsole.Write(System.Drawing.Color.Red,"{0} ",packet.ReadByte());
            }

            ServerConsole.WriteLine("");        
        }
    }
}
