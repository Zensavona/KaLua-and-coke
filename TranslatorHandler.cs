using System;
using System.Collections;
using System.Collections.Generic;
using Emulator.Translators;
using System.Reflection;

namespace Emulator
{
	public class TranslatorHandler
	{
		public Dictionary<byte, TranslatorMethod> Handlers = new Dictionary<byte, TranslatorMethod>();
		
		public delegate void TranslatorMethod(Client client, PacketIn packet);
		
		public void Add(byte packetId,TranslatorMethod translator) 
		{
		    Handlers[packetId] = translator;
		}
		
		public void Translate(Client client,byte packetId,PacketIn packet) 
		{
		    if(!Handlers.ContainsKey(packetId)) {
		        PacketTranslator.DumpUnknown(client,packet);  
		    } else {
		        Handlers[packetId](client,packet);
		    }	
		}
	}
}
