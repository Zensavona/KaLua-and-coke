using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.Packets
{
    public class StatPacket
    {
        public class EditPacket : PacketOut
        {
            public EditPacket(byte index,ushort size) : base(0x45,(ushort)(size + 1))
            {
                writer.Write(index);
            }            
        }
        
        public class Strength : EditPacket
        {
            public Strength(Character.CharacterStats stats) : base(0x00,7)
            {
                writer.Write(stats.Strength);
                writer.Write(stats.OTP);
                writer.Write(stats.MinDamage);
                writer.Write(stats.MaxDamage);   
            }
        }
        
        public class Health : EditPacket
        {
            public Health(Character.CharacterStats stats) : base(0x01,7)
            {
                writer.Write(stats.Health);
                writer.Write(stats.CurrentHP);
                writer.Write(stats.MaximumHP);
                writer.Write(stats.ParalysisResistance);   
            }
        }
        
        public class Intelligence : EditPacket
        {
            public Intelligence(Character.CharacterStats stats) : base(0x02,11)
            {
                writer.Write(stats.Intelligence);
                writer.Write(stats.MinMagicalDamage);
                writer.Write(stats.MaxMagialDamage);
                writer.Write(stats.FireResistance);
                writer.Write(stats.IceResistance);
                writer.Write(stats.LightningResistance);   
            }
        }
                
        public class Wisdom : EditPacket
        {
            public Wisdom(Character.CharacterStats stats) : base(0x03,7)
            {
                writer.Write(stats.Wisdom);
                writer.Write(stats.CurrentMP);
                writer.Write(stats.MaximumMP);
                writer.Write(stats.CurseResistance);   
            }
        }

        public class Agility : EditPacket
        {
            public Agility(Character.CharacterStats stats) : base(0x04,9)
            {
                writer.Write(stats.Agility);
                writer.Write(stats.OTP);
                writer.Write(stats.Evasion);
                writer.Write(stats.MinDamage);
                writer.Write(stats.MaxDamage);   
            }
        }
        
    }
}
