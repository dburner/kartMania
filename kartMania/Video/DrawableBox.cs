/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 24.09.2012
 * Time: 15:21 
 * 
 */
using System;
using kartMania.Physics;
using kartMania.Physics.Shapes;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;

namespace kartMania.Video
{
	/// <summary>
	/// Deprecated class
	/// </summary>
	public class DrawableBox:RigidBox
	{
		private const float white = 1f;
		
		private Vec2[]  mTexCoords;
		private OpenGL  gl;
		private Texture texture;
		
		public DrawableBox(Vec2 center, double width, double height)
			:base(center, width, height)
		{
			mTexCoords = new Vec2[4];
		}
		
		public void Init(OpenGL opengl, string texturePath)
		{
			mTexCoords[0] = new Vec2(0,1);
			mTexCoords[1] = new Vec2(1,1);
			mTexCoords[2] = new Vec2(1,0);
			mTexCoords[3] = new Vec2(0,0);
			
			gl = opengl;
			texture = new Texture();
			texture.Create(gl, texturePath);
			texture.Bind(gl);
		}

		public void Draw()
		{
			texture.Bind(gl);
			
			gl.Begin(BeginMode.Quads);
				
				gl.Color(white, white, white);
				gl.TexCoord(mTexCoords[0].X, mTexCoords[0].Y); gl.Vertex(mPoints[0].X, mPoints[0].Y);
				gl.TexCoord(mTexCoords[1].X, mTexCoords[1].Y); gl.Vertex(mPoints[1].X, mPoints[1].Y);
				gl.TexCoord(mTexCoords[2].X, mTexCoords[2].Y); gl.Vertex(mPoints[2].X, mPoints[2].Y);
				gl.TexCoord(mTexCoords[3].X, mTexCoords[3].Y); gl.Vertex(mPoints[3].X, mPoints[3].Y);	

			gl.End();
		}
	}
}
