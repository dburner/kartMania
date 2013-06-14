/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 17.09.2012
 * Time: 23:08 
 * 
 */
using System;

namespace kartMania.Physics.Shapes
{
	/// <summary>
	/// Description of RigidBox.
	/// </summary>
	public class RigidBox:RigidBody
	{
		//  A            B
		//   ------------
		//  |      .     |
		//  |      M     |
		//   ------------
		//  D            C
		
		protected Vec2[] mPoints;
		protected double mRotation;
		protected double mWidth;
		protected double mHeight;
		
		public RigidBox(Vec2 center, double width, double height)
		{
			mPos     = center;
			mPoints  = new Vec2[4];
			mRotation = 0;
			
		    mWidth  = width;
		    mHeight = height;
			
			CalculatePoints();
		}
		
		public override void Integrate(double delta)
		{		
			Vec2 dist = mVel * delta;
			
			mPos += dist;
			
			mPoints[0] += dist;
			mPoints[1] += dist;
			mPoints[2] += dist;
			mPoints[3] += dist;
		}
		
		public void Rotate(double angle)
		{
			mRotation += angle;
			//CalculatePoints();
			//ApplyRotation();
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
			mPos += vector;
			
			mPoints[0] += vector;
			mPoints[1] += vector;
			mPoints[2] += vector;
			mPoints[3] += vector;
		}
		
		private void CalculatePoints()
		{
			double halfWidth  = mWidth  / 2;
			double halfHeight = mHeight / 2;
			double x = mPos.X;
			double y = mPos.Y;
			
			mPoints[0] = new Vec2(x - halfWidth, y + halfHeight); //A
			mPoints[1] = new Vec2(x + halfWidth, y + halfHeight); //B
			mPoints[2] = new Vec2(x + halfWidth, y - halfHeight); //C
			mPoints[3] = new Vec2(x - halfWidth, y - halfHeight); //D
			
			if (mRotation != 0 )
				ApplyRotation();
		}
		
		private void ApplyRotation()
		{
			mPoints[0].Rotate(mPos, mRotation);
			mPoints[1].Rotate(mPos, mRotation);
			mPoints[2].Rotate(mPos, mRotation);
			mPoints[3].Rotate(mPos, mRotation);
		}
		
		private void ApplyRotation(double angle)
		{
			mPoints[0].Rotate(mPos, angle);
			mPoints[1].Rotate(mPos, angle);
			mPoints[2].Rotate(mPos, angle);
			mPoints[3].Rotate(mPos, angle);
		}
	}
}