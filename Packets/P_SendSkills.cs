using System;
using System.Collections;
using Emulator.ClassMaps;

namespace Emulator.Packets
{
    public class SendSkills : PacketOut
    {
        public SendSkills(Character character) : base(0x10)
        {
            SetCapacity((ushort)((character.Skills.Count * 2) + 1));

            writer.Write((byte)character.Skills.Count);
            
            foreach(PlayerSkill skill in character.Skills.Values) {
                writer.Write((byte)skill.SkillIndex);
                writer.Write((byte)skill.Level);
            }                         
        }
    }
}
