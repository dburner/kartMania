/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 14.09.2012
 * Time: 15:40 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using kartMania.Game;
using kartManiaCommons.Structs;

namespace kartMania.Forms
{
	/// <summary>
	/// Description of GameRoomForm.
	/// </summary>
	public partial class GameRoomForm : Form
	{
		private enum Status:byte
		{
			Joining		  = 1,
			Synchronizing = 2,
			Starting	  = 3,
		}
				
		public static GameRoomForm Instance   { get; private set; }
		//public static Status	   GameStatus { get; private set; }
		public static uint 		   RoomId 	  { get; set; }
		
		public static bool FormOpened
		{
			get { return formOpened;  }
			set { formOpened = value; }
		}
			
		private static volatile bool formOpened;
		
		#region Constructors
		
		public GameRoomForm()
		{
			InitializeComponent();
			
			FormOpened = true;
			Instance   = this;
		}
		
		public GameRoomForm(string[] playerNames, uint[] playerIds, bool isOwner)
		{
			InitializeComponent();
			if (isOwner)
				EnableControls();
			
			AddPlayers(playerNames, playerIds);
			
			FormOpened = true;
			Instance = this;
		}
		
		public GameRoomForm(PlayerInfo[] playerInfoArray, bool isOwner)
		{
			InitializeComponent();
			if (isOwner)
				EnableControls();
			
			AddPlayers(playerInfoArray);
			
			FormOpened = true;
			Instance = this;
		}
		
		#endregion
		
		#region Public methods
		
		public void SetStatus(string status)
		{
			this.Invoke( new MethodInvoker( () => statusLabel.Text = status ) );
		}
		
		public void AddPlayer(string player, uint playerId)
		{	
			this.Invoke( new MethodInvoker( () => AddPlayerInvoke(player, playerId) ) );
		}
		
		public void RemovePlayer(string player)
		{
			this.Invoke(new MethodInvoker( () => RemovePlayerInvoke(player) ) );
		}
		
		#endregion
		
		//TODO RemovePlayer by id not by name
		private void RemovePlayerInvoke(string player)
		{
			for(int i = 0; i < playersListView.Items.Count; i++)
			{
				ListViewItem item = playersListView.Items[i];
				
				if (item.Text == player)
				{
					playersListView.Items.Remove(item);
					break;
				}
			}
		}
		
		private void AddPlayers(string[] playerNames, uint[] playerIds)
		{
			for(int i = 0; i < playerNames.Length; i++)
			{
				ListViewItem item = new ListViewItem(playerNames[i]);
				item.Tag = playerIds[i];
				
				playersListView.Items.Add(item);
			}
			
			playersListView.Sort();
		}
		
		private void AddPlayers(PlayerInfo[] playersInfoArray)
		{
			for(int i = 0; i < playersInfoArray.Length; i++)
			{
				ListViewItem item = new ListViewItem(playersInfoArray[i].playerName);
				item.Tag = playersInfoArray[i].playerId;
				
				playersListView.Items.Add(item);
			}
			
			playersListView.Sort();
		}
		
		private void AddPlayerInvoke(string playerName, uint playerId)
		{
			ListViewItem item = new ListViewItem(playerName);
			item.Tag = playerId;
				
			playersListView.Items.Add(item);
		}
		
		private void EnableControls()
		{
			startButton.Enabled = true;
		}
		
		void GameRoomFormFormClosed(object sender, FormClosedEventArgs e)
		{
			FormOpened = false;
			Engine.Network.LeaveGameRoom();
		}
		
		void StartButtonClick(object sender, EventArgs e)
		{
			
		}
	}
}
