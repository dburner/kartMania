/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 14.06.2013
 * Time: 18:59 
 * 
 */
using System;

namespace kartMania.Physics.Shapes
{
	/// <summary>
	/// Description of IRigidBody.
	/// </summary>
	public interface IRigidBody
	{
		void GenerateContact();
		
		void Integrate(double delta); // mPos += mVel * delta;
	}
}
