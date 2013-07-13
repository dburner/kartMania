/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.07.2013
 * Time: 16:53 
 * 
 */
using System;

namespace kartManiaCommons.Network.Messages.GameRoom
{
	/// <summary>
	/// Description of LeaveGameRoomMsg.
	/// </summary>
	public class LeaveGameRoomMsg:NetMsg
	{
		private const ushort service = (ushort)NetGameRoomService.LeaveGameRoom;
		
		#region Factory Registration
		
		static LeaveGameRoomMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new LeaveGameRoomMsg(data); });
		}
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Make a new ... avalible for writing.
		/// </summary>
		public LeaveGameRoomMsg()
			:base(service)
		{
			
		}
		
		/// <summary>
		/// Make a new ... avalible for reading.
		/// </summary>
		public LeaveGameRoomMsg(byte[] data)
			:base(service, data)
		{
			
		}
		
		#endregion		
	}
}
