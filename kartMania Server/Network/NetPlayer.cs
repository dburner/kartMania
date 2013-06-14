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
using kartManiaServer.Network;
using kartManiaServer.Structs;

namespace kartManiaServer.Network
{
	public delegate void NewRoomMsgEventHandler      (NetPlayer player, NetMsg msg );
	public delegate void ClientDisconnectEventHandler(NetPlayer player);
	/// <summary>
	/// Description of NetPlayer.
	/// </summary>
	public class NetPlayer:NetClient
	{
		private static uint uniqueId = 0;
		private static ushort firstLobbyValue = (ushort)NetLobbyService.FirstValue;
		private static ushort  lastLobbyValue = (ushort)NetLobbyService.LastValue;
		
		private uint    idNumber;
		
		public string   Name       { get; set;}
		public uint     PlayerId   { get { return idNumber; } }
		public GameRoom JoinedRoom { get; set; }
		
		public event NewRoomMsgEventHandler NewLobbyMsg;
		public event NewRoomMsgEventHandler NewGameRoomMsg;
		public event ClientDisconnectEventHandler ClientDisconnect;
		
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
		
//		public new void SendMsg(NetMsg msg)
//		{
//			Logger.LogLine("Send to: " + Name + " " + msg.Service.ToString());
//			base.SendMsg(msg);
//		}
		
		protected override void OnDisconnect()
		{
			base.OnDisconnect();
			
			if (ClientDisconnect != null)
				ClientDisconnect(this);
		}
		
		protected override void OnDataReceived()
		{
			base.OnDataReceived();
			
			NetMsg msg = msgQueue.DequeueAsNetMsg();
			
			if (msg != null)
				//OnMessageReceived(msg);
				ThreadPool.QueueUserWorkItem( s => OnMessageReceived(msg) );
		}
		
		protected void OnMessageReceived(NetMsg msg)
		{
			//Logger.LogLine(Name + " " + msg.Service);
			
			if (IsLobbyService(msg.Service))
			{
				if (NewLobbyMsg != null)
					NewLobbyMsg(this, msg);
			}					
			else
			{
				if (NewGameRoomMsg != null)
					NewGameRoomMsg(this, msg);
			}
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
