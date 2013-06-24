/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 23.06.2013
 * Time: 14:41 
 * 
 */
using System;
using System.IO;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of LobbyChatMsg.
	/// </summary>
	public class LobbyChatMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.LobbyChat;
		
		#region Factory Registration
		
		static LobbyChatMsg()
		{
			NetMsgQueue.RegisterMsgType(service, CreateInstance);
		}
		
		private static NetMsg CreateInstance(byte[] data)
		{
			return new LobbyChatMsg(data);
		}
		
		#endregion
		
		#region Payload members
		
		private string mMessage;
		
		#endregion		
		
		#region Constructors

		/// <summary>
		/// Make a new LobbyChatMsg avalible for writing.
		/// </summary>
		public LobbyChatMsg()
			:base(service)
		{
			
		}
		
		/// <summary>
		/// Makes a new LobbyChatMsg from the specified parameter.
		/// </summary>
		/// <param name="chatMessage">The message to send</param>
		public LobbyChatMsg(string chatMessage)
			:base(service)
		{
			//streamWriter.Write(chatMessage);
			mMessage = chatMessage;
			Build();
		}
		
		/// <summary>
		/// Makes a new LobbyChatMsg avalible for reading.
		/// </summary>
		public LobbyChatMsg(byte[] data)
			:base(service, data)
		{
			mMessage = streamReader.ReadString();
		}
		
		#endregion
		
		#region Properties
		
		public string Message
		{
			get { return mMessage;  }
			set
			{
				if (mMsgMode != MsgMode.WritingMode)
					return;
				mMessage = value; 
			}
		}
		
		#endregion
		
		#region Public Methods
		
		public override void Build()
		{
			base.Build();
			
			streamWriter.Write(mMessage);
		}
		
		#endregion
	}
}
