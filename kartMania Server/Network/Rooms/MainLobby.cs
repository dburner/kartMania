/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.09.2012
 * Time: 18:52 
 * 
 */
using System;
using System.Runtime.CompilerServices;
using kartManiaCommons.Debug;
using kartManiaCommons.Network;
using kartManiaCommons.Network.Messages;
using kartManiaCommons.Network.Messages.Lobby;
using kartManiaCommons.Structs;

namespace kartManiaServer.Network
{
	/// <summary>
	/// Description of MainLobby.
	/// </summary>
	public  class MainLobby:NetRoom
	{
		public MainLobby()
		{
		}
		
		public delegate void CreateGameRoomEventHandler(GameRoom gameRoom);
		public event 		 CreateGameRoomEventHandler OnCreateGameRoom;
		
		public delegate void JoinGameRoomEventHandler(NetPlayer player, uint gameRoomId);
		public event 		 JoinGameRoomEventHandler OnPlayerJoinGameRoom;
		
		protected override void OnClientConnect(NetPlayer client)
		{
			base.OnClientConnect(client);
			
			client.NewLobbyMsg += new NewRoomMsgEventHandler(OnMessageReceived);
			
			Logger.LogLine(client.Name + " connected.");
			//TODO Send welcome message
		}
		
		protected override void OnMessageReceived(NetPlayer client, NetMsg msg)
		{
			base.OnMessageReceived(client, msg);
			
			switch((NetLobbyService)msg.Service)
			{
				case NetLobbyService.LobbyChat:
					SendChatMessage(client, msg);
					break;
				case NetLobbyService.JoinGameRoom:
					JoinGameRoom(client, msg);
					break;
				case NetLobbyService.CreateGameRoom:
					CreateGameRoom(client, msg);
					break;
			}
		}
		
		private void SendChatMessage(NetPlayer player, NetMsg msg)
		{
			LobbyChatMsg recvMsg = (LobbyChatMsg)msg;
			LobbyChatMsg sendMsg = new LobbyChatMsg();
			
			string chat = "[" + player.Name + "]: " + recvMsg.Message;
			
			sendMsg.Message = chat;
			
			SendMsgToAll(sendMsg);
		}
		
		private void JoinGameRoom   (NetPlayer player, NetMsg msg)
		{
			JoinGameRoomMsg joinMsg = (JoinGameRoomMsg)msg;
			uint gameRoomId = joinMsg.GameRoomId;
			
			if (player.JoinedRoom == null) //TODO comment or uncomment this EDIT: WTF???
				OnPlayerJoinGameRoom(player, gameRoomId);
		}
		
		private void CreateGameRoom (NetPlayer player, NetMsg msg)
		{
			CreateGameRoomMsg createRoomMsg = (CreateGameRoomMsg)msg;
					
			GameRoom gameRoom = new GameRoom(player, createRoomMsg.GameRoomInfo);
			
			gameRoom.PlayerJoined 		+= new GameRoom.OnGameRoomPlayersJoinLeave   (gameRoom_OnPlayerJoin);
			gameRoom.PlayerLeft 		+= new GameRoom.OnGameRoomPlayersJoinLeave	 (gameRoom_OnPlayerLeft);
			gameRoom.GameRoomDestroyed 	+= new GameRoom.GameRoomDestroyedEventHandler(gameRoom_OnGameRoomDestroy);
			
			GameRoomInfo gri = createRoomMsg.GameRoomInfo;
			
			gri.ownerName  = player.Name;
			gri.gameRoomId = gameRoom.RoomId;
			gri.curPlayers = 1;
			
			player.JoinedRoom = gameRoom;
			
			OnCreateGameRoom(gameRoom);
			
			NotifyGameRoomCreated(gri);
		}
		
		private void NotifyGameRoomCreated(GameRoomInfo gri)
		{
			GameRoomCreatedMsg msg = new GameRoomCreatedMsg();
			msg.GameRoomInfo = gri;
			
			SendMsgToAll(msg);
		}
		
		private void gameRoom_OnPlayerJoin(GameRoom gameRoom)
		{
			GameRoomUpdatePlayersMsg msg = new GameRoomUpdatePlayersMsg();
			
			msg.GameRoomId   = gameRoom.RoomId;
			msg.PlayersCount = (byte)gameRoom.PlayersCount;
			
			SendMsgToAll(msg);
		}
		
		private void gameRoom_OnPlayerLeft(GameRoom gameRoom)
		{
			GameRoomUpdatePlayersMsg msg = new GameRoomUpdatePlayersMsg();
			
			msg.GameRoomId   = gameRoom.RoomId;
			msg.PlayersCount = (byte)gameRoom.PlayersCount;
			
			SendMsgToAll(msg);
		}
		
		private void gameRoom_OnGameRoomDestroy(GameRoom gameRoom)
		{
			GameRoomDestroyedMsg msg = new GameRoomDestroyedMsg();
			
			msg.GameRoomId = gameRoom.RoomId;
			
			SendMsgToAll(msg);
		}
	}
}
