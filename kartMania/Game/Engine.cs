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
		// TODO Implement corect singletone instance
		public static Engine    Instance { get; private set; }
		public static NetEngine Network  { get { return Instance.m_networkEngine; } }
		
		private NetEngine    m_networkEngine;
		public  RenderEngine m_renderEngine;
		
		public Engine()
		{
			Init();
			Instance = this;
		}
		
		public void Init()
		{
			m_networkEngine = new NetEngine();
			//netEngine.
			//netEngine.NewNetMsg += new NetManager.NewNetMsgEventHandler( LobbyMsgHandler.HandleMsg );
		}
		
		public void StartGame()
		{
			m_renderEngine = new RenderEngine(1);
		}
	}
}
