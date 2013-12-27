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
		private double m_x; //TODO put float
		private double m_y;
		
		private const double TwoPi = 2 * Math.PI;
		
		#region Constructors
		
		/// <summary>
		/// Creates a new vector initialized with 0.
		/// </summary>
		public Vec2()
		{
			m_x = 0;
			m_y = 0;
		}
		
		/// <summary>
		/// Creates a new vector based on x and y
		/// </summary>
		public Vec2(double x, double y)
		{
			m_x = x;
			m_y = y;
		}
		
		public Vec2(Vec2 v)
		{
			m_x = v.m_x;
			m_y = v.m_y;
		}
		
		#endregion
		
		#region Public Methods
		
		public void Normalize()
		{
			double mag = Length;
			
			m_x /= mag;
			m_y /= mag;			
		}
		
		public void Rotate(Vec2 center, double angle)
		{
			double x,y;
			
			double sin = Math.Sin(angle);
			double cos = Math.Cos(angle);
			
			m_x -= center.X;
			m_y -= center.Y;
			
			x = m_x * cos - m_y * sin;
			y = m_x * sin + m_y * cos;
			
			m_x = x + center.X;
			m_y = y + center.Y;
			
		}
		
		public double DotProduct(Vec2 u)
		{
			return m_x * u.m_x + m_y * u.m_y;  //may not be real
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
				return Math.Sqrt(m_x * m_x + m_y * m_y);
			}
		}
		
		public double LengthSquare
		{
			get { return m_x * m_x + m_y * m_y; }
		}
		
		public double X
		{
			get { return m_x;  }
			set { m_x = value; }
		}
		
		public double Y
		{
			get { return m_y;  }
			set { m_y = value; }
		}
		
		public double Angle
		{
			get
			{
				double angle = Math.Atan2(m_y, m_x);
				if (angle < 0)
					angle += TwoPi;
				
				return angle;
			}
		}
		
		#endregion
		
		#region Operators
		
		public static Vec2 operator + (Vec2 a, Vec2 b)
		{
			return new Vec2(a.m_x + b.m_x, a.m_y + b.m_y);
		}
		
		public static Vec2 operator - (Vec2 a, Vec2 b)
		{
			return new Vec2(a.m_x - b.m_x, a.m_y - b.m_y);
		}
		
		public static Vec2 operator * (Vec2 u, double r)
		{
			return new Vec2(u.m_x * r, u.m_y * r);
		}
		
		public static Vec2 operator / (Vec2 u, double r)
		{
			return new Vec2(u.m_x / r, u.m_y / r);
		}
		
		/// <summary>
		/// Wedge product
		/// </summary>
		public static double operator ^ ( Vec2 a, Vec2 b)
		{
			return a.m_x * b.m_y - a.m_y * b.m_x;
		}
		
		#endregion
	}
}
