/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.09.2012
 * Time: 15:54 
 * 
 */
using System;
using System.Collections;

namespace kartManiaServer.Network
{
	/// <summary>
	/// A base class for room management
	/// </summary>
	public class NetRoom //Room types: UnnamedLobby, Lobby, GameRoom,
	{
		private static uint uniqueId = 0;
		
		private uint idNumber;
		
		protected volatile ArrayList playersList; //volatile
		
		public uint RoomId { get { return idNumber; } }
		
		public NetRoom()
		{
			idNumber    = uniqueId++;
			playersList = new ArrayList();
		}
		
		public void AddPlayer(NetPlayer player)
		{
			lock(playersList)
			{
				if (!playersList.Contains(player))
					playersList.Add(player);
			}
			
			player.ClientDisconnect += new ClientDisconnectEventHandler(OnClientDisconnect);
			
			OnClientConnect(player);
		}
		
		public void RemovePlayer(NetPlayer player)
		{
			lock(playersList)
				playersList.Remove(player);
			
			player.ClientDisconnect -= new ClientDisconnectEventHandler(OnClientDisconnect);
			
			OnClientRemove(player);
		}
		
		/// <summary>
		/// Obsolete
		/// </summary>
		/// <param name="tickDelta"></param>
		public virtual void OnTick(double tickDelta)
		{
			//get NetMsg from queues and execute OnMessageRec
			for( int i = 0; i < playersList.Count; i++ )
			{
				NetPlayer player = (NetPlayer)playersList[i];				
				NetMsg    msg    = player.GetNextMessage();
				
				if (msg == null) continue;
				
				OnMessageReceived(player, msg); //TODO Use ThreadPool to execute this function
			}
		}
		
		protected void SendMsgToAll(NetMsg msg)
		{
			for(int i = 0; i < playersList.Count; i++)
			{
				NetPlayer player = (NetPlayer)playersList[i];
				
				player.SendMsg(msg);
			}
		}
		
		protected void SendMsgToAllExcept(NetMsg msg, NetPlayer player)
		{
			for(int i = 0; i < playersList.Count; i++)
			{
				NetPlayer otherPlayer = (NetPlayer)playersList[i];
				
				if (otherPlayer != player)
					otherPlayer.SendMsg(msg);
			}
		}
		
//		protected virtual void OnClientPreConnect(NetPlayer client)
//		{
//			
//		}
		
		protected virtual void OnClientConnect(NetPlayer client)
		{
			
		}
		
		protected virtual void OnClientDisconnect(NetPlayer client)
		{
			RemovePlayer(client);
		}
		
		protected virtual void OnClientRemove(NetPlayer client)
		{
			
		}
		
		protected virtual void OnMessageReceived(NetPlayer client, NetMsg msg)
		{
			
		}
	}
}
