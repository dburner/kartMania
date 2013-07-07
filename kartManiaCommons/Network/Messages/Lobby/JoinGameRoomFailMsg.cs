/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.07.2013
 * Time: 14:20 
 * 
 */
using System;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of JoinGameRoomFailMsg.
	/// </summary>
	public class JoinGameRoomFailMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.JoinGameRoomFail;
		
		#region Factory Registration
		
		static JoinGameRoomFailMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new JoinGameRoomFailMsg(data); });
		}
		
		#endregion
		
		#region Payload Members
		
		private string mErrorMessage;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new JoinGameRoomFailMsg avalible for writing.
		/// </summary>
		public JoinGameRoomFailMsg()
			:base(service)
		{
			mErrorMessage = "";
		}
		
		/// <summary>
		/// Make a new JoinGameRoomFailMsg avalible for reading.
		/// </summary>
		public JoinGameRoomFailMsg(byte[] data)
			:base(service, data)
		{
			mErrorMessage = streamReader.ReadString();
		}
		
		#endregion
		
		#region Properties
		
		public string ErrorMessage
		{
			get { return mErrorMessage; }
			set
			{
				AssertWritingMode();
				mErrorMessage = value;
			}
		}
		
		#endregion
		
		#region Public Methods
		
		public override void Build()
		{
			base.Build();
			
			streamWriter.Write(mErrorMessage);
		}
		
		#endregion
	}
}
