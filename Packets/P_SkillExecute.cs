using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.Packets 
{
    public class SkillExecute : PacketOut
    {
        public SkillExecute(Character caster,byte index, byte level) : base(0x3f,11)
        {
            writer.Write(index);
            writer.Write(caster.WorldID);
            writer.Write(caster.WorldID);
            writer.Write((byte)0);
            writer.Write(level);
        }  
        
        public SkillExecute(Character caster,Character target,byte index, byte level) : base(0x3f,11)
        {
            writer.Write(index);
            writer.Write(caster.WorldID);
            writer.Write(target.WorldID);
            writer.Write((byte)0);
            writer.Write(level);
        } 		    
    }
}
