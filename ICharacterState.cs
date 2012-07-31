using System;
using System.Collections.Generic;
using System.Text;
using Emulator.ClassMaps;

namespace Emulator
{
    public interface ICharacterState
    {
        double Begin(Character character);
        
        double Tick(Character character);
        
        void End(Character character);
        
        bool FitsCategory(string name);
        
        bool Contended(ICharacterState other);
    }
}
