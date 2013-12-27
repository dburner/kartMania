/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 08.12.2013
 * Time: 17:29 
 * 
 */
using System;
using kartManiaCommons.Network.Messages;

namespace kartMania.Network
{
	/// <summary>
	/// Description of INetMsgHandler.
	/// </summary>
	public interface INetMsgHandler
	{
		void HandleMsg(NetMsg msg);
	}
}
