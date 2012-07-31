using System;
using Emulator.ClassMaps;

namespace Emulator.Packets
{
    public class PlayerInfo : PacketOut
    {
        public PlayerInfo(Character character) : base(0x42, 59)
        {
            writer.Write((byte)character.Player.Job);
            writer.Write((ushort)0);
            // Contribution
            writer.Write((ushort)character.Player.Contribution);	
            // Stats	
            writer.Write(character.Stats.Strength);
            writer.Write(character.Stats.Health);
            writer.Write(character.Stats.Intelligence);
            writer.Write(character.Stats.Wisdom);
            writer.Write(character.Stats.Agility);
            // Health
            writer.Write(character.Stats.CurrentHP);
            writer.Write(character.Stats.MaximumHP);
            // Mana
            writer.Write(character.Stats.CurrentMP);
            writer.Write(character.Stats.MaximumMP);
            // Special stats
            writer.Write(character.Stats.OTP);
            writer.Write(character.Stats.Evasion);
            writer.Write(character.Stats.Defense);
            writer.Write(character.Stats.Absorb);
            // Other
            writer.Write(character.Player.Experience);
            // Damage			
            writer.Write(character.Stats.MinDamage);
            writer.Write(character.Stats.MaxDamage);
            writer.Write(character.Stats.MinMagicalDamage);
            writer.Write(character.Stats.MaxMagialDamage);
            // Points
            writer.Write((short)character.Player.StatPoints);
            writer.Write((short)character.Player.SkillPoints);
            // Resistance
            writer.Write(character.Stats.FireResistance);
            writer.Write(character.Stats.IceResistance);
            writer.Write(character.Stats.LightningResistance);
            writer.Write(character.Stats.CurseResistance);
            writer.Write(character.Stats.ParalysisResistance);
            // Rage
            writer.Write((int)character.Player.Rage);
        }
    }
}
