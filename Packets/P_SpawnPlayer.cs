using System;
using Emulator.ClassMaps;

namespace Emulator.Packets
{
    public class SpawnPlayer : PacketOut
    {
        public SpawnPlayer(Character character, bool isPlayerCharacter) : base(0x32)
        {
            SetCapacity((ushort)(59 + character.Player.Name.Length));

            writer.Write(character.WorldID);
            writer.Write(character.Player.Name.ToCharArray());
            writer.Write((byte)0);

            if(isPlayerCharacter) {
                writer.Write((byte)((byte)0x80 | (byte)character.Player.ClassId));
            } else {
                writer.Write((byte)character.Player.ClassId);
            }
            
            ///
            /// Coordinates
            /// 
            writer.Write(character.Player.X);
            writer.Write(character.Player.Y);
            writer.Write(character.Player.Z);
            
            writer.Write((short)1);
            
            if(character.IsAlive) {
                writer.Write((byte)0); // Alive
            } else {
                writer.Write((byte)2); // Dead
            }
            
            writer.Write((short)0);
            writer.Write((byte)0);

            /// 
            /// Gear
            /// 
            writer.Write(character.Player.Inventory.Gear.Weapon);
            writer.Write(character.Player.Inventory.Gear.Shield);   
            writer.Write(character.Player.Inventory.Gear.Helmet);  
            writer.Write(character.Player.Inventory.Gear.UpperArmor);  
            writer.Write(character.Player.Inventory.Gear.LowerArmor); 
            writer.Write(character.Player.Inventory.Gear.Gauntlet);  
            writer.Write(character.Player.Inventory.Gear.Boots); 
            
            ///
            /// Look
            /// 
            writer.Write((byte)character.Player.Face);
            writer.Write((byte)character.Player.Hair);

            for(int i=0;i<19;i++) {
                writer.Write((byte)0);
            }
        }
    }
}
