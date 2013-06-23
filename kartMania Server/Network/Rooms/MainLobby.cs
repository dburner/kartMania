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
using kartManiaServer.Structs;

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
			NetMsg sendMsg = new NetMsg(msg.Service); //NetLobbyChatservice
			
			string chat = "[" + player.Name + "]: " + msg.Reader.ReadString();
			
			sendMsg.Writer.Write(chat);
			SendMsgToAll(sendMsg);
		}
		
		private void JoinGameRoom   (NetPlayer player, NetMsg msg)
		{
			uint gameRoomId = msg.Reader.ReadUInt32();
			
			if (player.JoinedRoom == null) //TODO comment or uncomment this EDIT: WTF???
				OnPlayerJoinGameRoom(player, gameRoomId);
		}
		
		private void CreateGameRoom (NetPlayer player, NetMsg msg)
		{
			GameRoomInfo gri = new GameRoomInfo();
			
			gri.roomName   = msg.Reader.ReadString();
			gri.maxPlayers = msg.Reader.ReadByte  ();
			gri.trackName  = msg.Reader.ReadString();
			gri.password   = msg.Reader.ReadString();
					
			GameRoom gameRoom = new GameRoom(player, gri);
			
			gameRoom.OnPlayerJoin 		+= new GameRoom.OnGameRoomPlayersJoinLeave   (gameRoom_OnPlayerJoin);
			gameRoom.OnPlayerLeft 		+= new GameRoom.OnGameRoomPlayersJoinLeave	 (gameRoom_OnPlayerLeft);
			gameRoom.OnGameRoomDestroy 	+= new GameRoom.OnGameRoomDestroyEventHandler(gameRoom_OnGameRoomDestroy);
			
			gri.ownerName  = player.Name;
			gri.gameRoomId = gameRoom.RoomId;
			gri.curPlayers = 1;
			
			player.JoinedRoom = gameRoom;
			
			OnCreateGameRoom(gameRoom);
			
			NotifyGameRoomCreated(gri);
		}
		
		private void NotifyGameRoomCreated(GameRoomInfo gri)
		{
			ushort service = (ushort)NetLobbyService.GameRoomCreated;
			NetMsg msg 	   = new NetMsg(service);
			
			msg.Writer.Write(gri.roomName  );
			msg.Writer.Write(gri.maxPlayers);
			msg.Writer.Write(gri.trackName );
			msg.Writer.Write(gri.password  );
			msg.Writer.Write(gri.ownerName );
			msg.Writer.Write(gri.gameRoomId);
			msg.Writer.Write(gri.curPlayers);
			
			SendMsgToAll(msg);
		}
		
		private void gameRoom_OnPlayerJoin(GameRoom gameRoom)
		{
			ushort service = (ushort)NetLobbyService.GameRoomUpdatePlayers;
			NetMsg msg = new NetMsg(service);
			
			msg.Writer.Write(gameRoom.RoomId);
			msg.Writer.Write(gameRoom.PlayersCount);
			
			SendMsgToAll(msg);
		}
		
		private void gameRoom_OnPlayerLeft(GameRoom gameRoom)
		{
			ushort service = (ushort)NetLobbyService.GameRoomUpdatePlayers;
			NetMsg msg = new NetMsg(service);
			
			msg.Writer.Write(gameRoom.RoomId);
			msg.Writer.Write(gameRoom.PlayersCount);
			
			SendMsgToAll(msg);
		}
		
		private void gameRoom_OnGameRoomDestroy(GameRoom gameRoom)
		{
			ushort service = (ushort)NetLobbyService.GameRoomDestroyed;
			NetMsg msg = new NetMsg(service);
			
			msg.Writer.Write(gameRoom.RoomId);
			
			SendMsgToAll(msg);
		}
	}
}
