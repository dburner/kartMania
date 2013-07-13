/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.07.2013
 * Time: 15:40 
 * 
 */
using System;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of GameRoomDestroyedMsg.
	/// </summary>
	public class GameRoomDestroyedMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.GameRoomDestroyed;
		
		#region Factory Registration
		
		static GameRoomDestroyedMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new GameRoomDestroyedMsg(data); });
		}
		
		#endregion
		
		#region Payload Members
		
		private uint mGameRoomId;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new GameRoomDestroyedMsg avalible for writing.
		/// </summary>
		public GameRoomDestroyedMsg()
			:base(service)
		{
			mGameRoomId = 0;
		}
		
		/// <summary>
		/// Makes a new GameRoomDestroyedMsg from the specified parameter.
		/// </summary>
		/// <param name="chatMessage">The GameRoomId</param>
		public GameRoomDestroyedMsg(uint gameRoomId)
			:base(service)
		{
			mGameRoomId = gameRoomId;
			//Build();
		}
		
		/// <summary>
		/// Make a new GameRoomDestroyedMsg avalible for reading.
		/// </summary>
		public GameRoomDestroyedMsg(byte[] data)
			:base(service, data)
		{
			mGameRoomId = streamReader.ReadUInt32();
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
		
		#endregion
		
		#region Public Methods
		
		public override void Build()
		{
			base.Build();
			
			streamWriter.Write(mGameRoomId);
		}
		
		#endregion
	}
}
