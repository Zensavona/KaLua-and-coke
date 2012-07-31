using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;

namespace Emulator.Skills
{
    class Rest : ISkill
    {
        public Rest()
        {
        }

        public void OnExecute(PlayerSkill skill, Character caster, byte mode, byte level, Character target)
        {
            if(mode > 0)
            {
                //caster.BeginState(new CharacterStates.Rest());
            }
            else
            {
                //caster.EndStates("Rest");
            }
        }

        public void OnRequest(PlayerSkill skill, Character caster, Character target)
        {
        }

        public void OnPassiveApply(PlayerSkill skill, Character caster, byte level)
        {
        }

        public void OnPassiveRemove(PlayerSkill skill, Character caster, byte level)
        {
        }
    }
}
