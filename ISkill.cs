using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;

namespace Emulator
{
    public interface ISkill
    {
        void OnRequest(PlayerSkill skill, Character caster, Character target);

        void OnExecute(PlayerSkill skill, Character caster, byte mode, byte level, Character target);

        void OnPassiveApply(PlayerSkill skill, Character caster, byte level);

        void OnPassiveRemove(PlayerSkill skill, Character caster, byte level);
    }
}
