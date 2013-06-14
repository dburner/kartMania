/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 06.09.2012
 * Time: 19:12 
 * 
 */
namespace kartMania.Forms
{
	partial class CreateRoomForm
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
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.nameLabel = new System.Windows.Forms.Label();
			this.playersLabel = new System.Windows.Forms.Label();
			this.trackLabel = new System.Windows.Forms.Label();
			this.roomNameTextBox = new System.Windows.Forms.TextBox();
			this.trackComboBox = new System.Windows.Forms.ComboBox();
			this.maxPlayersUpDown = new System.Windows.Forms.NumericUpDown();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.passwordCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.maxPlayersUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(58, 132);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(151, 132);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// nameLabel
			// 
			this.nameLabel.Location = new System.Drawing.Point(11, 9);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(100, 23);
			this.nameLabel.TabIndex = 2;
			this.nameLabel.Text = "Room name";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// playersLabel
			// 
			this.playersLabel.Location = new System.Drawing.Point(11, 32);
			this.playersLabel.Name = "playersLabel";
			this.playersLabel.Size = new System.Drawing.Size(100, 23);
			this.playersLabel.TabIndex = 3;
			this.playersLabel.Text = "Number of players";
			this.playersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// trackLabel
			// 
			this.trackLabel.Location = new System.Drawing.Point(11, 58);
			this.trackLabel.Name = "trackLabel";
			this.trackLabel.Size = new System.Drawing.Size(100, 23);
			this.trackLabel.TabIndex = 4;
			this.trackLabel.Text = "Track";
			this.trackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// roomNameTextBox
			// 
			this.roomNameTextBox.Location = new System.Drawing.Point(151, 9);
			this.roomNameTextBox.Name = "roomNameTextBox";
			this.roomNameTextBox.Size = new System.Drawing.Size(120, 20);
			this.roomNameTextBox.TabIndex = 5;
			this.roomNameTextBox.Text = "Room1";
			// 
			// trackComboBox
			// 
			this.trackComboBox.BackColor = System.Drawing.SystemColors.Window;
			this.trackComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.trackComboBox.FormattingEnabled = true;
			this.trackComboBox.Items.AddRange(new object[] {
									"MyTrack1",
									"MyTrack2",
									"MyTrack3",
									"MyTrack4"});
			this.trackComboBox.Location = new System.Drawing.Point(151, 58);
			this.trackComboBox.Name = "trackComboBox";
			this.trackComboBox.Size = new System.Drawing.Size(121, 21);
			this.trackComboBox.Sorted = true;
			this.trackComboBox.TabIndex = 7;
			// 
			// maxPlayersUpDown
			// 
			this.maxPlayersUpDown.Location = new System.Drawing.Point(151, 35);
			this.maxPlayersUpDown.Minimum = new decimal(new int[] {
									2,
									0,
									0,
									0});
			this.maxPlayersUpDown.Name = "maxPlayersUpDown";
			this.maxPlayersUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.maxPlayersUpDown.Size = new System.Drawing.Size(120, 20);
			this.maxPlayersUpDown.TabIndex = 8;
			this.maxPlayersUpDown.Value = new decimal(new int[] {
									2,
									0,
									0,
									0});
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Enabled = false;
			this.passwordTextBox.Location = new System.Drawing.Point(151, 84);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '*';
			this.passwordTextBox.Size = new System.Drawing.Size(120, 20);
			this.passwordTextBox.TabIndex = 10;
			this.passwordTextBox.Text = "Room1";
			// 
			// passwordCheckBox
			// 
			this.passwordCheckBox.Location = new System.Drawing.Point(11, 84);
			this.passwordCheckBox.Name = "passwordCheckBox";
			this.passwordCheckBox.Size = new System.Drawing.Size(100, 24);
			this.passwordCheckBox.TabIndex = 11;
			this.passwordCheckBox.Text = "Password";
			this.passwordCheckBox.UseVisualStyleBackColor = true;
			this.passwordCheckBox.CheckedChanged += new System.EventHandler(this.PasswordCheckBoxCheckedChanged);
			// 
			// CreateRoomForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(284, 168);
			this.Controls.Add(this.passwordCheckBox);
			this.Controls.Add(this.passwordTextBox);
			this.Controls.Add(this.maxPlayersUpDown);
			this.Controls.Add(this.trackComboBox);
			this.Controls.Add(this.roomNameTextBox);
			this.Controls.Add(this.trackLabel);
			this.Controls.Add(this.playersLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CreateRoomForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Create a new room";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateRoomFormFormClosed);
			((System.ComponentModel.ISupportInitialize)(this.maxPlayersUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox passwordCheckBox;
		private System.Windows.Forms.TextBox passwordTextBox;
		private System.Windows.Forms.NumericUpDown maxPlayersUpDown;
		private System.Windows.Forms.ComboBox trackComboBox;
		private System.Windows.Forms.TextBox roomNameTextBox;
		private System.Windows.Forms.Label trackLabel;
		private System.Windows.Forms.Label playersLabel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
	}
}
