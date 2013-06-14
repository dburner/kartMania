/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 04.09.2012
 * Time: 14:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace kartMania.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.disconnectButton = new System.Windows.Forms.Button();
			this.connectButton = new System.Windows.Forms.Button();
			this.createButton = new System.Windows.Forms.Button();
			this.joinButton = new System.Windows.Forms.Button();
			this.gameRoomsList = new kartMania.Forms.GameRoomsList();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.chatInputBox = new System.Windows.Forms.TextBox();
			this.chatBox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Location = new System.Drawing.Point(12, 12);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(648, 415);
			this.tabControl.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.tabPage2.Controls.Add(this.button1);
			this.tabPage2.Controls.Add(this.nameTextBox);
			this.tabPage2.Controls.Add(this.nameLabel);
			this.tabPage2.Controls.Add(this.disconnectButton);
			this.tabPage2.Controls.Add(this.connectButton);
			this.tabPage2.Controls.Add(this.createButton);
			this.tabPage2.Controls.Add(this.joinButton);
			this.tabPage2.Controls.Add(this.gameRoomsList);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(640, 389);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Games";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.nameTextBox.Location = new System.Drawing.Point(214, 362);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(106, 20);
			this.nameTextBox.TabIndex = 13;
			this.nameTextBox.Text = "dburner";
			// 
			// nameLabel
			// 
			this.nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.nameLabel.Location = new System.Drawing.Point(168, 360);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(40, 23);
			this.nameLabel.TabIndex = 12;
			this.nameLabel.Text = "Name:";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// disconnectButton
			// 
			this.disconnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.disconnectButton.Location = new System.Drawing.Point(87, 360);
			this.disconnectButton.Name = "disconnectButton";
			this.disconnectButton.Size = new System.Drawing.Size(75, 23);
			this.disconnectButton.TabIndex = 11;
			this.disconnectButton.Text = "Disconnect";
			this.disconnectButton.UseVisualStyleBackColor = true;
			this.disconnectButton.Click += new System.EventHandler(this.DisconnectButtonClick);
			// 
			// connectButton
			// 
			this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.connectButton.Location = new System.Drawing.Point(6, 360);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(75, 23);
			this.connectButton.TabIndex = 10;
			this.connectButton.Text = "Connect";
			this.connectButton.UseVisualStyleBackColor = true;
			this.connectButton.Click += new System.EventHandler(this.ConnectButtonClick);
			// 
			// createButton
			// 
			this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.createButton.Location = new System.Drawing.Point(478, 360);
			this.createButton.Name = "createButton";
			this.createButton.Size = new System.Drawing.Size(75, 23);
			this.createButton.TabIndex = 9;
			this.createButton.Text = "Create room";
			this.createButton.UseVisualStyleBackColor = true;
			this.createButton.Click += new System.EventHandler(this.CreateButtonClick);
			// 
			// joinButton
			// 
			this.joinButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.joinButton.Location = new System.Drawing.Point(559, 359);
			this.joinButton.Name = "joinButton";
			this.joinButton.Size = new System.Drawing.Size(75, 23);
			this.joinButton.TabIndex = 8;
			this.joinButton.Text = "Join Room";
			this.joinButton.UseVisualStyleBackColor = true;
			// 
			// gameRoomsList
			// 
			this.gameRoomsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.gameRoomsList.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.gameRoomsList.Location = new System.Drawing.Point(6, 6);
			this.gameRoomsList.Name = "gameRoomsList";
			this.gameRoomsList.Size = new System.Drawing.Size(628, 348);
			this.gameRoomsList.TabIndex = 7;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.tabPage1.Controls.Add(this.chatInputBox);
			this.tabPage1.Controls.Add(this.chatBox);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(640, 389);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "Chat";
			// 
			// chatInputBox
			// 
			this.chatInputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.chatInputBox.Location = new System.Drawing.Point(6, 363);
			this.chatInputBox.Name = "chatInputBox";
			this.chatInputBox.Size = new System.Drawing.Size(628, 20);
			this.chatInputBox.TabIndex = 1;
			this.chatInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatInputBoxKeyDown);
			// 
			// chatBox
			// 
			this.chatBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.chatBox.Location = new System.Drawing.Point(6, 6);
			this.chatBox.Multiline = true;
			this.chatBox.Name = "chatBox";
			this.chatBox.Size = new System.Drawing.Size(628, 351);
			this.chatBox.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(397, 359);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 14;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(672, 439);
			this.Controls.Add(this.tabControl);
			this.DoubleBuffered = true;
			this.Name = "MainForm";
			this.Text = "kartMania";
			this.tabControl.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox chatBox;
		private System.Windows.Forms.TextBox chatInputBox;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Button disconnectButton;
		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.Button createButton;
		private System.Windows.Forms.Button joinButton;
		private kartMania.Forms.GameRoomsList gameRoomsList;
	}
}
