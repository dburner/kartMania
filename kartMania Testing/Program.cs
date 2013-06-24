﻿/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 06.09.2012
 * Time: 17:42 
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using kartManiaCommons.Network;
using kartManiaCommons.Network.Messages;
using kartManiaCommons.Network.Messages.Lobby;

namespace kartMania_Testing
{
	class Program
	{
		private class TestClass
		{
			private int x = 0;
			
			public delegate int MethodDel(int x, int y);
			public static int Method(int x, int y) { return x+y; }
		}
		[StructLayout(LayoutKind.Explicit)]
		private struct ByteUnion
		{
			[FieldOffset(0)]
			public byte byteA;
		
		 	[FieldOffset(1)]
		 	public byte byteB;		 	
		
		 	[FieldOffset(0)]
		 	public ushort intVal;
		}
		
		public static void Main(string[] args)
		{	
			StaticCtorTesting();
			//NetMsgQueue.RegisterMsgTypes();
			
			Console.ReadLine();
		}
		
		
		public static void StaticCtorTesting()
		{
			//LobbyChatMsg msg = new LobbyChatMsg();
			Assembly asm = Assembly.GetAssembly(typeof(NetMsg));
		  
			var typesArray = asm.GetTypes();
			
			foreach(Type type in typesArray)
			{
				if (type.BaseType == typeof(NetMsg))
				{
					//RuntimeHelpers.RunClassConstructor(type.TypeHandle);
					var method = type.GetMethod("CreateInstance", BindingFlags.Static | BindingFlags.NonPublic);
					
					//method.DeclaringType
					//type.GetMethods(BindingFlags.Static |BindingFlags.NonPublic);
				}
				
			}
		}
		
		public static void TypeTesting()
		{
			List<TestClass.MethodDel> list = new List<TestClass.MethodDel>();
			list.Add(TestClass.Method);
			
			int x = list[0](2,7);
			Console.WriteLine(x);
			Console.ReadLine();
		}
		
		public static void ReflectionTesting()
		{
			Type   netMsgType = typeof(NetMsg);
			Type lobbyMsgType = typeof(LobbyChatMsg);
			NetMsg msg;
			
			byte[] byteArray = new byte[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 };
			
			foreach(var ctor in lobbyMsgType.GetConstructors())
			{
				Console.WriteLine(ctor.ToString());
				if (ctor.GetParameters().Length == 1)
					if (ctor.GetParameters()[0].ParameterType == byteArray.GetType())
						msg = (NetMsg)ctor.Invoke(new object[] { byteArray } );
			}
			
			Console.ReadLine();
		}
		
		public static void EnumTesting()
		{
			//int x = NetService.LastValue;
			
			for(int i = 0; i< (int)NetService.LastValue; i++)
			{
				Console.WriteLine(i + "\t- " + (NetService)i);
			}
			
			for(int i = 0; i< (int)NetLobbyService.LastValue; i++)
			{
				Console.WriteLine(i + "\t- " + (NetLobbyService)i);
			}
			
			Console.ReadKey(true);
		}
		
		public static void UnionTesting()
		{
			ByteUnion union = new ByteUnion();			
			
			ushort val = 300;
			byte[] array;
			
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);
			
			writer.Write(val);
			array = stream.ToArray();
			
			union.byteA = array[0]; union.byteB = array[1];
			
			Console.WriteLine("ByteA = {0} , ByteB = {1}, ushort = {2} ", union.byteA, union.byteB, union.intVal);
			
			Console.ReadKey(true);
		}
	}	
}