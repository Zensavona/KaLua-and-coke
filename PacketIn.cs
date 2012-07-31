using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Emulator 
{
    /// <summary>
    /// Parses the recieved packets from the client
    /// </summary>
    public class PacketIn
    {
        private MemoryStream stream;
        private BinaryReader reader;

        private ushort length;
        private byte type;

        public byte PacketType { 
            get { return type; } 
        }
        
        public ushort PacketSize { 
            get { return length; } 
        }

        public PacketIn(byte[] packetBuffer,uint packetKey)
        {
            if(packetBuffer[2] != 0xA3) {
                packetBuffer = Crypter.DecodeString(packetBuffer,packetKey);
            }
            
            stream = new MemoryStream(packetBuffer);
            reader = new BinaryReader(stream);

            length = reader.ReadUInt16();
            type   = reader.ReadByte();
        }

        public byte ReadByte()
        {
            return reader.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return reader.ReadSByte();
        }

        public ushort ReadUShort()
        {
            return reader.ReadUInt16();;
        }

        public uint ReadUInt32()
        {
            return reader.ReadUInt32();;
        }

        public string ReadString()
        {
            byte[] readString = new byte[1024];
            byte c;
            int charCount = 0;

            // Read from the stream until \0 is read
            while((c = reader.ReadByte()) != 0x00)
            {
                try {
                    readString[charCount] = c;
                    charCount++;
                } catch(ArgumentOutOfRangeException) {
                    ServerConsole.WriteLine(System.Drawing.Color.Red,"Received string was longer than 1024 byte!");
                    break;
                }
            }

            string returnString;
            
            try {
                returnString = Encoding.ASCII.GetString(readString, 0, charCount);
            } catch(Exception) {
                ServerConsole.WriteLine(System.Drawing.Color.Red,"There was an error while converting data to a string!");
                returnString = " ";
            }
            
            return returnString;
        }
    }
}
