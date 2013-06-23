/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 04.09.2012
 * Time: 14:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using kartMania.Game;
using kartMania.Network;
using kartMania.Structs;
using kartManiaCommons.Network;
using kartManiaCommons.Debug;

namespace kartMania.Forms
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public static MainForm Instance { get; set; }
		
		public MainForm()
		{
			InitializeComponent();
			
			Instance = this;
			
			CreateRoomForm.FormOpened = false;
		}
		
		public void PasteChatText(string chatText)
		{
			chatBox.Invoke( new MethodInvoker( () => chatBox.Text += chatText + Environment.NewLine) );
		}
		
		public void AddGameRoom(GameRoomInfo gri)
		{
			gameRoomsList.AddGameRoom(gri);
		}
		
		public void CreateGameRoomForm(string[] playerNames, uint[] playerIds, bool isOwner)
		{
			this.Invoke( new MethodInvoker( () => {
				GameRoomForm form = new GameRoomForm(playerNames, playerIds, isOwner);
				form.Show();            	
				}));
		}
		
		public void UpdateGameRoom(uint roomId, byte players)
		{
			gameRoomsList.UpdateGameRoom(roomId, players);
		}
		
		public void DestroyGameRoom(uint roomId)
		{
			gameRoomsList.DestroyGameRoom(roomId);
		}
		
		
		private void ConnectButtonClick   (object sender, EventArgs e)
		{
			Engine.Network.Connect("127.0.0.1", 1234);
			Engine.Network.SendName(nameTextBox.Text);
			Logger.LogLine("Connection: " + Engine.Network.Connected.ToString());
		}
		
		private void DisconnectButtonClick(object sender, EventArgs e)
		{
			Engine.Network.Disconnect();
		}
		
		private void CreateButtonClick    (object sender, EventArgs e)
		{
			if (!CreateRoomForm.FormOpened)
				new CreateRoomForm().Show();
			else
				CreateRoomForm.Instance.BringToFront();
		}
		
		private void ChatInputBoxKeyDown  (object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Engine.Network.SendChat(chatInputBox.Text);
				chatInputBox.Text = "";
			}
		}
		
		
		private void Button1Click(object sender, EventArgs e)
		{
			new GameForm().Show();
		}
	}
}
