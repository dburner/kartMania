/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 06.09.2012
 * Time: 17:42 
 * 
 */
using System;
using System.IO;
using System.Runtime.InteropServices;
using kartManiaCommons.Network;

namespace kartMania_Testing
{
	class Program
	{
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
			EnumTesting();
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