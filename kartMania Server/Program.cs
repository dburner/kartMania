/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 04.09.2012
 * Time: 15:16
 * 
 */
using System;
using System.Diagnostics;
using kartManiaCommons.Debug;
using kartManiaServer.Network;

namespace kartManiaServer
{
	class Program
	{
		public static void Main(string[] args)
		{
			//TODO Test server performance without the volatile keywords
			GameServer gameServer = new GameServer();
			gameServer.Start();
			
			Console.ReadKey(true);
		}
	}
}