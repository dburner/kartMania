/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 14.09.2012
 * Time: 16:04 
 * 
 */
using System;
using System.Collections.Generic;
using kartMania.Forms;
using kartMania.Network;
using kartMania.Exceptions;
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
	public class LobbyMsgHandler : INetMsgHandler //TODO: make this a non static class and implement INetMsgHandler
	{
		private delegate void MessageHandler(NetMsg msg);
		
		private Dictionary<NetLobbyService, MessageHandler> m_messageHandlers;
		
		public LobbyMsgHandler()
		{
			m_messageHandlers = new Dictionary<NetLobbyService, LobbyMsgHandler.MessageHandler>();
			
			m_messageHandlers.Add(NetLobbyService.LobbyChat,             LobbyChat            );
			m_messageHandlers.Add(NetLobbyService.GameRoomCreated,       GameRoomCreated      );
			m_messageHandlers.Add(NetLobbyService.JoinGameRoomSucces,    JoinGameRoomSucces   );
			m_messageHandlers.Add(NetLobbyService.GameRoomPlayerJoined,  GameRoomPlayerJoined );
			m_messageHandlers.Add(NetLobbyService.GameRoomPlayerLeft,    GameRoomPlayerLeft   );
			m_messageHandlers.Add(NetLobbyService.GameRoomUpdatePlayers, GameRoomUpdatePlayers);
			m_messageHandlers.Add(NetLobbyService.GameRoomDestroyed,     GameRoomDestroyed    );
			m_messageHandlers.Add(NetLobbyService.GameRoomsList,         GameRoomsList        );
		}
		
		public void HandleMsg(NetMsg msg)
		{
			//TODO Use delegates array for all this
			NetLobbyService service = (NetLobbyService)msg.Service;
			Logger.LogLine(service.ToString());
			
			if (m_messageHandlers.ContainsKey(service))
			{
				m_messageHandlers[service](msg);
			}
			else
			{
				throw new NoHandlerException();
			}		
		}
		
		private void LobbyChat(NetMsg msg)
		{
			LobbyChatMsg chatMsg = (LobbyChatMsg)msg;
			
			MainForm.Instance.PasteChatText(chatMsg.Message);
		}
		
		private void GameRoomCreated(NetMsg msg)
		{
			GameRoomCreatedMsg gameRoomMsg = (GameRoomCreatedMsg)msg;
			
			MainForm.Instance.AddGameRoom(gameRoomMsg.GameRoomInfo);
		}
		
		private void JoinGameRoomSucces(NetMsg msg)
		{
			JoinGameRoomSuccesMsg joinMsg = (JoinGameRoomSuccesMsg)msg;
				
			MainForm.Instance.CreateGameRoomForm(joinMsg.PlayersInformation, joinMsg.IsOwner);
		  //MainForm.Instance.CreateGameRoomForm(playerNames, playerIds, isOwner);
		}
		
		private void GameRoomPlayerJoined(NetMsg msg)
		{
			GameRoomPlayerJoinedMsg playerJoinedMsg = (GameRoomPlayerJoinedMsg)msg;
			
			string playerName = playerJoinedMsg.PlayerName;
			uint   playerId   = playerJoinedMsg.PlayerId;
			
			if (GameRoomForm.FormOpened)
				GameRoomForm.Instance.AddPlayer(playerName, playerId);
		}
		
		private void GameRoomPlayerLeft(NetMsg msg)
		{
			GameRoomPlayerLeftMsg leftMsg = (GameRoomPlayerLeftMsg)msg;
			
			GameRoomForm.Instance.RemovePlayer(leftMsg.PlayerName);
		}
		
		private void GameRoomUpdatePlayers(NetMsg msg)
		{			
			GameRoomUpdatePlayersMsg updateMsg = (GameRoomUpdatePlayersMsg)msg;

			uint roomId  = updateMsg.GameRoomId;
			byte players = updateMsg.PlayersCount;
			
			MainForm.Instance.UpdateGameRoom(roomId, players);
		}
		
		private void GameRoomDestroyed(NetMsg msg)
		{
			GameRoomDestroyedMsg destroyedMsg = (GameRoomDestroyedMsg)msg;
			
			uint roomId = destroyedMsg.GameRoomId;
			
			MainForm.Instance.DestroyGameRoom(roomId);
			
			if (GameRoomForm.FormOpened)
				GameRoomForm.Instance.SetStatus("Host left the room!");
			
			//TODO fix, check gameroomform for room id
		}
		
		private void GameRoomsList(NetMsg msg)
		{
			GameRoomsListMsg roomsMsg = (GameRoomsListMsg)msg;
			
			for (int i = 0; i< roomsMsg.RoomsCount; i++)
			{
				MainForm.Instance.AddGameRoom(roomsMsg.RoomsInfo[i]);
			}
		}
	}
}