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
using kartMania.Structs;
using kartManiaCommons.Debug;
using kartManiaCommons.Network;

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
			ushort service = (ushort)NetLobbyService.CreateGameRoom;
			NetMsg msg     = new NetMsg(service);
			
			msg.Writer.Write(gameRoom.roomName  );
			msg.Writer.Write(gameRoom.maxPlayers);
			msg.Writer.Write(gameRoom.trackName );
			msg.Writer.Write(gameRoom.password  );
			
			SendMsg(msg);
		}
		
		public void SendName(string name)
		{
			NetMsg msg = new NetMsg((ushort)NetGameRoomService.UserSetName);
			
			msg.Writer.Write(name);
			
			SendMsg(msg);
		}
		
		public void SendChat(string chatText)
		{
			if (connected)
			{
				ushort service = (ushort)NetLobbyService.LobbyChat;
				NetMsg msg = new NetMsg(service);
				
				msg.Writer.Write(chatText);
				
				SendMsg(msg);
			}
			else
			{
				MessageBox.Show("You are not connected to the server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}	
		
		public void JoinGameRoom(uint gameRoomId, string password)
		{
			ushort service = (ushort)NetLobbyService.JoinGameRoom;
			NetMsg msg = new NetMsg(service);
			
			msg.Writer.Write(gameRoomId);
			msg.Writer.Write(password);
			
			SendMsg(msg);
		}
		
		public void LeaveGameRoom()
		{
			ushort service = (ushort)NetGameRoomService.LeaveGameRoom;
			NetMsg msg = new NetMsg(service);
			
			SendMsg(msg);
		}
	}
}
