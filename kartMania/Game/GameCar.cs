/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 15.06.2013
 * Time: 14:06 
 * 
 */
using System;
using kartMania.Physics;
using kartMania.Physics.Shapes;
using kartMania.Video;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;

namespace kartMania.Game
{
	/// <summary>
	/// Description of PlayerCar.
	/// </summary>
	public class GameCar:IDrawableBox, IRigidBox
	{
		//  A            B
		//   ------------
		//  |      .     |
		//  |      M     |
		//   ------------
		//  D            C
		
		// RigidBody
		protected Vec2 mPosistion;
		protected Vec2 mVelocity;
		
		protected double mMass;
		
		// RigidBox
		protected Vec2[] mPoints;
		protected double mRotation;		// Rotation is in radians
		protected double mWidth;
		protected double mHeight;
		
		// DrawableBox
		private   const float white = 1f;
		protected OpenGL  gl;			// gl object to draw the car		
		protected Vec2[]  mTexCoords;
		protected Texture texture;
		
		public GameCar()
		{
		}
		
		
		#region IDrawableBox Implementation
		
		public void Init(OpenGL opengl, string texturePath)
		{
			mTexCoords 	  = new Vec2[4];
			mTexCoords[0] = new Vec2(0,1);
			mTexCoords[1] = new Vec2(1,1);
			mTexCoords[2] = new Vec2(1,0);
			mTexCoords[3] = new Vec2(0,0);
			
			gl = opengl;
			texture = new Texture();
			texture.Create(gl, texturePath);
			texture.Bind(gl);
			//texture.Dispse();
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
		
		
		#endregion
		
		
		#region IRigidBox Implementation
		
		public void Integrate(double delta)
		{
			Vec2 dist = mVelocity * delta;
			
			mPosistion += dist;
			
			mPoints[0] += dist;
			mPoints[1] += dist;
			mPoints[2] += dist;
			mPoints[3] += dist;
		}
		
		public void Rotate(double angle)
		{
			mRotation += angle;
			ApplyRotation(angle);
		}
		
		public void Scale(double r)
		{
			mWidth  *= r;
			mHeight *= r;
			CalculatePoints();
		}
		
		public void Translate(Vec2 vector)
		{
			mPosistion += vector;
			
			mPoints[0] += vector;
			mPoints[1] += vector;
			mPoints[2] += vector;
			mPoints[3] += vector;
		}
		
		
		private void CalculatePoints()
		{
			double halfWidth  = mWidth  / 2;
			double halfHeight = mHeight / 2;
			double x = mPosistion.X;
			double y = mPosistion.Y;
			
			mPoints[0] = new Vec2(x - halfWidth, y + halfHeight); //A
			mPoints[1] = new Vec2(x + halfWidth, y + halfHeight); //B
			mPoints[2] = new Vec2(x + halfWidth, y - halfHeight); //C
			mPoints[3] = new Vec2(x - halfWidth, y - halfHeight); //D
			
			if (mRotation != 0 )
				ApplyRotation();
			
			//TODO Optimize: First apply rotation then translate position
		}
		
		private void ApplyRotation()
		{
			mPoints[0].Rotate(mPosistion, mRotation);
			mPoints[1].Rotate(mPosistion, mRotation);
			mPoints[2].Rotate(mPosistion, mRotation);
			mPoints[3].Rotate(mPosistion, mRotation);
		}
		
		private void ApplyRotation(double angle)
		{
			mPoints[0].Rotate(mPosistion, angle);
			mPoints[1].Rotate(mPosistion, angle);
			mPoints[2].Rotate(mPosistion, angle);
			mPoints[3].Rotate(mPosistion, angle);
		}
		
		#endregion
	}
}
