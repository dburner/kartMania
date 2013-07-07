/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 29.06.2013
 * Time: 16:07 
 * 
 */
using System;

namespace kartManiaCommons.Structs
{
	/// <summary>
	/// Description of PlayerInfo.
	/// </summary>
	public class PlayerInfo
	{
		public uint   playerId;
		public string playerName;
		
		public PlayerInfo()
		{
			playerId   = 0;
			playerName = "";
		}
	}
}
