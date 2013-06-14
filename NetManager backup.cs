/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 08.09.2012
 * Time: 15:29 
 * 
 */
using System;
using System.Threading;
using kartManiaCommons.Debug;

namespace kartMania.Network
{	
	/// <summary>
	/// Description of NetManager.
	/// </summary>
	public class NetManager:NetClient
	{
		private Thread networkThread;
		
		public delegate void NewNetMsgEventHandler( NetMsg msg );
		public event 		 NewNetMsgEventHandler  NewNetMsg;
		
		public NetManager()
		{	
		}
		
		public new void Connect(string host, int port)
		{
			base.Connect(host, port);
			
			if (connected)
			{
				networkThread = new Thread(new ThreadStart(Work));
				networkThread.Name = "Network Thread";
				networkThread.Start();
			}
		}
		
		public override void Disconnect()
		{
			base.Disconnect();
			networkThread.Abort();
		}
		
		private void Work()
		{
			NetMsg msg;
			
			try
			{
				while(true)
				{
					msg = msgQueue.DequeueAsNetMsg();
					
					if (msg != null && NewNetMsg != null)
						NewNetMsg(msg);
					
					Thread.Sleep(1);
				}
			}
			catch(ThreadAbortException e)
			{
				Logger.LogLine("Network thread stoped");
			}
		}		
		
	}
}
