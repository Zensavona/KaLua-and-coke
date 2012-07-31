using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.Packets 
{
    class SpawnMob : PacketOut
    {
        public SpawnMob() : base(0x33,43)
        {
            // test
            writer.Write((ushort)213);
            writer.Write(World.NewId());
            writer.Write(257368);
            writer.Write(258650);
            
            writer.Write((ushort)1);
            
            writer.Write((ushort)2000);
            writer.Write((ushort)0);
            writer.Write((ushort)2000);
            writer.Write((ushort)0);
            
            writer.Write((byte)0);
            
            for(int i=0;i<12;i++) {
                writer.Write((byte)0);
            }
            
            writer.Write((byte)1);
            
            for(int i=0;i<5;i++) {
                writer.Write((byte)0);
            }

            //swWriter.Write(mob.MID);

            //swWriter.Write(mob.WorldID);
            //swWriter.Write(mob.Position.X);
            //swWriter.Write(mob.Position.Y);

            //swWriter.Write((ushort)1);
            //swWriter.Write(mob.Stats.CurrentHP);
            //swWriter.Write((ushort)0);
            //swWriter.Write(mob.Stats.MaximumHP);
            //swWriter.Write((ushort)0);
            //swWriter.Write((byte)(mob.Dead?1:0));
            //for(int lp=0; lp<12; lp++) swWriter.Write((byte)0);
            //swWriter.Write(mob.Race);
            //for(int lp=0; lp<5; lp++) swWriter.Write((byte)0);
        }
    }
}
