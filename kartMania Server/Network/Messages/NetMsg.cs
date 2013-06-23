/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 06.09.2012
 * Time: 16:05
 * 
 */
using System;
using System.IO;
using System.Runtime.InteropServices;
using kartManiaCommons.Network;

namespace kartManiaServer.Network
{
	/// <summary>
	/// NetMsg.
	/// </summary>
	public class NetMsg:IDisposable
	{
		private ushort mService; //2B
		private ushort mLenght;  //2B
		
		private MemoryStream dataStream;
		private BinaryReader streamReader;
		private BinaryWriter streamWriter;
		
		#region Constructors
		
		/// <summary>
		/// Make a new NetMsg avalible for writing.
		/// </summary>
		public NetMsg(ushort service)
		{
			dataStream   = new MemoryStream();
			//streamReader = new BinaryReader(dataStream);
			streamWriter = new BinaryWriter(dataStream);
			
			this.mService = (ushort)service;
		}
		
		
		/// <summary>
		/// Make a new NetMsg avalible for reading.
		/// </summary>
		public NetMsg(ushort service, byte[] data)
		{
			dataStream   = new MemoryStream(data);
			streamReader = new BinaryReader(dataStream);
			//streamWriter = new BinaryWriter(dataStream);
			
			this.mService = service;
			this.mLenght  = (ushort)data.Length;
		}
		
		public byte[] GetData()
		{
			mLenght = (ushort)(dataStream.Length);
			
			byte[] byteBuffer = new byte[mLenght + 4];
			ByteUnion union   = new ByteUnion();
			
			union.shrotVal = mService;
			byteBuffer[0]  = union.byteA;
			byteBuffer[1]  = union.byteB;
			
			union.shrotVal = mLenght;
			byteBuffer[2]  = union.byteA;
			byteBuffer[3]  = union.byteB;
			
			dataStream.Position = 0;
			dataStream.Read(byteBuffer, 4, mLenght);
			//dataStream.Read(byteBuffer, 0, mLenght);
			
			return byteBuffer;
		}
		
		public void Dispose()
		{
			dataStream.Dispose();
		}
		
		#endregion 
		
		#region Properties
		
		public ushort Service { get { return mService; } }
		public ushort Length  { get { return mLenght;  } }
		
		public BinaryReader Reader { get { return streamReader; } } //TODO throw exception if in writing mode
		public BinaryWriter Writer { get { return streamWriter; } } //TODO throw exception if in reading mode
		
		#endregion
		
		#region Helper Union
		
		private ushort ConvertBytes(byte x, byte y)
        {
        	ByteUnion union = new ByteUnion();
        	
        	union.byteA = x;
        	union.byteB = y;
        	
        	return union.shrotVal;
        }
        
        [StructLayout(LayoutKind.Explicit)]
		private struct ByteUnion
		{
			[FieldOffset(0)]
			public byte byteA;
		
		 	[FieldOffset(1)]
		 	public byte byteB;		 	
		
		 	[FieldOffset(0)]
		 	public ushort shrotVal;
		}
        
        #endregion
	}
}
