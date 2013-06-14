/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.09.2012
 * Time: 18:44 
 * 
 */
using System;
using System.Threading;
using kartManiaCommons.Debug;
using kartManiaCommons.Network;
using kartManiaServer.Structs;

namespace kartManiaServer.Network
{
	/// <summary>
	/// Description of NetPlayer.
	/// </summary>
	public class NetPlayer:NetClient
	{
		private static uint uniqueId = 0;
		private static ushort firstLobbyValue = (ushort)NetLobbyService.FirstValue;
		private static ushort  lastLobbyValue = (ushort)NetLobbyService.LastValue;
		
		private static MainLobby lobby;
		
		public  static MainLobby Lobby { set { lobby = value; } }
		
		private uint    idNumber;
		private NetRoom gameRoom;
		
		public  string  Name { get; set;}
		public NetRoom  Room { get { return gameRoom; } set { gameRoom = value; } }
		
		public NetPlayer()
		{
			//name = "";
			Name = "";
			idNumber = uniqueId++;
		}
		
		public NetMsg GetNextMessage()
		{
			return msgQueue.DequeueAsNetMsg();
		}
		
		protected override void OnDataReceived()
		{
			base.OnDataReceived();
			
			NetMsg msg = msgQueue.DequeueAsNetMsg();
			
			if (msg != null)
				ThreadPool.QueueUserWorkItem( s => OnMessageReceived(msg) );
		}
		
		protected void OnMessageReceived(NetMsg msg)
		{
			if (IsLobbyService(msg.Service))
				   lobby.OnMessageReceived(this, msg);
			else
				gameRoom.OnMessageReceived(this, msg);
			//TODO rewrite rooms OnMessageReceived by using a NetPlayer event
		}
		
		private bool IsLobbyService(ushort service)
		{
			if ( firstLobbyValue < service && service < lastLobbyValue )
				return true;
			else
				return false;
		}
	}
}

//			gri.roomName   = msg.Reader.ReadString();
//			gri.maxPlayers = msg.Reader.ReadByte();
//			gri.trackName  = msg.Reader.ReadString();
//			gri.password   = msg.Reader.ReadString();
//			
//			Logger.LogLine(gri.roomName);
//			Logger.LogLine(gri.maxPlayers.ToString());
//			Logger.LogLine(gri.trackName);
//			Logger.LogLine(gri.password);