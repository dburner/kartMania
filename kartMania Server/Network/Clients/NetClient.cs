/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 07.09.2012
 * Time: 15:54 
 * 
 */
using System;
using System.Net.Sockets;
using System.Threading;

using kartManiaCommons.Debug;
using kartManiaCommons.Network.Messages;

namespace kartManiaServer.Network
{
	/// <summary>
	/// Description of NetClient.
	/// </summary>
	public class NetClient
	{	
        private Socket      socket; 
        
        private const int   bufferSize = 1024;
        private byte[]      byteBuffer;
        
        protected volatile  NetMsgQueue msgQueue;
        
        #region Constructors
        
		public NetClient()
		{
			 msgQueue   = new NetMsgQueue();
             byteBuffer = new byte[bufferSize];
		}
		
		#endregion
		
		#region Public Members
		
		public bool Init(Socket connection)
        {
            try
            {
            	socket = connection;
            	socket.BeginReceive(byteBuffer, 0, 1024, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            	
                return true;
            }
            catch(Exception e)
            {
            	Logger.LogLine("Exception at Init function \n " + e.ToString());
                return false;
            }
        }	
		
		public void Disconnect()
		{
			socket.Disconnect(false);
			socket.Dispose();
			
			OnDisconnect();
			//msgQueue.Dispose();
			
			//ThreadPool.QueueUserWorkItem( s => OnDisconnect() );
		}
		
		public void SendMsg(NetMsg msg)
		{
			try
			{
				byte[] data = msg.GetData();
				socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
			}
			catch(SocketException e)
			{
				Logger.LogLine(e.ToString());
				Disconnect();
			}
			catch(Exception e)
			{
				Logger.LogLine(e.ToString());
			}
		}
		
		#endregion
		
		#region Protected Members
		
		/// <summary>
		/// Executes when new data has arrived.
		/// </summary>
		protected virtual void OnDataReceived()
		{
		}
		
		/// <summary>
		/// Executes when the client gets dissconected.
		/// </summary>
		protected virtual void OnDisconnect()
		{
			
		}
		
		#endregion
		
		#region Private Members
		
		private void ReceiveCallback( IAsyncResult ar ) 
		{
			try
			{
				int bytesRead;			
				
				bytesRead = socket.EndReceive(ar);
				if (bytesRead > 0)
				{
					lock(msgQueue)
						msgQueue.Enqueue(byteBuffer, bytesRead);
					
					//TODO ThreadPool OnDataReceived
					OnDataReceived();
					
					socket.BeginReceive(byteBuffer, 0, bufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
				}
				else
				{
					Logger.LogLine("Connection closed");
					Disconnect();
				}
			}
			catch(Exception e)
			{
				Logger.LogLine("Connection closed exception");
				Disconnect();
			}
			
		}
		
		private void    SendCallback( IAsyncResult ar ) 
		{
        	try 
        	{
            	//int bytesSent = client.EndSend(ar);
            	socket.EndSend(ar);

        	} 
        	catch(ObjectDisposedException e)
        	{
        		Logger.LogLine("Socket closed while sending data.");
        		Disconnect();
        	}
        	catch (Exception e)
        	{
        		Logger.LogLine(e.ToString());
        	}
       }
		
		#endregion
	}
}
