/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.07.2013
 * Time: 15:18 
 * 
 */
using System;
using kartManiaCommons.Structs;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of GameRoomCreatedMsg.
	/// </summary>
	public class GameRoomCreatedMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.GameRoomCreated;
		
		#region Factory Registration
		
		static GameRoomCreatedMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new GameRoomCreatedMsg(data); });
		}
		
		#endregion
		
		#region Payload Members
		
		private GameRoomInfo mGameRoomInfo;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new GameRoomDestroyedMsg avalible for writing.
		/// </summary>
		public GameRoomCreatedMsg()
			:base(service)
		{
			
		}
		
		/// <summary>
		/// Make a new GameRoomDestroyedMsg avalible for reading.
		/// </summary>
		public GameRoomCreatedMsg(byte[] data)
			:base(service, data)
		{
			mGameRoomInfo = new GameRoomInfo();
			
			mGameRoomInfo.roomName   = streamReader.ReadString();
			mGameRoomInfo.maxPlayers = streamReader.ReadByte  ();
			mGameRoomInfo.trackName  = streamReader.ReadString();
			mGameRoomInfo.password   = streamReader.ReadString();
			mGameRoomInfo.ownerName  = streamReader.ReadString();			
			mGameRoomInfo.gameRoomId = streamReader.ReadUInt32();
			mGameRoomInfo.curPlayers = streamReader.ReadByte  ();
		}
		
		#endregion
		
		#region Properties
		
		public GameRoomInfo GameRoomInfo
		{
			get { return mGameRoomInfo; }
			set
			{
				AssertWritingMode();
				mGameRoomInfo = value;
			}
		}
		
		#endregion
		
		#region Public Methods
		
		public override void Build()
		{
			base.Build();
			
			if (mGameRoomInfo == null)
				throw new NetMsgException("GameRoomInfo property is null");
			
			streamWriter.Write(mGameRoomInfo.roomName  );
			streamWriter.Write(mGameRoomInfo.maxPlayers);
			streamWriter.Write(mGameRoomInfo.trackName );
			streamWriter.Write(mGameRoomInfo.password  );
			streamWriter.Write(mGameRoomInfo.ownerName );
			streamWriter.Write(mGameRoomInfo.gameRoomId);
			streamWriter.Write(mGameRoomInfo.curPlayers);
		}			
		
		#endregion
	}
}
