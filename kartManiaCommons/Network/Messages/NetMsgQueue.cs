/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 06.09.2012
 * Time: 15:00
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using kartManiaCommons.Debug;

namespace kartManiaCommons.Network.Messages
{
    public class NetMsgQueue
    {
        private byte[] mBuffer;

        private int mFirst =  0;
        private int mLast  = -1;
        private int mSize;
        private int mGrowSize;

        private const int mDefaultSize     = 4096;
        private const int mDefaultGrowSize = 4096;
        
        private ByteUnion union = new ByteUnion();
        
        
        #region Factory related
        
        private static   Dictionary<ushort, QueueConstructor> mNetMsgConstructors = new Dictionary<ushort, QueueConstructor>();
        public  delegate NetMsg QueueConstructor(byte[] data);
        
        public  static void RegisterMsgTypes()
        {
        	Assembly asm = Assembly.GetAssembly(typeof(NetMsg));
		  
			var typesArray = asm.GetTypes();
			
			foreach(Type type in typesArray)
			{
				if (type.BaseType == typeof(NetMsg))
				{
					RuntimeHelpers.RunClassConstructor(type.TypeHandle);
				}
			}
        }
        
        public  static void RegisterMsgType(ushort service, QueueConstructor typeConstructor)
        {
        	if (!mNetMsgConstructors.ContainsKey(service))
        		mNetMsgConstructors.Add(service, typeConstructor);
        	
        	Logger.LogLine("Registered service: " + service + " \t with " + typeConstructor.Method.DeclaringType.Name, Logger.ServerLogLevel.Verbose);
        	//mNetMsgConstructors[service] = typeConstructor;
        }
        
        #endregion

        #region Constructors

        public NetMsgQueue()
        {
            mSize     = mDefaultSize;
            mGrowSize = mDefaultGrowSize;

            mBuffer = new byte[mSize];
        }

        public NetMsgQueue(int size)
        {
            mSize     = size;
            mGrowSize = mDefaultGrowSize;

            mBuffer = new byte[mSize];
        }

        public NetMsgQueue(int size, int growSize)
        {
            mSize     = size;
            mGrowSize = growSize;

            mBuffer = new byte[mSize];
        }

        #endregion

        #region Public Methods

        public bool Enqueue(byte[] data)
        {
            if (SpaceLeft < data.Length)
            {
                //see if we can free up some space
                if (UnusedSpace > data.Length )
                    FreeUpSpace();
                else //if not make the buffer bigger
                    GrowBuffer();
                //BUG: if buffer dosent grow enough
            }

            //Copy the new data to the queue
            for (int i = 0; i < data.Length; i++)
                mBuffer[i + mLast + 1] = data[i];

            mLast += data.Length;

            return true;
        }
        
        public bool Enqueue(byte[] data, int length)
        {
            if (SpaceLeft < length)
            {
                //see if we can free up some space
                if (UnusedSpace > length )
                    FreeUpSpace();
                else //if not make the buffer bigger
                    GrowBuffer();
                //BUG: if buffer dosent grow enough
            }

            //Copy the new data to the queue
            for (int i = 0; i < length; i++)
                mBuffer[i + mLast + 1] = data[i];

            mLast += length;

            return true;
        }

        public byte DequeueAsByte()
        {
            return mBuffer[mFirst++];
        }
        
        public string DequeueAsString()
        {
        	string ret = Encoding.ASCII.GetString(mBuffer, mFirst, mLast - mFirst + 1 );
        	
        	mFirst = mLast+1;
        	
        	return ret;
        }
            
        /// <summary>
        /// Decodes the next message from the queue
        /// </summary>
        /// <returns>Returns the next message</returns>
        public NetMsg DequeueAsNetMsg()
        {
        	ushort service = ConvertBytes(mBuffer[mFirst  ], mBuffer[mFirst+1]);
        	ushort length  = ConvertBytes(mBuffer[mFirst+2], mBuffer[mFirst+3]);     	
        	
        	if ( length + 4 > UsedSpace )        	
        		return null;
        	
        	byte[] data = new byte[length];
        	mFirst += 4;
        	
        	for(int i = 0; i < length; i++)
        		data[i] = mBuffer[mFirst+i]; //TODO Optimize make mFirst += 4 from the begining EDIT: Done
        	
        	mFirst += length;
        	
        	NetMsg msg = new NetMsg(service, data);
        	//NetMsg msg = mNetMsgConstructors[service](data);
        	
        	return msg;
        }

        #endregion

        #region Private Methods

        private int SpaceLeft   { get { return mSize - mLast - 1;  } }

        private int UsedSpace   { get { return mLast - mFirst + 1; } }

        private int UnusedSpace { get { return mSize - UsedSpace;  } }

        /// <summary>
        /// Free some space by pulling the array to the left, deleting unnecesary data in the process.
        /// </summary>
        private void FreeUpSpace()
        {
            for (int i = mFirst; i <= mLast; i++)
                mBuffer[i - mFirst] = mBuffer[i];

            mLast -= mFirst;
            mFirst = 0;
        }

        /// <summary>
        /// Redimension the buffer because the queue has become full
        /// </summary>
        private void GrowBuffer()
        {
            mSize += mGrowSize;

            byte[] tmpArray = new byte[mSize];

            for (int i = 0; i < UsedSpace; i++)
                tmpArray[i] = mBuffer[i + mFirst];

            mBuffer = tmpArray;

            mLast -= mFirst;
            mFirst = 0;
        }
        
        private ushort ConvertBytes(byte x, byte y)
        {
        	//ByteUnion union = new ByteUnion();
        	
        	union.byteA = x;
        	union.byteB = y;
        	
        	return union.shrotVal;
        }

        #endregion
        
        #region Helper Union
        
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
