using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    public class SkillHandler
    {
        public Dictionary<byte,ISkill> Handlers = new Dictionary<byte,ISkill>();
    
        public void Add(byte skillId,ISkill skill) 
        {
            Handlers[skillId] = skill;
        }
        
        public void Execute(byte skillId,byte skillLvl) 
        {
            
        }

        public void Request(byte skillId,uint playerId) 
        {
            if(!Handlers.ContainsKey(skillId)) {
                ServerConsole.WriteLine(System.Drawing.Color.Red,"Unknown Skill Id #{0}",skillId);
            } 	
        }
    }
}
