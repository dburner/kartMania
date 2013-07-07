/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.07.2013
 * Time: 16:22 
 * 
 */
using System;
using kartManiaCommons.Structs;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of GameRoomsListMsg.
	/// </summary>
	public class GameRoomsListMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.GameRoomsList;
		
		#region Factory Registration
		
		static GameRoomsListMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new GameRoomsListMsg(data); });
		}
		
		#endregion
		
		#region Payload Members
		
		private uint           mRoomsCount;
		private GameRoomInfo[] mRoomsInfo;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new GameRoomsListMsg avalible for writing.
		/// </summary>
		public GameRoomsListMsg()
			:base(service)
		{
			
		}
		
		/// <summary>
		/// Make a new GameRoomsListMsg avalible for reading.
		/// </summary>
		public GameRoomsListMsg(byte[] data)
			:base(service, data)
		{			
			mRoomsCount = streamReader.ReadUInt32();
			mRoomsInfo  = new GameRoomInfo[mRoomsCount];
			
			for (uint i = 0; i < mRoomsCount; i++)
			{
				GameRoomInfo gri = new GameRoomInfo();
				
				gri.roomName   = streamReader.ReadString();
				gri.maxPlayers = streamReader.ReadByte  ();	
				gri.trackName  = streamReader.ReadString();
				gri.password   = streamReader.ReadString();
				gri.ownerName  = streamReader.ReadString();
				gri.gameRoomId = streamReader.ReadUInt32();
				gri.curPlayers = streamReader.ReadByte  ();
				
				mRoomsInfo[i] = gri;
			}
		}
		
		#endregion
		
		#region Properties
		
		public uint RoomsCount
		{
			get { return mRoomsCount; }
			set
			{
				AssertWritingMode();
				mRoomsCount = value;
			}
		}
		
		public GameRoomInfo[] RoomsInfo
		{
			get { return mRoomsInfo; }
			set
			{
				AssertWritingMode();
				mRoomsInfo = value;
			}
		}
		
		#endregion
		
		#region Public Methods
		
		public override void Build()
		{
			base.Build();
			
			mRoomsCount = (uint)mRoomsInfo.Length;
			
			for(uint i = 0; i < mRoomsCount; i++)
			{
				streamWriter.Write(mRoomsInfo[i].roomName  );
				streamWriter.Write(mRoomsInfo[i].maxPlayers);
				streamWriter.Write(mRoomsInfo[i].trackName );
				streamWriter.Write(mRoomsInfo[i].password  );
				streamWriter.Write(mRoomsInfo[i].ownerName );
				streamWriter.Write(mRoomsInfo[i].gameRoomId);
				streamWriter.Write(mRoomsInfo[i].curPlayers);
			}
		}
		
		#endregion
	}
}
