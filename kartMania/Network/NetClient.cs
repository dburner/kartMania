/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 05.09.2012
 * Time: 14:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Net.Sockets;

using kartManiaCommons.Debug;
using kartManiaCommons.Network.Messages;

namespace kartMania.Network
{
	/// <summary>
	/// Description of NetClient.
	/// </summary>
	public class NetClient
	{        
        private Socket      client;
        
        private const int   bufferSize = 1024;
        private byte[]      byteBuffer;
        
        protected NetMsgQueue msgQueue;
        protected bool		  connected;
        
        public bool Connected { get { return connected; } }
        
        #region Constructors
        
		public NetClient()
		{
			 NetMsgQueue.RegisterMsgTypes();
			 msgQueue   = new NetMsgQueue();
             byteBuffer = new byte[bufferSize];
             connected  = false;
		}
		
		#endregion
		
		#region Public Members
		
		public void Connect(string host, int port)
        {
            try
            {
            	client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
            	
            	client.Connect(host, port);             
                client.BeginReceive(byteBuffer, 0, 1024, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
                
               	connected = true;
            }
            catch(Exception e)
            {
            	connected = false;
            	Logger.LogLine(e.ToString());
            }
        }	
		
		public virtual void Disconnect()
		{
			// Close = Disconnect + Dispose
			if (client != null)
				client.Close();
			//client.Disconnect(false);
			//client.Dispose();
		 	connected = false;
			//msgQueue.Dispose();
		}
		
		public virtual void OnDisconnect()
		{
			
		}
		
		#endregion
		
		#region Protected Members
		
		protected virtual void OnDataReceived()
		{
			
		}
		
		protected void SendMsg(NetMsg msg)
		{
			try
			{
				byte[] data = msg.GetData();
				client.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
			}
			catch(SocketException e)
			{
				if (Connected)
					Disconnect();
				Logger.LogLine(e.ToString());
			}
			catch(Exception e)
			{
				Logger.LogLine(e.ToString());
			}
		}
		
		#endregion
		
		#region Private Members
		
		private void ReceiveCallback( IAsyncResult ar ) 
		{
			try
			{
				int bytesRead;			
				
				bytesRead = client.EndReceive(ar);
				if (bytesRead > 0)
				{
					lock(msgQueue)
						msgQueue.Enqueue(byteBuffer, bytesRead);
					//Logger.LogLine(msgQueue.DequeueAsString());
					OnDataReceived();
					
					client.BeginReceive(byteBuffer, 0, bufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
				}
				else
				{
					Logger.LogLine("Connection closed");
					Disconnect();
				}
			}
			catch(Exception e)
			{
				Logger.LogLine(e.ToString());
				Disconnect();
			}
			
		}
		
		private void    SendCallback( IAsyncResult ar ) 
		{
        	try 
        	{
            	//int bytesSent = client.EndSend(ar);
            	client.EndSend(ar);

        	} catch (Exception e) 
        	{
        		Logger.LogLine("SendCallback exception:\n" + e.ToString());
        	}
       }
		
		#endregion
	}
}
