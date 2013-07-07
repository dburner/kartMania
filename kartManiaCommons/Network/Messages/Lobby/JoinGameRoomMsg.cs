/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 23.06.2013
 * Time: 15:28 
 * 
 */
using System;
using kartManiaCommons.Debug;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of JoinGameRoomMsg.
	/// </summary>
	public class JoinGameRoomMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.JoinGameRoom;
		
		#region Factory Registration
		
		static JoinGameRoomMsg()
		{
		  //NetMsgQueue.RegisterMsgType(service, CreateInstance);
			NetMsgQueue.RegisterMsgType(service, (data) => { return new JoinGameRoomMsg(data); });
		}
		
//		private static NetMsg CreateInstance(byte[] data)
//		{
//			return new JoinGameRoomMsg(data);
//		}
		
		#endregion
		
		#region Payload Members
		
		private uint   mGameRoomId;
		private string mPassword;
			
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new LobbyChatMsg avalible for writing.
		/// </summary>
		public JoinGameRoomMsg()
			:base(service)
		{
			
		}
		
		/// <summary>
		/// Makes a new JoinGameRoomMsg avalible for reading.
		/// </summary>
		public JoinGameRoomMsg(byte[] data)
			:base(service, data)
		{
			mGameRoomId = streamReader.ReadUInt32();
			mPassword   = streamReader.ReadString();
		}
		
		#endregion
		
		#region Properties
		
		public uint   GameRoomId
		{
			get { return mGameRoomId; }
			set
			{
				AssertWritingMode();
				mGameRoomId = value;
			}
		}
		
		public string GameRoomPassword
		{
			get { return mPassword; }
			set
			{
				AssertWritingMode();
				mPassword = value;
			}
		}
		
		#endregion
		
		#region Public Methods
		
		public override void Build()
		{
			base.Build();
			
			streamWriter.Write(mGameRoomId);
			streamWriter.Write(mPassword);
		}
		
		#endregion
	}
}
