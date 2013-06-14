/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 14.06.2013
 * Time: 19:09 
 * 
 */
using System;

namespace kartMania.Physics.Shapes
{
	/// <summary>
	/// Description of IRigidBox.
	/// </summary>
	public interface IRigidBox
	{
		void Integrate(double delta);
		
		void Rotate(double angle);
		
		void Scale(double r);
		
		void Translate(Vec2 vector);
		
		// private void CalculatePoints();
		
		// private void ApplyRotation();
		
		// private void ApplyRotation(double angle);
	}
}
