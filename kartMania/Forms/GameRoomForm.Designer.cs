/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 14.09.2012
 * Time: 15:40 
 * 
 */
namespace kartMania.Forms
{
	partial class GameRoomForm
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
			this.playersListView = new System.Windows.Forms.ListView();
			this.nameHeader = new System.Windows.Forms.ColumnHeader();
			this.lagHeader = new System.Windows.Forms.ColumnHeader();
			this.startButton = new System.Windows.Forms.Button();
			this.leaveButton = new System.Windows.Forms.Button();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// playersListView
			// 
			this.playersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.nameHeader,
									this.lagHeader});
			this.playersListView.FullRowSelect = true;
			this.playersListView.GridLines = true;
			this.playersListView.Location = new System.Drawing.Point(12, 12);
			this.playersListView.Name = "playersListView";
			this.playersListView.Size = new System.Drawing.Size(478, 294);
			this.playersListView.TabIndex = 0;
			this.playersListView.UseCompatibleStateImageBehavior = false;
			this.playersListView.View = System.Windows.Forms.View.Details;
			// 
			// nameHeader
			// 
			this.nameHeader.Text = "Player";
			this.nameHeader.Width = 155;
			// 
			// lagHeader
			// 
			this.lagHeader.Text = "Latency";
			// 
			// startButton
			// 
			this.startButton.Enabled = false;
			this.startButton.Location = new System.Drawing.Point(334, 312);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 23);
			this.startButton.TabIndex = 1;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			// 
			// leaveButton
			// 
			this.leaveButton.Location = new System.Drawing.Point(415, 312);
			this.leaveButton.Name = "leaveButton";
			this.leaveButton.Size = new System.Drawing.Size(75, 23);
			this.leaveButton.TabIndex = 2;
			this.leaveButton.Text = "Leave";
			this.leaveButton.UseVisualStyleBackColor = true;
			// 
			// statusStrip
			// 
			this.statusStrip.BackColor = System.Drawing.SystemColors.Control;
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel,
									this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 338);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.statusStrip.Size = new System.Drawing.Size(502, 22);
			this.statusStrip.TabIndex = 3;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
			this.toolStripStatusLabel.Text = "Status:";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(106, 17);
			this.statusLabel.Text = "Waiting for players";
			// 
			// GameRoomForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(502, 360);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.leaveButton);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.playersListView);
			this.Name = "GameRoomForm";
			this.Text = "GameRoomForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameRoomFormFormClosed);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.Button leaveButton;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.ColumnHeader lagHeader;
		private System.Windows.Forms.ColumnHeader nameHeader;
		private System.Windows.Forms.ListView playersListView;
	}
}
