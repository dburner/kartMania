/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.09.2012
 * Time: 16:29 
 * 
 */
using System;
using System.Collections;
using kartManiaCommons.Network;
using kartManiaServer.Structs;

namespace kartManiaServer.Network
{
	/// <summary>
	/// Description of GameRoom.
	/// </summary>
	public class GameRoom:NetRoom
	{
		private enum GameRoomState:byte
		{
			Joining    = 1,
			Syncronize = 2,
			Racing 	   = 3
		}
		
		private NetPlayer owner;
		
		
		// TODO rewrite all delegates as
		// GameRoomEventHandler(GameRoom sender, GameRoomEventArgs)
		
		public delegate void OnGameRoomDestroyEventHandler(GameRoom gameRoom);
		public event 		 OnGameRoomDestroyEventHandler OnGameRoomDestroy;
		
		public delegate void OnGameRoomPlayersJoinLeave(GameRoom gameRoom);
		public event 		 OnGameRoomPlayersJoinLeave OnPlayerJoin;
		public event 		 OnGameRoomPlayersJoinLeave OnPlayerLeft;
		
		public GameRoomInfo RoomInfo     { get; set; }
		public int 			PlayersCount { get { return playersList.Count; } }
		
		public GameRoom()
		{
		}
		
		public GameRoom(NetPlayer ownerPlayer, GameRoomInfo roomInfo)
		{
			owner    = ownerPlayer;
			RoomInfo = roomInfo;
			AddPlayer(owner);
			
			//TODO Override constructorul de baza din netroom cu numarul corect de jucatori 
		}
		
		protected override void OnClientConnect(NetPlayer client)
		{
			base.OnClientConnect(client);
			
			client.NewGameRoomMsg += new NewRoomMsgEventHandler(OnMessageReceived);
			client.JoinedRoom = this;
			
			ConfirmPlayerJoin(client);
			NotifyPlayerJoined(client);
			
			if (OnPlayerJoin != null)
				OnPlayerJoin(this);
		}
		
		protected override void OnClientRemove(NetPlayer client)
		{
			base.OnClientRemove(client);
			
			client.NewGameRoomMsg -= new NewRoomMsgEventHandler(OnMessageReceived);
			
			client.JoinedRoom = null;
			
			if (client == owner)
				DestroyGameRoom();
			else
			{
				NotifyPlayerLeft(client);
				if (OnPlayerLeft != null)
					OnPlayerLeft(this);
			}		
		}
		
		protected override void OnMessageReceived(NetPlayer client, NetMsg msg)
		{
			base.OnMessageReceived(client, msg);
			NetGameRoomService service = (NetGameRoomService)msg.Service;
			
			switch (service)
			{
				case NetGameRoomService.LeaveGameRoom:
					RemovePlayer(client);
					break;
			}
		}
		
		private void DestroyGameRoom()
		{
			
			OnGameRoomDestroy(this);
		}
		
		private void ConfirmPlayerJoin(NetPlayer player)
		{
			ushort service = (ushort)NetLobbyService.JoinGameRoomSucces;
			NetMsg msg = new NetMsg(service);
			
			bool isOwner = player == owner;
			
			msg.Writer.Write(isOwner);    
			msg.Writer.Write((byte)playersList.Count);
			
			for(int i = 0; i < playersList.Count; i++)
			{
				NetPlayer otherPlayer = (NetPlayer)playersList[i];
				
				msg.Writer.Write(otherPlayer.Name);
				msg.Writer.Write(otherPlayer.PlayerId);
			}
			
			player.SendMsg(msg);
		}
		
		private void NotifyPlayerJoined(NetPlayer player)
		{
			ushort service = (ushort)NetLobbyService.GameRoomPlayerJoined;
			NetMsg msg = new NetMsg(service);
			
			msg.Writer.Write(player.Name);
			msg.Writer.Write(player.PlayerId);
			
			SendMsgToAllExcept(msg, player);
		}
		
		private void NotifyPlayerLeft(NetPlayer player)
		{
			ushort service = (ushort)NetLobbyService.GameRoomPlayerLeft;
			NetMsg msg = new NetMsg(service);
			
			msg.Writer.Write(player.Name);
			msg.Writer.Write(player.PlayerId);
			
			SendMsgToAll(msg);
		}
		
		
		#region Debug only
		
		public void PrintPlayers()
		{
			for(int i = 0; i < playersList.Count; i++)
			{
				NetPlayer player = (NetPlayer)playersList[i];
				Console.WriteLine(player.Name);
			}
		}
		
		#endregion
	}
}
