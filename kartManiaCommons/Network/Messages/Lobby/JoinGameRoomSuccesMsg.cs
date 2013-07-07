/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 29.06.2013
 * Time: 16:01 
 * 
 */
using System;
using kartManiaCommons.Structs;

namespace kartManiaCommons.Network.Messages.Lobby
{
	/// <summary>
	/// Description of JoinGameRoomSucces.
	/// </summary>
	public class JoinGameRoomSuccesMsg:NetMsg
	{
		private const ushort service = (ushort)NetLobbyService.JoinGameRoomSucces;
		
		#region Factory Registration
		
		static JoinGameRoomSuccesMsg()
		{
			NetMsgQueue.RegisterMsgType(service, (data) => { return new JoinGameRoomSuccesMsg(data); });
		}
		
		#endregion
		
		#region Payload Members
		
		private bool 		 mIsOwner;
		private byte 		 mPlayerCount;
		private PlayerInfo[] mPlayersInfo;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Makes a new JoinGameRoomSuccesMsg avalible for writing.
		/// </summary>
		public JoinGameRoomSuccesMsg()
			:base(service)
		{
			
		}
		
		/// <summary>
		/// Makes a new JoinGameRoomSuccesMsg avalible for reading.
		/// </summary>
		public JoinGameRoomSuccesMsg(byte[] data)
			:base(service, data)
		{
			mIsOwner	 = streamReader.ReadBoolean();
			mPlayerCount = streamReader.ReadByte   ();
			
			mPlayersInfo = new PlayerInfo[mPlayerCount];
			
			for(int i = 0; i < mPlayerCount; i++)
			{
				mPlayersInfo[i] = new PlayerInfo();
				
				mPlayersInfo[i].playerId   = streamReader.ReadUInt32();
				mPlayersInfo[i].playerName = streamReader.ReadString();
			}
		}
		
		#endregion
		
		#region Properties
		
		public bool 		IsOwner
		{
			get { return mIsOwner; }
			set
			{
				AssertWritingMode();
				mIsOwner = value;
			}
		}
		
		public byte 		PlayersCount
		{
			get { return mPlayerCount; }
//			set
//			{
//				AssertWritingMode();
//				mPlayerCount = value;
//			}
		}
		
		public PlayerInfo[] PlayersInformation
		{
			get { return mPlayersInfo; }
			set
			{
				AssertWritingMode();
				mPlayersInfo = value;
			}
		}
		
		#endregion
		
		#region Public Methods
		
		public override void Build()
		{
			base.Build();
			
			mPlayerCount = (byte)mPlayersInfo.Length;
			
			streamWriter.Write(mIsOwner    );
			streamWriter.Write(mPlayerCount);
			
			for(int i = 0; i < mPlayerCount; i++)
			{
				streamWriter.Write(mPlayersInfo[i].playerId  );
				streamWriter.Write(mPlayersInfo[i].playerName);
			}
		}
		
		#endregion
	}
}
