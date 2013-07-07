/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.07.2013
 * Time: 15:02 
 * 
 */
using System;
using kartManiaCommons.Structs;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of CreateGameRoomMsg.
	/// </summary>
	public class CreateGameRoomMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.CreateGameRoom;
		
		#region Factory Registration
		
		static CreateGameRoomMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new CreateGameRoomMsg(data); });
		}
		
		#endregion
		
		#region Payload Members
		
		private GameRoomInfo mGameRoomInfo;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new ... avalible for writing.
		/// </summary>
		public CreateGameRoomMsg()
			:base(service)
		{
			
		}
		
		/// <summary>
		/// Make a new ... avalible for reading.
		/// </summary>
		public CreateGameRoomMsg(byte[] data)
			:base(service, data)
		{
			mGameRoomInfo = new GameRoomInfo();
			
			mGameRoomInfo.roomName   = streamReader.ReadString();
			mGameRoomInfo.maxPlayers = streamReader.ReadByte  ();
			mGameRoomInfo.trackName  = streamReader.ReadString();
			mGameRoomInfo.password   = streamReader.ReadString();
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
		}
		
		#endregion
	}
}
