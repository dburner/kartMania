/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 14.06.2013
 * Time: 19:07 
 * 
 */
using System;
using SharpGL;

namespace kartMania.Video
{
	/// <summary>
	/// Description of IDrawableBox.
	/// </summary>
	public interface IDrawableBox
	{
		void Init(OpenGL opengl, string texturePath);
		
		void Draw();
	}
}
