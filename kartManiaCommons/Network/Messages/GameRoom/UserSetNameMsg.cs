/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.07.2013
 * Time: 16:46 
 * 
 */
using System;

namespace kartManiaCommons.Network.Messages.GameRoom
{
	/// <summary>
	/// Description of UserSetNameMsg.
	/// </summary>
	public class UserSetNameMsg:NetMsg
	{
		private const ushort service = (ushort)NetGameRoomService.UserSetName;
		
		#region Factory Registration
		
		static UserSetNameMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new UserSetNameMsg(data); });
		}
		
		#endregion
		
		#region Payload Members
		
		private string mUserName;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new UserSetNameMsg avalible for writing.
		/// </summary>
		public UserSetNameMsg()
			:base(service)
		{
			mUserName = "";
		}
		
		
		/// <summary>
		/// Makes a new UserSetNameMsg from the specified parameter.
		/// </summary>
		/// <param name="userName">The desired username</param>
		public UserSetNameMsg(string userName)
			:base(service)
		{
			//streamWriter.Write(chatMessage);
			mUserName = userName;
			Build();
		}
		
		/// <summary>
		/// Make a new UserSetNameMsg avalible for reading.
		/// </summary>
		public UserSetNameMsg(byte[] data)
			:base(service, data)
		{
			mUserName = streamReader.ReadString();
		}
		
		#endregion
		
		#region Properties
		
		public string UserName
		{
			get { return mUserName; }
			set
			{
				AssertWritingMode();
				mUserName = value;
			}
		}
		
		#endregion
		
		#region Public Methods
		
		public override void Build()
		{
			base.Build();
			
			streamWriter.Write(mUserName);
		}
		
		#endregion
	}
}
