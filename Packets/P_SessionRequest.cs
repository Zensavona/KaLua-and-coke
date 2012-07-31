using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.Packets 
{
    class SessionRequest : PacketOut
    {
        public SessionRequest() : base(0x67, 41)
        {
            writer.Write((byte)0x01);
            writer.Write((byte)0x01);
            writer.Write((byte)0x10);
            writer.Write((byte)0x03);
            writer.Write((byte)0x10);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x0f);
            writer.Write((byte)0x10);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x2f);
            writer.Write((byte)0x30);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
            writer.Write((byte)0x00);
        }
    }
}
