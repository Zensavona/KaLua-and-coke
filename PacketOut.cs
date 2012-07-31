using System;
using System.IO;
using System.Text;

namespace Emulator
{
    /// <summary>
    /// Send packages to the connected client
    /// </summary>
    public class PacketOut
    {
        /// <summary>
        /// Packet Key
        /// </summary>
        protected MemoryStream stream;
        protected BinaryWriter writer;
        private ushort _length;
        private byte _type;
        private bool _compiled = false;
        private uint _key = 56;
        
        public uint Key {
            get { return _key; }
            set { _key = value; } 
        } 
        
        public MemoryStream Stream {
            get { return stream; }
        }

        public PacketOut(byte type,ushort length)
        {
            _type   = type;
            _length = (ushort)(length + 3);

            stream = new MemoryStream(_length);
            writer = new BinaryWriter(stream);

            writer.Write(_length);
            writer.Write(_type);
        }

        public PacketOut(byte type)
        {
            _type = type;
        }

        public void SetCapacity(ushort newCapacity)
        {
            _length = (ushort)(newCapacity + 3);
            
            stream  = new MemoryStream(_length);
            writer  = new BinaryWriter(stream);
            
            writer.Write(_length);
            writer.Write(_type);
        }

        public byte[] Compile()
        {
            if(!_compiled) {
                stream.Position = 0;
                writer.Write(Crypter.EncodeString(stream.ToArray(),_key));
                _compiled = true;
            }
            return stream.ToArray();
        }
    }
}
