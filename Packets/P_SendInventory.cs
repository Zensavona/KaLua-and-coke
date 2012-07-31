using System;
using System.Collections;
using Emulator.ClassMaps;

namespace Emulator.Packets
{
    public class SendInventory : PacketOut
    {
        public SendInventory(Inventory inventory) : base(0x04)
        {
            SetCapacity((ushort)((inventory.Items.Count * 26) + 1));
            
            writer.Write((byte)inventory.Items.Count);
            foreach(PlayerItem item in inventory.Items)
            {
                writer.Write((int)item.ItemId);                  
                writer.Write((ushort)item.ItemIndex);       
                writer.Write((byte)item.Prefix);
                writer.Write((int)item.Info);
                writer.Write((int)item.Quantity);
                writer.Write((byte)item.MaxEndurance);
                writer.Write((byte)item.CurEndurance);
                writer.Write((byte)0x00); // SetGem            
                writer.Write((byte)item.Attack);
                writer.Write((byte)item.Magic);             
                writer.Write((byte)item.Defense);
                writer.Write((byte)item.OTP);
                writer.Write((byte)item.Evade);               
                writer.Write((byte)item.Protect);
                writer.Write((byte)item.EBLevel);
                writer.Write((byte)item.EBRate);
            }
        }
    }
}
