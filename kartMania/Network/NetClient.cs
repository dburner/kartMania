/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 05.09.2012
 * Time: 14:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
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
        private Socket      m_client;
        
        private const int   m_bufferSize = 1024;
        private byte[]      m_byteBuffer;
        
        protected NetMsgQueue m_msgQueue;
        protected bool		  m_connected;
        
        public bool Connected { get { return m_connected; } }
        
        #region Constructors
        
		public NetClient()
		{
			 NetMsgQueue.RegisterMsgTypes();
			 m_msgQueue   = new NetMsgQueue();
             m_byteBuffer = new byte[m_bufferSize];
             m_connected  = false;
		}
		
		#endregion
		
		#region Public Members
		
		public void Connect(string host, int port)
        {
            try
            {
            	m_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
            	
            	m_client.Connect(host, port);             
                m_client.BeginReceive(m_byteBuffer, 0, 1024, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
                
               	m_connected = true;
            }
            catch(Exception e)
            {
            	m_connected = false;
            	Logger.LogLine(e.ToString());
            }
        }	
		
		public virtual void Disconnect()
		{
			// Close = Disconnect + Dispose
			if (m_client != null)
				m_client.Close();
			//client.Disconnect(false);
			//client.Dispose();
		 	m_connected = false;
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
				m_client.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
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
				
				bytesRead = m_client.EndReceive(ar);
				if (bytesRead > 0)
				{
					lock(m_msgQueue)
						m_msgQueue.Enqueue(m_byteBuffer, bytesRead);
					//Logger.LogLine(msgQueue.DequeueAsString());
					OnDataReceived();
					
					m_client.BeginReceive(m_byteBuffer, 0, m_bufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
				}
				else
				{
					Logger.LogLine("Connection closed");
					Disconnect();
				}
			}
			catch(Exception e) // TODO:Implement corect catch with specific exceptions
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
            	m_client.EndSend(ar);

        	} catch (Exception e) 
        	{
        		Logger.LogLine("SendCallback exception:\n" + e.ToString());
        	}
       }
		
		#endregion
	}
}
