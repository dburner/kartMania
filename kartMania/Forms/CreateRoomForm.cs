/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 06.09.2012
 * Time: 19:12 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using kartMania.Game;
using kartMania.Structs;

namespace kartMania.Forms
{
	/// <summary>
	/// Description of CreateRoomForm.
	/// </summary>
	public partial class CreateRoomForm : Form
	{
		public static bool FormOpened { get; set; }
		public static CreateRoomForm Instance { get; set; }
		
		public CreateRoomForm()
		{
			InitializeComponent();
			
			Instance = this;
			FormOpened = true;
			
			trackComboBox.SelectedIndex = 0;
		}
		
		void PasswordCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			passwordTextBox.Enabled = !passwordTextBox.Enabled;
		}
		
		void OkButtonClick(object sender, EventArgs e)
		{
			GameRoomInfo gri = new GameRoomInfo();
			
			gri.roomName   = roomNameTextBox.Text;
			gri.maxPlayers = (byte)maxPlayersUpDown.Value;
			gri.trackName  = trackComboBox.Text;
			
			if (passwordCheckBox.Checked)
				gri.password = passwordTextBox.Text;
			
			Engine.Network.CreateGameRoom(gri);
			
			this.Close();
		}	
		
		void CancelButtonClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void CreateRoomFormFormClosed(object sender, FormClosedEventArgs e)
		{
			FormOpened = false;
			this.Dispose();
		}
	}
}
