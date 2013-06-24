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
			NetMsgQueue.RegisterMsgType(service, CreateInstance);
		}
		
		private static NetMsg CreateInstance(byte[] data)
		{
			return new JoinGameRoomMsg(data);
		}
		
		#endregion
		
		#region Payload Members
		#endregion
		
		#region Constructors
		
		public JoinGameRoomMsg(byte[] data)
			:base(service, data)
		{
			
		}
		
		#endregion
		
		#region Properties
		#endregion
		
		#region Public Methods
		#endregion
		
	}
}
