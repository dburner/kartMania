/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.07.2013
 * Time: 16:09 
 * 
 */
using System;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of GameRoomUpdatePlayersMsg.
	/// </summary>
	public class GameRoomUpdatePlayersMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.GameRoomUpdatePlayers;
		
		#region Factory Registration
		
		static GameRoomUpdatePlayersMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new GameRoomUpdatePlayersMsg(data); });
		}
		
		#endregion
		
		#region Payload Members
		
		private uint mGameRoomId;
		private byte mPlayersCount;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new GameRoomUpdatePlayersMsg avalible for writing.
		/// </summary>
		public GameRoomUpdatePlayersMsg()
			:base(service)
		{
			mGameRoomId   = 0;
			mPlayersCount = 0;
		}
		
		/// <summary>
		/// Make a new GameRoomUpdatePlayersMsg from the parameters
		/// </summary>
		/// <param name="gameRoomId"></param>
		/// <param name="playersCount">The new players count from the specified game room</param>
		public GameRoomUpdatePlayersMsg(uint gameRoomId, byte playersCount)
			:base(service)
		{
			mGameRoomId   = gameRoomId;
			mPlayersCount = playersCount;
			
			//Build();
		}
		
		/// <summary>
		/// Make a new GameRoomUpdatePlayersMsg avalible for reading.
		/// </summary>
		public GameRoomUpdatePlayersMsg(byte[] data)
			:base(service, data)
		{
			mGameRoomId   = streamReader.ReadUInt32();
			mPlayersCount = streamReader.ReadByte  ();
		}	                           
		
		#endregion
		
		#region Properties
		
		public uint GameRoomId
		{
			get { return mGameRoomId; }
			set 
			{
				AssertWritingMode();
				mGameRoomId = value;
			}
		}
		
		public byte PlayersCount
		{
			get { return mPlayersCount; }
			set 
			{
				AssertWritingMode();
				mPlayersCount = value;
			}
		} 
		
		#endregion
		
		#region Public Methods
		
		public override void Build()
		{
			base.Build();
			
			streamWriter.Write(mGameRoomId  );
			streamWriter.Write(mPlayersCount);
		}
		
		#endregion
	}
}
