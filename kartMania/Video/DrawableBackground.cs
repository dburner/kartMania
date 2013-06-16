/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 24.09.2012
 * Time: 17:27 
 * 
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using kartMania.Game;
using kartMania.Physics;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;

namespace kartMania.Video
{
	/// <summary>
	/// Description of DrawableBackground.
	/// </summary>
	public class DrawableBackground
	{
		private const float white = 1f;
		
		private Vec2[] mTexCoords;
		private Vec2[] mPoints;
		
		private Texture texture;
		private OpenGL  gl;
		
		byte[] bitmapData;
		Bitmap textureImage;
		
		public DrawableBackground()
		{
			mTexCoords = new Vec2[4];
			mPoints    = new Vec2[4];
		}
		
		public void Init(OpenGL opengl, string texturePath)
		{
			float width = RenderEngine.Width;
			float height = RenderEngine.Height;
//			mTexCoords[0] = new Vec2(0,1);
//			mTexCoords[1] = new Vec2(1,1);
//			mTexCoords[2] = new Vec2(1,0);
//			mTexCoords[3] = new Vec2(0,0);
//			
//			mPoints[0] = new Vec2(0	   , height);
//			mPoints[1] = new Vec2(width, height);
//			mPoints[2] = new Vec2(width, 0     );
//			mPoints[3] = new Vec2(0	   , 0     );
			
			mTexCoords[0] = new Vec2(0,0);
			mTexCoords[1] = new Vec2(1,0);
			mTexCoords[2] = new Vec2(1,1);
			mTexCoords[3] = new Vec2(0,1);
			
			mPoints[0] = new Vec2(0	   , 0     );
			mPoints[1] = new Vec2(width, 0     );
			mPoints[2] = new Vec2(width, height);
			mPoints[3] = new Vec2(0	   , height);
				
			gl = opengl;
			
			texture = new Texture();
			texture.Create(gl, texturePath);
			texture.Bind(gl);
			//GenerateTexture(texturePath);
//			MemoryStream ms = new MemoryStream();
//			Bitmap yourBitmap = new Bitmap(texturePath);
//			yourBitmap.Save(ms, ImageFormat.Bmp);//.Save(ms, ImageFormat.Bmp);
//        	bitmapData = ms.ToArray();
		}
		
		public void Draw()
		{
			texture.Bind(gl);
			
			gl.PushMatrix();
			
			gl.Begin(BeginMode.Quads);
			
				gl.Color(white, white, white);
				gl.Scale(1d, -1d, 1d);
				gl.TexCoord(mTexCoords[0].X, mTexCoords[0].Y); gl.Vertex(mPoints[0].X, mPoints[0].Y);
				gl.TexCoord(mTexCoords[1].X, mTexCoords[1].Y); gl.Vertex(mPoints[1].X, mPoints[1].Y);
				gl.TexCoord(mTexCoords[2].X, mTexCoords[2].Y); gl.Vertex(mPoints[2].X, mPoints[2].Y);
				gl.TexCoord(mTexCoords[3].X, mTexCoords[3].Y); gl.Vertex(mPoints[3].X, mPoints[3].Y);
				
			gl.End();
			
			gl.PopMatrix()
		}	

	}
}
