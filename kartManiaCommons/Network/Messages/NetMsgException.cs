/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 23.06.2013
 * Time: 15:19 
 * 
 */
using System;
using System.Runtime.Serialization;

namespace kartManiaCommons.Network.Messages
{
	/// <summary>
	/// Desctiption of NetMsgException.
	/// </summary>
	public class NetMsgException : Exception, ISerializable
	{
		public NetMsgException()
		{
		}

	 	public NetMsgException(string message) : base(message)
		{
		}

		public NetMsgException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		protected NetMsgException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}