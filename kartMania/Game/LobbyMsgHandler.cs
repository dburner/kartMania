/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 14.09.2012
 * Time: 16:04 
 * 
 */
using System;
using kartMania.Forms;
using kartMania.Network;
using kartMania.Structs;
using kartManiaCommons.Debug;
using kartManiaCommons.Network;

namespace kartMania.Game
{
	/// <summary>
	/// Description of LobbyMsgHandler.
	/// </summary>
	public static class LobbyMsgHandler
	{
		public static void HandleMsg(NetMsg msg)
		{
			NetLobbyService service = (NetLobbyService)msg.Service;
			Logger.LogLine(service.ToString());
			switch (service)
			{
				case NetLobbyService.LobbyChat:
					LobbyChat(msg);
					break;
				case NetLobbyService.GameRoomCreated:
					GameRoomCreated(msg);
					break;
				case NetLobbyService.JoinGameRoomSucces:
					JoinGameRoomSucces(msg);
					break;
				case NetLobbyService.JoinGameRoomFail:
					break;
				case NetLobbyService.GameRoomPlayerJoined:
					GameRoomPlayerJoined(msg);
					break;
				case NetLobbyService.GameRoomPlayerLeft:
					GameRoomPlayerLeft(msg);
					break;
				case NetLobbyService.GameRoomUpdatePlayers:
					GameRoomUpdatePlayers(msg);
					break;
				case NetLobbyService.GameRoomDestroyed:
					GameRoomDestroyed(msg);
					break;
				case NetLobbyService.GameRoomsList:
					GameRoomsList(msg);
					break;
				//default:
				//	break;
			}
		}
		
		private static void LobbyChat(NetMsg msg)
		{
			string chat = msg.Reader.ReadString();
			
			MainForm.Instance.PasteChatText(chat);
		}
		
		private static void GameRoomCreated(NetMsg msg)
		{
			GameRoomInfo gri = new GameRoomInfo();
			
			gri.roomName   = msg.Reader.ReadString();
			gri.maxPlayers = msg.Reader.ReadByte  ();
			gri.trackName  = msg.Reader.ReadString();
			gri.password   = msg.Reader.ReadString();
			gri.ownerName  = msg.Reader.ReadString();
			gri.gameRoomId = msg.Reader.ReadUInt32();
			gri.curPlayers = msg.Reader.ReadByte  ();
			
			MainForm.Instance.AddGameRoom(gri);
		}
		
		private static void JoinGameRoomSucces(NetMsg msg)
		{
			bool     isOwner	  = msg.Reader.ReadBoolean();
			byte     playersCount = msg.Reader.ReadByte();
			string[] playerNames  = new string[playersCount];
			uint[]   playerIds	  = new uint[playersCount];
			
			for(int i = 0; i < playersCount; i++)
			{
				playerNames[i] = msg.Reader.ReadString();
				playerIds[i]   = msg.Reader.ReadUInt32();
			}
			
			MainForm.Instance.CreateGameRoomForm(playerNames, playerIds, isOwner);
		}
		
		private static void GameRoomPlayerJoined(NetMsg msg)
		{
			string playerName = msg.Reader.ReadString();
			uint   playerId   = msg.Reader.ReadUInt32();
			
			if (GameRoomForm.FormOpened)
				GameRoomForm.Instance.AddPlayer(playerName, playerId);
		}
		
		private static void GameRoomPlayerLeft(NetMsg msg)
		{
			string player = msg.Reader.ReadString();
			
			GameRoomForm.Instance.RemovePlayer(player);
		}
		
		private static void GameRoomUpdatePlayers(NetMsg msg)
		{
			uint roomId  = msg.Reader.ReadUInt32();
			byte players = msg.Reader.ReadByte();
			
			MainForm.Instance.UpdateGameRoom(roomId, players);
		}
		
		private static void GameRoomDestroyed(NetMsg msg)
		{
			uint roomId = msg.Reader.ReadUInt32();
			
			MainForm.Instance.DestroyGameRoom(roomId);
			
			if (GameRoomForm.FormOpened)
				GameRoomForm.Instance.SetStatus("Host left the room!");
			
			//TODO fix, check gameroomform for room id
		}
		
		private static void GameRoomsList(NetMsg msg)
		{
			int roomsCount = msg.Reader.ReadInt32();
			
			for(int i = 0; i < roomsCount; i++)
			{
				GameRoomInfo gri = new GameRoomInfo();
				
				gri.roomName   = msg.Reader.ReadString();
				gri.maxPlayers = msg.Reader.ReadByte  ();
				gri.trackName  = msg.Reader.ReadString();
				gri.password   = msg.Reader.ReadString();
				gri.ownerName  = msg.Reader.ReadString();
				gri.gameRoomId = msg.Reader.ReadUInt32();
				gri.curPlayers = msg.Reader.ReadByte  ();
			
				MainForm.Instance.AddGameRoom(gri);
			}
		}
	}
}