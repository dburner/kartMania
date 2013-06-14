/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 19.09.2012
 * Time: 16:32 
 * 
 */
namespace kartMania.Forms
{
	partial class GameForm
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
			this.openGLControl = new SharpGL.OpenGLControl();
			((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
			this.SuspendLayout();
			// 
			// openGLControl
			// 
			this.openGLControl.BitDepth = 24;
			this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.openGLControl.DrawFPS = true;
			this.openGLControl.FrameRate = 28;
			this.openGLControl.Location = new System.Drawing.Point(0, 0);
			this.openGLControl.Name = "openGLControl";
			this.openGLControl.RenderContextType = SharpGL.RenderContextType.NativeWindow;
			this.openGLControl.Size = new System.Drawing.Size(1202, 673);
			this.openGLControl.TabIndex = 0;
			this.openGLControl.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.OpenGLControlOpenGLDraw);
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1202, 673);
			this.Controls.Add(this.openGLControl);
			this.Name = "GameForm";
			this.Text = "GameForm";
			((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
			this.ResumeLayout(false);
		}
		private SharpGL.OpenGLControl openGLControl;
	}
}
