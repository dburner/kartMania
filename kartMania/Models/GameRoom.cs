/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 24.10.2013
 * Time: 14:34 
 * 
 */
using System;

namespace kartMania.Models
{
	/// <summary>
	/// Description of GameRoom.
	/// </summary>
	public class GameRoomModel
	{
		#region Members

		private string m_roomName;
		private string m_trackName;	
		private string m_ownerName;
		private bool   m_hasPassword;
		private int    m_playersCount;
		
		#endregion
		
		public GameRoomModel()
		{
		}
		
		#region Properties
		
		public string RoomName
		{
			get { return m_roomName;  }
			set { m_roomName = value; }
		}			
		public string TrackName 
		{
			get { return m_trackName;  }
			set { m_trackName = value; }
		}	
		public string OwnerName 
		{
			get { return m_ownerName;  }
			set { m_ownerName = value; }
		}	
		public bool   HasPassword 
		{
			get { return m_hasPassword;  }
			set { m_hasPassword = value; }
		}		
		public int    PlayersCount 
		{
			get { return m_playersCount;  }
			set { m_playersCount = value; }
		}
		
		#endregion
	}
}