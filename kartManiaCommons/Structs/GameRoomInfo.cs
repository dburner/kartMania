﻿/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 11.09.2012
 * Time: 21:11 
 * 
 */
using System;

namespace kartManiaCommons.Structs
{
	/// <summary>
	/// Description of GameRoomInfo.
	/// </summary>
	public class GameRoomInfo
	{
		public string roomName;
		public string trackName;
		public string password;
		public string ownerName;
		
		public byte   maxPlayers;
		public byte	  curPlayers;
		public uint	  gameRoomId;
		
		public GameRoomInfo()
		{
			roomName  = "";
			trackName = "";
			password  = "";
			ownerName = "";
		}
	}
}
