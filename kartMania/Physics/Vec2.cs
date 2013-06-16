/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 17.09.2012
 * Time: 20:56 
 * 
 */
using System;

namespace kartMania.Physics
{
	/// <summary>
	/// Description of Vec2.
	/// </summary>
	public class Vec2
	{
		//TODO overwrite += operator for extra optimization
		private double mx; //TODO put float
		private double my;
		
		private const double TwoPi = 2 * Math.PI;
		
		#region Constructors
		
		/// <summary>
		/// Creates a new vector initialized with 0.
		/// </summary>
		public Vec2()
		{
			mx = 0;
			my = 0;
		}
		
		/// <summary>
		/// Creates a new vector based on x and y
		/// </summary>
		public Vec2(double x, double y)
		{
			mx = x;
			my = y;
		}
		
		public Vec2(Vec2 v)
		{
			mx = v.mx;
			my = v.my;
		}
		
		#endregion
		
		#region Public Methods
		
		public void Normalize()
		{
			double mag = Length;
			
			mx /= mag;
			my /= mag;			
		}
		
		//TODO Try rotating using projection
		/// <summary>
		/// DEPRECATED
		/// </summary>
		public void RotateOld(Vec2 center, double angle)
		{
			mx -= center.mx;
			my -= center.my;
			
			double len = Length;
			double ang = Angle;
			
			ang += angle;
			
			mx = len * Math.Cos(ang);
			my = len * Math.Sin(ang);
			
			mx += center.mx;
			my += center.my;
		}
		
		public void Rotate(Vec2 center, double angle)
		{
			double x,y;
			
			double sin = Math.Sin(angle);
			double cos = Math.Cos(angle);
			
			mx -= center.X;
			my -= center.Y;
			
			x = mx * cos - my * sin;
			y = mx * sin + my * cos;
			
			mx = x + center.X;
			my = y + center.Y;
			
		}
		
		public double DotProduct(Vec2 u)
		{
			return mx * u.mx + my * u.my;  //may not be real
		}
		
		public Vec2 Project(Vec2 u)
		{
			double dotProd = DotProduct(u);			
			Vec2 ret = this * dotProd;			
			
			return ret;
		}
		
		#endregion
		
		#region Properties
		
		public double Length
		{
			get
			{
				return Math.Sqrt(mx * mx + my * my);
			}
		}
		
		public double LengthSquare
		{
			get { return mx * mx + my * my; }
		}
		
		public double X
		{
			get { return mx;  }
			set { mx = value; }
		}
		
		public double Y
		{
			get { return my;  }
			set { my = value; }
		}
		
		public double Angle
		{
			get
			{
				double angle = Math.Atan2(my, mx);
				if (angle < 0)
					angle += TwoPi;
				
				return angle;
			}
		}
		
		#endregion
		
		#region Operators
		
		public static Vec2 operator + (Vec2 a, Vec2 b)
		{
			return new Vec2(a.mx + b.mx, a.my + b.my);
		}
		
		public static Vec2 operator - (Vec2 a, Vec2 b)
		{
			return new Vec2(a.mx - b.mx, a.my - b.my);
		}
		
		public static Vec2 operator * (Vec2 u, double r)
		{
			return new Vec2(u.mx * r, u.my * r);
		}
		
		public static Vec2 operator / (Vec2 u, double r)
		{
			return new Vec2(u.mx / r, u.my / r);
		}
		
		/// <summary>
		/// Wedge product
		/// </summary>
		public static double operator ^ ( Vec2 a, Vec2 b)
		{
			return a.mx * b.my - a.my * b.mx;
		}
		
		#endregion
	}
}
