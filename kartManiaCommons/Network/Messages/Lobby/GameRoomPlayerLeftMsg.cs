/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.07.2013
 * Time: 16:04 
 * 
 */
using System;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of GameRoomPlayerLeftMsg.
	/// </summary>
	public class GameRoomPlayerLeftMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.GameRoomPlayerLeft;
		
		#region Factory Registration
		
		static GameRoomPlayerLeftMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new GameRoomPlayerLeftMsg(data); });
		}
		
		#endregion
		
		#region Payload Members
		
		private uint   mPlayerId;
		private string mPlayerName;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new GameRoomPlayerLeftMsg avalible for writing.
		/// </summary>
		public GameRoomPlayerLeftMsg()
			:base(service)
		{
			mPlayerId   = 0;
			mPlayerName = "";
		}
		
		/// <summary>
		/// Make a new GameRoomPlayerLeftMsg from the parameters
		/// </summary>
		public GameRoomPlayerLeftMsg(uint playerId, string playerName)
			:base(service)
		{
			mPlayerId   = playerId;
			mPlayerName = playerName;
			
			Build();
		}
		
		/// <summary>
		/// Make a new GameRoomPlayerLeftMsg avalible for reading.
		/// </summary>
		public GameRoomPlayerLeftMsg(byte[] data)
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
