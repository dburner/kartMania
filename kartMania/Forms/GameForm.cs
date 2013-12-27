/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 19.09.2012
 * Time: 16:32 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using kartMania.Game;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;

namespace kartMania.Forms
{
	/// <summary>
	/// Description of GameForm.
	/// </summary>
	public partial class GameForm : Form
	{
		public static GameForm Instance { get; private set; }
		public static OpenGL   OpenGLComponent { get { return Instance.gl; } }
		
		private OpenGL gl;
		private Texture texture;
		
		public GameForm()
		{
			InitializeComponent();
			
			gl = openGLControl.OpenGL;
			
			//gl.SetDimensions(openGLControl.Width, openGLControl.Height);
			//gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
			
			
			//gl.Disable(OpenGL.GL_TEXTURE_3D);
			gl.Disable(OpenGL.GL_DEPTH_TEST);
			
			gl.Enable(OpenGL.GL_BLEND);
			gl.Enable(OpenGL.GL_TEXTURE_2D);
			//gl.Enable(OpenGL.GL_TEXTURE_RECTANGLE_ARB);
            
			gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_MODULATE);
			gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
			gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST  );
			gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR  );
			
			Engine.Instance.StartGame();
			Engine.Instance.m_renderEngine.setGl(gl);
		}
		
		
		
		void OpenGLControlOpenGLDraw(object sender, PaintEventArgs e)
		{
			Engine.Instance.m_renderEngine.Render(openGLControl.Width, openGLControl.Height);
		}
	}
}
