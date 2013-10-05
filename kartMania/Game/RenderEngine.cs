/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 17.09.2012
 * Time: 20:56 
 * 
 */
using System;
using System.Collections;
using kartMania.Physics;
using kartMania.Video;
using SharpGL;
using SharpGL.Enumerations;

namespace kartMania.Game
{
	/// <summary>
	/// Description of RenderEngine.
	/// </summary>
	public class RenderEngine
	{
		public const int Width = 1200;
		public const int Height = 675;
		private ArrayList playerObjects;
		private DrawableBackground backGround;
		private OpenGL gl;
		
		public RenderEngine(int players)
		{
			playerObjects = new ArrayList(players);
			
		}
		
		public void setGl(OpenGL opengl)
		{
			gl = opengl;
			
			backGround = new DrawableBackground();
			backGround.Init(gl, "Images\\Track1.bmp");
			
			DrawableBox box = new DrawableBox(new Vec2(167, 112), 175, 84);
			
			box.Rotate(Math.PI);
			box.Init(gl, "Images\\Car_01.png");
			box.Scale(0.5);
			box.Rotate(Math.PI*1.2);
			playerObjects.Add(box);
			
			//box = new DrawableBox(new Vec2(167, 112), 175, 84);
			//box.Init(gl, "Images\\Car_01.png");
			//playerObjects.Add(box);
			
		}
		
		public void Render(int width, int height)
		{
			width = Width;
			height = Height;
			gl.MatrixMode(OpenGL.GL_PROJECTION);
			gl.LoadIdentity();
			
			//gl.SetDimensions(width, height);
			gl.Ortho2D(0, width, 0, height);
			
			gl.MatrixMode(MatrixMode.Modelview);
			gl.LoadIdentity();
			gl.ClearColor(0.3f, 0.3f, 0.3f, 0f);
			gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
			
			
			backGround.Draw();
			foreach(DrawableBox box in playerObjects)
			{
				box.Draw();
				//box.Rotate(Math.PI/100);
				//box.Translate(new Vec2(2,0));
			}

			
			gl.Flush();
		}
	}
}
