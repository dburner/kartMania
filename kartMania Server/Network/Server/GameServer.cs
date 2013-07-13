/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.09.2012
 * Time: 19:47 
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using kartManiaCommons.Debug;
using kartManiaCommons.Network;
using kartManiaCommons.Network.Messages;
using kartManiaCommons.Network.Messages.Lobby;

namespace kartManiaServer.Network
{
	/// <summary>
	/// Description of GameServer.
	/// </summary>
	public class GameServer:NetServer
	{
		private UnnamedLobby 		 unnamedLobby;
		private MainLobby    		 mainLobby;
		private LinkedList<GameRoom> gameRoomsList;
		 
		public GameServer()
		{
			unnamedLobby  = new UnnamedLobby();
			mainLobby	  = new MainLobby();
			gameRoomsList = new LinkedList<GameRoom>();
			
			NetMsgQueue.RegisterMsgTypes();
		}
		
		public override void Start()
		{
			base.Start();
			
			unnamedLobby.OnMoveClientToLobby += new UnnamedLobby.MoveClientToLobbyEventHandler(MovePlayerToLobby);
			mainLobby.OnCreateGameRoom 	     += new MainLobby.CreateGameRoomEventHandler(PlayerCreateGameRoom);
			mainLobby.OnPlayerJoinGameRoom   += new MainLobby.JoinGameRoomEventHandler(JoinGameRoom);
			
			Logger.LogLine("Server started");
			
			Work();
		}
		
		/// <summary>
		/// Main application loop
		/// </summary>
		private void Work()
		{
			while(true)
			{
				System.Threading.Thread.Sleep(100);
				Commands();
			}
		}
		
		private void Commands()
		{
			string cmd = Console.ReadLine();
						
			switch (cmd)
			{
				case "gr_count":
					Console.Out.WriteLine(gameRoomsList.Count);
					break;
				case "gr_list":
					Console.Write("room to list = ");

					int roomIndex;
					int.TryParse(Console.ReadLine(), out roomIndex);
					GameRoom room = gameRoomsList.ElementAt(roomIndex);
					
					if (room != null)
						room.PrintPlayers();
					break;
				default:
					Console.Out.WriteLine("Not a valid command");
					break;
			}
		}
		
		protected override void OnClientConnect(NetPlayer player)
		{
			unnamedLobby.AddPlayer(player);
			Logger.LogLine("Client connected");
		}
		
		private void PlayerCreateGameRoom(GameRoom gameRoom)
		{
			lock(gameRoomsList)
				gameRoomsList.AddLast(gameRoom);
			
			gameRoom.GameRoomDestroyed += new GameRoom.GameRoomDestroyedEventHandler(DestroyGameRoom);
		}
		
		private void JoinGameRoom(NetPlayer player, uint gameRoomId)
		{
			foreach(GameRoom room in gameRoomsList)
			{
				if (room.RoomId == gameRoomId)
				{
					room.AddPlayer(player);
					break;
				}
			}
		}
		
		private void MovePlayerToLobby(NetPlayer player)
		{
			mainLobby.AddPlayer(player);
			
			SendGameRoomsList(player);
		}
		
		private void SendGameRoomsList(NetPlayer player)
		{
			GameRoomsListMsg msg = new GameRoomsListMsg();
			
			msg.RoomsInfo = gameRoomsList.Select( room => room.RoomInfo).ToArray();
			
			player.SendMsg(msg);
		}
		
		private void DestroyGameRoom(GameRoom gameRoom)
		{
			lock(gameRoomsList)
				gameRoomsList.Remove(gameRoom);
		}
	}
}
