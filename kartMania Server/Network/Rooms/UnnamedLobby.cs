﻿/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.09.2012
 * Time: 20:18 
 * 
 */
using System;
using kartManiaCommons.Network;

namespace kartManiaServer.Network
{
	/// <summary>
	/// Description of UnnamedLobby.
	/// </summary>
	public class UnnamedLobby:NetRoom
	{
		public delegate void MoveClientToLobbyEventHandler(NetPlayer player);
		
		public event         MoveClientToLobbyEventHandler OnMoveClientToLobby;
		
		public UnnamedLobby()
		{
		
		}
		
		protected override void OnClientConnect(NetPlayer client)
		{
			base.OnClientConnect(client);
			
			client.NewGameRoomMsg += new NewRoomMsgEventHandler(OnMessageReceived);
		}
		
		protected override void OnClientRemove(NetPlayer client)
		{
			base.OnClientRemove(client);
			
			client.NewGameRoomMsg -= new NewRoomMsgEventHandler(OnMessageReceived);
		}
		
		protected override void OnMessageReceived(NetPlayer player, NetMsg msg)
		{
			if ( msg.Service == (ushort)NetGameRoomSerice.SetName )
			{
				player.Name = msg.Reader.ReadString();
				if ( OnMoveClientToLobby != null)
					OnMoveClientToLobby(player);
				
				RemovePlayer(player);
			}
		}		
		
		protected override void OnClientDisconnect(NetPlayer client)
		{
			base.OnClientDisconnect(client);
			
			client.NewGameRoomMsg -= new NewRoomMsgEventHandler(OnMessageReceived);
		}
	}
}