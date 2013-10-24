/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 24.10.2013
 * Time: 14:51 
 * 
 */
using System;
using System.Runtime.Serialization;

namespace kartMania.Exceptions
{
	/// <summary>
	/// Desctiption of NoHandlerException.
	/// </summary>
	public class NoHandlerException : Exception, ISerializable
	{
		public NoHandlerException()
		{
		}

	 	public NoHandlerException(string message) : base(message)
		{
		}

		public NoHandlerException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		protected NoHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}