/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 11.09.2012
 * Time: 20:54 
 * 
 */
using System;
using System.Windows.Forms;
using kartMania.Network;
using kartManiaCommons.Debug;
using kartManiaCommons.Network;
using kartManiaCommons.Network.Messages;
using kartManiaCommons.Network.Messages.GameRoom;
using kartManiaCommons.Network.Messages.Lobby;
using kartManiaCommons.Structs;

namespace kartMania.Game
{
	/// <summary>
	/// Description of NetworkEngine.
	/// </summary>
	public class NetEngine:NetManager
	{		
		public NetEngine()
		{
		}
		
		public void CreateGameRoom(GameRoomInfo gameRoom)
		{
			CreateGameRoomMsg msg = new CreateGameRoomMsg();
			msg.GameRoomInfo = gameRoom;
			
			SendMsg(msg);
		}
		
		public void SendName(string name)
		{
			UserSetNameMsg msg = new UserSetNameMsg(name);
			
			SendMsg(msg);
		}
		
		public void SendChat(string chatText)
		{
			if (connected)
			{
				LobbyChatMsg msg = new LobbyChatMsg(chatText);
				
				SendMsg(msg);
			}
			else
			{
				MessageBox.Show("You are not connected to the server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		public void JoinGameRoom(uint gameRoomId, string password)
		{
			JoinGameRoomMsg msg = new JoinGameRoomMsg();
			
			msg.GameRoomId 		 = gameRoomId;
			msg.GameRoomPassword = password;
			
			SendMsg(msg);
		}
		
		public void LeaveGameRoom()
		{
			LeaveGameRoomMsg msg = new LeaveGameRoomMsg();
			
			SendMsg(msg);
		}
	}
}
