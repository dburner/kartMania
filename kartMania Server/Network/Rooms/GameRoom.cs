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
using kartManiaCommons.Network.Messages;
using kartManiaCommons.Network.Messages.Lobby;
using kartManiaCommons.Structs;

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
		
		public delegate void GameRoomDestroyedEventHandler(GameRoom gameRoom);
		public event 		 GameRoomDestroyedEventHandler GameRoomDestroyed;
		
		public delegate void OnGameRoomPlayersJoinLeave(GameRoom gameRoom);
		public event 		 OnGameRoomPlayersJoinLeave PlayerJoined;
		public event 		 OnGameRoomPlayersJoinLeave PlayerLeft;
		
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
			
			if (PlayerJoined != null)
				PlayerJoined(this);
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
				if (PlayerLeft != null)
					PlayerLeft(this);
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
			if (GameRoomDestroyed != null)
				GameRoomDestroyed(this);
		}
		
		private void ConfirmPlayerJoin (NetPlayer player)
		{
			JoinGameRoomSuccesMsg msg = new JoinGameRoomSuccesMsg();
			
			msg.IsOwner = player == owner;
			msg.PlayersInformation = new PlayerInfo[playersList.Count];
			
			int count = 0;
			
			foreach(NetPlayer otherPlayer in playersList)
			{
				PlayerInfo info = new PlayerInfo();
				
				info.playerName = otherPlayer.Name;
				info.playerId   = otherPlayer.PlayerId;
				
				msg.PlayersInformation[count++] = info;
			}
			
			player.SendMsg(msg);
		}		
		
		private void NotifyPlayerJoined(NetPlayer player)
		{
			GameRoomPlayerJoinedMsg msg =
				new GameRoomPlayerJoinedMsg(player.PlayerId, player.Name);
			
			SendMsgToAllExcept(msg, player);
		}
		
		private void NotifyPlayerLeft  (NetPlayer player)
		{
			GameRoomPlayerLeftMsg msg =
				new GameRoomPlayerLeftMsg(player.PlayerId, player.Name);
			
			SendMsgToAll(msg);
		}
		
		
		#region Debug only
		
		public void PrintPlayers()
		{
			foreach(NetPlayer player in playersList)
			{
				Console.WriteLine(player.Name);
			}
		}
		
		#endregion
	}
}
