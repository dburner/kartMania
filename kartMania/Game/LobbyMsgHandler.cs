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
using kartManiaCommons.Debug;
using kartManiaCommons.Network;
using kartManiaCommons.Network.Messages;
using kartManiaCommons.Network.Messages.Lobby;
using kartManiaCommons.Structs;

namespace kartMania.Game
{
	/// <summary>
	/// Description of LobbyMsgHandler.
	/// </summary>
	public static class LobbyMsgHandler
	{
		public static void HandleMsg(NetMsg msg)
		{
			//TODO Use delegates array for all this
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
			LobbyChatMsg chatMsg = (LobbyChatMsg)msg;
			
			MainForm.Instance.PasteChatText(chatMsg.Message);
		}
		
		private static void GameRoomCreated(NetMsg msg)
		{
			GameRoomCreatedMsg gameRoomMsg = (GameRoomCreatedMsg)msg;
			
			MainForm.Instance.AddGameRoom(gameRoomMsg.GameRoomInfo);
		}
		
		private static void JoinGameRoomSucces(NetMsg msg)
		{
			JoinGameRoomSuccesMsg joinMsg = (JoinGameRoomSuccesMsg)msg;
				
			MainForm.Instance.CreateGameRoomForm(joinMsg.PlayersInformation, joinMsg.IsOwner);
		  //MainForm.Instance.CreateGameRoomForm(playerNames, playerIds, isOwner);
		}
		
		private static void GameRoomPlayerJoined(NetMsg msg)
		{
			GameRoomPlayerJoinedMsg playerJoinedMsg = (GameRoomPlayerJoinedMsg)msg;
			
			string playerName = playerJoinedMsg.PlayerName;
			uint   playerId   = playerJoinedMsg.PlayerId;
			
			if (GameRoomForm.FormOpened)
				GameRoomForm.Instance.AddPlayer(playerName, playerId);
		}
		
		private static void GameRoomPlayerLeft(NetMsg msg)
		{
			GameRoomPlayerLeftMsg leftMsg = (GameRoomPlayerLeftMsg)msg;
			
			GameRoomForm.Instance.RemovePlayer(leftMsg.PlayerName);
		}
		
		private static void GameRoomUpdatePlayers(NetMsg msg)
		{			
			GameRoomUpdatePlayersMsg updateMsg = (GameRoomUpdatePlayersMsg)msg;

			uint roomId  = updateMsg.GameRoomId;
			byte players = updateMsg.PlayersCount;
			
			MainForm.Instance.UpdateGameRoom(roomId, players);
		}
		
		private static void GameRoomDestroyed(NetMsg msg)
		{
			GameRoomDestroyedMsg destroyedMsg = (GameRoomDestroyedMsg)msg;
			
			uint roomId = destroyedMsg.GameRoomId;
			
			MainForm.Instance.DestroyGameRoom(roomId);
			
			if (GameRoomForm.FormOpened)
				GameRoomForm.Instance.SetStatus("Host left the room!");
			
			//TODO fix, check gameroomform for room id
		}
		
		private static void GameRoomsList(NetMsg msg)
		{
			GameRoomsListMsg roomsMsg = (GameRoomsListMsg)msg;
			
			for (int i = 0; i< roomsMsg.RoomsCount; i++)
			{
				MainForm.Instance.AddGameRoom(roomsMsg.RoomsInfo[i]);
			}
		}
	}
}