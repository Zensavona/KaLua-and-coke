using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator.CharacterStates
{
	public class Rest
	{
		public double Begin(Character character)
		{
			if(character != null)
			{
				//character.PlayAnimation(3, true);
				//character.Resting = true;

				return 1.0;
			}
			return -1.0;
		}
		
		public double Tick(Character character)
		{
			character.Player.HP += Server.ServerRandom.Next(15,30);
			character.Player.MP += Server.ServerRandom.Next(15,30);

			return 1.0;
		}
		
		public void End(Character character)
		{
			if(character != null)
			{
				//character.PlayAnimation(3, false);
				//character.Resting = false;
			}
		}

		public bool FitsCategory(string name)
		{
			return name == "Move" || name == "Rest";
		}
		
		public bool Contended(ICharacterState other)
		{
			return other.FitsCategory("Run");
		}
	}
}
