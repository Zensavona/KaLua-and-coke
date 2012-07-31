using System;
using Emulator.ClassMaps;

namespace Emulator.Translators
{
	public partial class PacketTranslator
	{
		public static void EquipItem(Client client, PacketIn packet)
		{
		    int itemId = (int)packet.ReadUInt32();
		    PlayerItem item = PlayerItem.Get(itemId);
		    if(item == null) {
		        ServerConsole.WriteLine(
		            System.Drawing.Color.Red,
		            "Player #{0} attempted to equip non-existant item!",
		            client.Character.Player.PlayerId
		        );    
		        return;
		    }
		    if(item.PlayerId != client.Character.Player.PlayerId) {
		        ServerConsole.WriteLine(System.Drawing.Color.Red,
		            "Player #{0} attempted to equip item that belongs to #{1}!",
		            client.Character.Player.PlayerId,
		            item.PlayerId
		        );    
		        return;
		    }
		    if(!item.Wearable) {
		        ServerConsole.WriteLine(
		            System.Drawing.Color.Red,
		            "Player #{0} attempted to equip non-equipable item #{1}!",
		            item.PlayerId,item.ItemId
		        );
		        return;
		    }
		    PlayerItem.Equip(item);
		    client.Send(new Packets.EquipItem(item),"Equip Item");
		}
		
		public static void UnequipItem(Client client, PacketIn packet)
		{
		    int itemId = (int)packet.ReadUInt32();
		    PlayerItem item = PlayerItem.Get(itemId);
		    if(item == null) {
		        ServerConsole.WriteLine(
		            System.Drawing.Color.Red,
		            "Player #{0} attempted to equip non-existant item!", 
		            client.Character.Player.PlayerId
		        );    
		        return;
		    }
		    if(item.PlayerId != client.Character.Player.PlayerId) {
		        ServerConsole.WriteLine(System.Drawing.Color.Red,
		            "Player #{0} attempted to equip item that belongs to #{1}!",
		            client.Character.Player.PlayerId,
		            item.PlayerId
		        );    
		        return;
		    }
		    if(!item.Wearable) {
		        ServerConsole.WriteLine(
		            System.Drawing.Color.Red,
		            "Player #{0} attempted to equip non-equipable item #{1}!",
		            item.PlayerId,item.ItemId
		        );
		        return;
		    }
		    PlayerItem.Unequip(item);
		    client.Send(new Packets.UnequipItem(item),"Unequip Item");
		}
		
		public static void UseItem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_UseItem",packet);
		}
		
		public static void DropItem(Client client, PacketIn packet)
		{
		    int itemId      = (int)packet.ReadUInt32();
			PlayerItem item = PlayerItem.Get(itemId);
			int quantity    = (int)packet.ReadUInt32();
			
			if(item == null) {
				ServerConsole.WriteLine(
				    "Player #{0} attempted to drop non-existant item #{1} !", 
				    client.Character.Player.PlayerId,
				    itemId
				);
				return;
			}
			
			if(item.PlayerId != client.Character.Player.PlayerId) {
			    ServerConsole.WriteLine(
			        "Player #{0} attempted to drop item that belongs to #{1}!", 
			        client.Character.Player.PlayerId,
			        item.PlayerId
			    );
				return;
			}		
			
			uint worldId = World.NewId();
			Server.WorldDrops[worldId] = new Drop(item,client.Character,quantity,worldId);

			client.Send(new Packets.SpawnDrop(Server.WorldDrops[worldId]),"Spawn Drop");	
			client.Send(new Packets.RemoveFromInventory(item.ItemId,quantity),"Update Inventory");		
			client.Character.Player.Inventory.Items.Remove(item);
		}
		
		public static void PickupDrop(Client client, PacketIn packet)
		{
		    Drop drop;
		    try {
		        drop = Server.WorldDrops[packet.ReadUInt32()];
		    } catch(Exception) {
		        return;
		    }
			
		    PlayerItem item = PlayerItem.Get(drop.PlayerItem.ItemId);
		    client.Character.Player.Inventory.Items.Add(item);

			client.Send(new Packets.PickupDrop(item),"Pickup Drop");
			client.Send(new Packets.AddToInventory(drop.ItemId,drop.Quantity),"Update Inventory");		
			client.Send(new Packets.UnspawnDrop(drop),"Unspawn Drop");
			
			Server.WorldDrops.Remove((uint)drop.WorldId);
			World.FreeId((uint)drop.WorldId);
		}
		
		public static void DestroyItem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_DestroyItem",packet);
		}
		
		public static void UpgradeItem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_UpgradeItem",packet);
		}
		
		public static void MixItem(Client client, PacketIn packet)
		{
		    Utils.DumpUnusedPacket("PT_MixItem",packet);
		}
	}
}
