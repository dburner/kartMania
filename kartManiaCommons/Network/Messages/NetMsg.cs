/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 23.06.2013
 * Time: 14:36 
 * 
 */
using System;
using System.Dynamic;
using System.IO;
using System.Runtime.InteropServices;
using kartManiaCommons.Network;

namespace kartManiaCommons.Network.Messages
{
	/// <summary>
	/// Base class for specialized network messages
	/// A message can only be in writing mode or reading mode
	/// </summary>
	public class NetMsg:IDisposable
	{
		protected ushort mService; //2B
		protected ushort mLenght;  //2B
		
		protected MsgMode	   mMsgMode;
		protected MemoryStream dataStream;
		protected BinaryReader streamReader;
		protected BinaryWriter streamWriter;
		
		
		private const string writingModeExMsg = "Message is not in writing mode";
		
		#region Static Constructors
		
		//protected static void CreateInstance(byte[] data)
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new NetMsg avalible for writing.
		/// </summary>
		public NetMsg(ushort service)
		{
			dataStream   = new MemoryStream();
		  //streamReader = new BinaryReader(dataStream);
			streamWriter = new BinaryWriter(dataStream);
			
			mService = (ushort)service;
			
			mMsgMode  = MsgMode.WritingMode;
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
			mMsgMode       = MsgMode.ReadingMode;
		}
		
		#endregion
		
		#region Public Methods
		
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
		
		
		/// <summary>
		/// Writes all the data to the buffer and ends the writing mode.
		/// </summary>
		public virtual void Build()
		{
			if (mMsgMode == MsgMode.WritingMode)
			{
				// Set the message mode to BuildMode
				mMsgMode = MsgMode.Build;
				//mMsgMode = MsgMode.ReadingMode;
			}
			else
			{
				throw new NetMsgException("Message is not in writing mode");
			}
		}
		
		#endregion 
		
		#region Protected Methods
		
		protected void AssertWritingMode()
		{
			if (mMsgMode != MsgMode.WritingMode)
					new NetMsgException(writingModeExMsg);
		}
		
		#endregion
		
		#region Properties
		
		public ushort Service { get { return mService; } }
		public ushort Length  { get { return mLenght;  } }
		
		public BinaryReader Reader { get { return streamReader; } } //TODO throw exception if in writing mode
		public BinaryWriter Writer { get { return streamWriter; } } //TODO throw exception if in reading mode
		
		#endregion
		
		#region Helper Structs
		
		protected enum MsgMode
		{
			ReadingMode = 0,
			WritingMode = 1,
			Build       = 2  //Redundant???
		}
        
		
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
