/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 08.09.2012
 * Time: 15:10 
 * 
 */
using System;
using System.Threading;
using kartMania.Forms;
using kartMania.Network;

namespace kartMania.Game
{
	/// <summary>
	/// Description of Engine.
	/// </summary>
	public class Engine
	{
		public static Engine Instance   { get; private set; }
		public static NetEngine Network { get { return Instance.netEngine; } }
		
		private NetEngine    netEngine;
		public  RenderEngine renEngine;
		
		public Engine()
		{
			Init();
			Instance = this;
		}
		
		public void Init()
		{
			netEngine = new NetEngine();
			//netEngine.
			//netEngine.NewNetMsg += new NetManager.NewNetMsgEventHandler( LobbyMsgHandler.HandleMsg );
		}
		
		public void StartGame()
		{
			renEngine = new RenderEngine(1);
		}
	}
}
