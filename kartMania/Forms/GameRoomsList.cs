/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 04.09.2012
 * Time: 14:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using kartMania.Game;
using kartMania.Structs;
using kartManiaCommons.Debug;

namespace kartMania.Forms
{
	/// <summary>
	/// Description of GameRoomsList.
	/// </summary>
	public partial class GameRoomsList : UserControl
	{		
		public GameRoomsList()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
		}
		
		#region Public Methods
		
		public void AddGameRoom(GameRoomInfo gameRoom)
		{
			this.Invoke( new MethodInvoker( () => AddGameRoomInvoke(gameRoom) ) );
		}
		
		public void UpdateGameRoom(uint roomId, byte players)
		{
			this.Invoke( new MethodInvoker( () => UpdateGameRoomInvoke(roomId, players) ) );
		}
		
		public void DestroyGameRoom(uint roomId)
		{
			this.Invoke( new MethodInvoker( () => DestroyGameRoomInvoke(roomId) ) );
		}
		
		#endregion
		
		#region Private Methods
		
		private void AddGameRoomInvoke(GameRoomInfo gameRoom)
		{
			string[] items = new string[5];
			
			items[0] = gameRoom.roomName;
			items[1] = gameRoom.curPlayers.ToString() + "/" + gameRoom.maxPlayers.ToString();
			items[2] = gameRoom.trackName;
			items[3] = gameRoom.ownerName;
			items[4] = gameRoom.password != "" ? "Yes" : "No";
			
			ListViewItem gameRoomItem = new ListViewItem(items);
			
			gameRoomItem.Tag = gameRoom;
			
			gamesListView.Items.Add(gameRoomItem);
			
		}
		
		private void UpdateGameRoomInvoke(uint roomId, byte players)
		{
			for(int i = 0; i < gamesListView.Items.Count; i++)
			{
				ListViewItem item = gamesListView.Items[i];
				GameRoomInfo gri  = (GameRoomInfo)item.Tag;
				if (gri.gameRoomId == roomId)
				{
					item.SubItems[1].Text = players.ToString() + "/" + gri.maxPlayers.ToString();
					break;
				}
			}
		}
		
		private void DestroyGameRoomInvoke(uint roomId)
		{
			for(int i = 0; i < gamesListView.Items.Count; i++)
			{
				ListViewItem item = gamesListView.Items[i];
				GameRoomInfo gri  = (GameRoomInfo)item.Tag;
				if (gri.gameRoomId == roomId)
				{
					gamesListView.Items.Remove(item);
					break;
				}
			}
		}
		
		private void GamesListViewDoubleClick(object sender, EventArgs e)
		{
			if (gamesListView.SelectedItems != null )
			{
				GameRoomInfo gri = (GameRoomInfo)gamesListView.SelectedItems[0].Tag;
				JoinGameRoom(gri);
			}
				
		}
		
		private void JoinGameRoom(GameRoomInfo gri)
		{
			if (!GameRoomForm.FormOpened)
				Engine.Network.JoinGameRoom(gri.gameRoomId, "");
			else
				GameRoomForm.Instance.BringToFront();
		}
		
		private void GameRoomsListResize(object sender, EventArgs e)
		{
			//TODO resize Rooms Header
			//gamesListView.Columns[0].Width += 5;
		}
		
		#endregion
	}
}
