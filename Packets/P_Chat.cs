using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.Packets
{
    class Chat : PacketOut
    {
        public Chat(string name,string message,params object[] args) : base(0x3c)
        {
            string msg = String.Format(message,args);
            SetCapacity((ushort)(msg.Length + name.Length + 2));
            
            writer.Write(name.ToCharArray());
            writer.Write((byte)0);
            writer.Write(msg.ToCharArray());
            writer.Write((byte)0);
        }   
    }
}
