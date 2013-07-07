/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.07.2013
 * Time: 15:51 
 * 
 */
using System;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of GameRoomPlayerJoinedMsg.
	/// </summary>
	public class GameRoomPlayerJoinedMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.GameRoomPlayerJoined;
		
		#region Factory Registration
		
		static GameRoomPlayerJoinedMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new GameRoomPlayerJoinedMsg(data); });
		}
		
		#endregion
		
		#region Payload Members
		
		private uint   mPlayerId;
		private string mPlayerName;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new GameRoomPlayerJoinedMsg avalible for writing.
		/// </summary>
		public GameRoomPlayerJoinedMsg()
			:base(service)
		{
			mPlayerId   = 0;
			mPlayerName = "";
		}
		
		/// <summary>
		/// Make a new GameRoomPlayerJoinedMsg from the parameters
		/// </summary>
		public GameRoomPlayerJoinedMsg(uint playerId, string playerName)
			:base(service)
		{
			mPlayerId   = playerId;
			mPlayerName = playerName;
			
			Build();
		}
		
		/// <summary>
		/// Make a new GameRoomPlayerJoinedMsg avalible for reading.
		/// </summary>
		public GameRoomPlayerJoinedMsg(byte[] data)
			:base(service, data)
		{
			mPlayerId   = streamReader.ReadUInt32();
			mPlayerName = streamReader.ReadString();
		}	                           
		
		#endregion
		
		#region Properties
		
		public uint   PlayerId
		{
			get { return mPlayerId; }
			set 
			{
				AssertWritingMode();
				mPlayerId = value;
			}
		}
		
		public string PlayerName
		{
			get { return mPlayerName; }
			set 
			{
				AssertWritingMode();
				mPlayerName = value;
			}
		} 
		
		#endregion
		
		#region Public Methods
		
		public override void Build()
		{
			base.Build();
			
			streamWriter.Write(mPlayerId);
			streamWriter.Write(mPlayerName);
		}
		
		#endregion
	}
}
