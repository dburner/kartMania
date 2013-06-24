/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 06.09.2012
 * Time: 16:11
 * 
 */
using System;

namespace kartManiaCommons.Network
{
	public enum NetService:ushort
	{
		FirstValue = 0,
		Blank,
		Ping,
		LastValue
	}
	
	public enum NetLobbyService:ushort
	{
		FirstValue = NetService.LastValue+1,
		LobbyChat,			   // 5
		JoinGameRoom,		   // 6
		JoinGameRoomSucces,    // 7
		JoinGameRoomFail,	   // 8
		CreateGameRoom,		   // 9
		GameRoomCreated,	   //10
		GameRoomDestroyed,	   //11
		GameRoomPlayerJoined,  //12
		GameRoomPlayerLeft,    //13
		GameRoomUpdatePlayers, //14
		GameRoomsList,		   //15
		LastValue			   //16
	}
	
	public enum NetGameRoomSerice:ushort
	{
		FirstValue = NetLobbyService.LastValue + 1,
		SetName,
		LeaveGameRoom, //18
		LastValue
	}
	
	/// <summary>
	/// A static class containing common network constants
	/// </summary>
	public static class NetConsants
    {
        private const string fieldsDelim        = "ÀÛ";//"À€";
        private const string fieldsDelimReplace = "&#192;&#219";//"&#192;&#8364;";
        private const string packetHeader       = "GMSG";
        private const int    port               =  1234;
        private const string serverIp           = "127.0.0.1";

        //private static string fieldsDelim     = Encoding.UTF8.GetString(new byte[] {192, 219});

        //private static string stringEncoder = "ASCII";

        public static string PacketHeader
        {
            get { return packetHeader; }
        }

        public static string FieldsDelimiter
        {
            get { return fieldsDelim; }
        }

        public static string FieldsDelimiterReplace
        {
            get { return fieldsDelimReplace; }
        }

        public static int ServerPort
        {
            get { return port; }
        }

        public static string ServerIp
        {
            get { return serverIp; }
        }

    }
}
