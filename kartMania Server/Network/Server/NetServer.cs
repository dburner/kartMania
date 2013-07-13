/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.09.2012
 * Time: 13:24 
 * 
 */
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

using kartManiaCommons.Debug;
using kartManiaCommons.Network;

namespace kartManiaServer.Network
{
	/// <summary>
	/// Description of NetServer.
	/// </summary>
	public class NetServer
	{
		private Socket    serverSocket;
		
		private const int acceptCallbacks = 10;
		
		public NetServer()
		{
			//serverSocket.BeginAccept
		}
		
		public virtual void Start()
		{
			 SetupServerSocket();
        	 for (int i = 0; i < acceptCallbacks; i++)
             	serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
		}
		
		#region Protected Members
		
		protected virtual void OnClientConnect(NetPlayer player)
		{
			
		}
		
		#endregion
		
		
		#region Private Members
		
		private void SetupServerSocket()
		{
			
			IPAddress  ipAddress  = IPAddress.Parse(NetConsants.ServerIp);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, NetConsants.ServerPort);
			
			serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			serverSocket.Bind(ipEndPoint);
			serverSocket.Listen((int)SocketOptionName.MaxConnections);
		}
		
		private void CloseConnection()
		{
			
		}
		
		#endregion
		
		#region Callbacks
		
		private void AcceptCallback(IAsyncResult result)
    	{
        	NetPlayer newPlayer = new NetPlayer();
        	try
        	{
	            Socket newSocket = serverSocket.EndAccept(result);
	            newPlayer.Init(newSocket);
	            
	            OnClientConnect(newPlayer);
	            
	            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
	        }
	        catch (SocketException e)
	        {
	            //CloseConnection(connection);
	            Logger.LogLine("Socket exception: " + e.SocketErrorCode);
	        }
	        catch (Exception e)
	        {
	            //CloseConnection(connection);
	             Logger.LogLine("Exception: " + e.ToString());
	        }
    	}
		
		#endregion
	}
}
