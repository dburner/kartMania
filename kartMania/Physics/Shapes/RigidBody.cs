/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 17.09.2012
 * Time: 22:09 
 * 
 */
using System;

namespace kartMania.Physics.Shapes
{
	/// <summary>
	/// Description of RigidBody.
	/// </summary>
	public class RigidBody
	{
		protected Vec2 mPos;
		protected Vec2 mVel;
		
		protected double mMass;
		
		public RigidBody()
		{
			mPos = new Vec2();
			mVel = new Vec2();
			
			mMass = 1;
		}
		
		public RigidBody(Vec2 pos, Vec2 vel, double mass)
		{
			mPos  = pos;
			mVel  = vel;
			mMass = mass;
		}
		
		public virtual void GenerateContact()
		{
			throw new NotImplementedException("Not implemented in base class");
		}
		
		public virtual void Integrate(double delta)
		{
			mPos += mVel * delta;
		}
	}
}
