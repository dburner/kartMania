/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 08.09.2012
 * Time: 15:29 
 * 
 */
using System;
using System.Threading;
using kartMania.Game;
using kartManiaCommons.Debug;
using kartManiaCommons.Network;
using kartManiaCommons.Network.Messages;

namespace kartMania.Network
{	
	/// <summary>
	/// Description of NetManager.
	/// </summary>
	public class NetManager:NetClient
	{
		//private Thread networkThread;
		
		//public delegate void NewNetMsgEventHandler( NetMsg msg );
		//public event 		 NewNetMsgEventHandler  NewNetMsg;
		
		private static ushort firstLobbyValue = (ushort)NetLobbyService.FirstValue;
		private static ushort  lastLobbyValue = (ushort)NetLobbyService.LastValue;
		
		public NetManager()
		{	
		}
		
		public new void Connect(string host, int port)
		{
			if ( !connected)
				base.Connect(host, port);
		}
		
		protected override void OnDataReceived()
		{
			NetMsg msg = msgQueue.DequeueAsNetMsg();
			
			if (msg != null)
				OnMessageReceived(msg);
		}
		
		protected virtual void OnMessageReceived(NetMsg msg)
		{
			if (IsLobbyService(msg.Service))
				LobbyMsgHandler.HandleMsg(msg);
			//NewNetMsg(msg);
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
