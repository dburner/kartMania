/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 04.09.2012
 * Time: 14:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace kartMania.Forms
{
	partial class GameRoomsList
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			this.gamesListView = new System.Windows.Forms.ListView();
			this.roomHeader = new System.Windows.Forms.ColumnHeader();
			this.playersHeader = new System.Windows.Forms.ColumnHeader();
			this.trackHeader = new System.Windows.Forms.ColumnHeader();
			this.OwnerHeader = new System.Windows.Forms.ColumnHeader();
			this.passwordHeader = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// gamesListView
			// 
			this.gamesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.roomHeader,
									this.playersHeader,
									this.trackHeader,
									this.OwnerHeader,
									this.passwordHeader});
			this.gamesListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gamesListView.FullRowSelect = true;
			this.gamesListView.GridLines = true;
			this.gamesListView.Location = new System.Drawing.Point(0, 0);
			this.gamesListView.Name = "gamesListView";
			this.gamesListView.Size = new System.Drawing.Size(495, 342);
			this.gamesListView.TabIndex = 0;
			this.gamesListView.UseCompatibleStateImageBehavior = false;
			this.gamesListView.View = System.Windows.Forms.View.Details;
			this.gamesListView.DoubleClick += new System.EventHandler(this.GamesListViewDoubleClick);
			// 
			// roomHeader
			// 
			this.roomHeader.Text = "Room name";
			this.roomHeader.Width = 104;
			// 
			// playersHeader
			// 
			this.playersHeader.Text = "Players";
			this.playersHeader.Width = 102;
			// 
			// trackHeader
			// 
			this.trackHeader.Text = "Track name";
			this.trackHeader.Width = 107;
			// 
			// OwnerHeader
			// 
			this.OwnerHeader.Text = "Owner";
			this.OwnerHeader.Width = 96;
			// 
			// passwordHeader
			// 
			this.passwordHeader.Text = "Password";
			this.passwordHeader.Width = 81;
			// 
			// GameRoomsList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gamesListView);
			this.DoubleBuffered = true;
			this.Name = "GameRoomsList";
			this.Size = new System.Drawing.Size(495, 342);
			this.Resize += new System.EventHandler(this.GameRoomsListResize);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ColumnHeader passwordHeader;
		private System.Windows.Forms.ColumnHeader OwnerHeader;
		private System.Windows.Forms.ColumnHeader trackHeader;
		private System.Windows.Forms.ColumnHeader playersHeader;
		private System.Windows.Forms.ColumnHeader roomHeader;
		private System.Windows.Forms.ListView gamesListView;
	}
}
