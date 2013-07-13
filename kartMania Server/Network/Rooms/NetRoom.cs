/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.09.2012
 * Time: 15:54 
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using kartManiaCommons.Network.Messages;

namespace kartManiaServer.Network
{
	/// <summary>
	/// A base class for room management
	/// </summary>
	public class NetRoom //Room types: UnnamedLobby, Lobby, GameRoom,
	{
		private static uint uniqueId = 0;
		
		private uint idNumber;
		
		protected volatile LinkedList<NetPlayer> playersList;
		
		public uint RoomId { get { return idNumber; } }
		
		public NetRoom()
		{
			idNumber    = uniqueId++;
			playersList = new LinkedList<NetPlayer>();
		}
		
		public void AddPlayer(NetPlayer player)
		{
			lock(playersList)
			{
				if (!playersList.Contains(player))
					playersList.AddLast(player);
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
				
		protected void SendMsgToAll(NetMsg msg)
		{
			foreach(NetPlayer player in playersList)
			{
				player.SendMsg(msg);
			}
		}
		
		protected void SendMsgToAllExcept(NetMsg msg, NetPlayer player)
		{
			foreach(NetPlayer otherPlayer in playersList)
			{
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
