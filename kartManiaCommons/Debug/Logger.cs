/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 06.09.2012
 * Time: 15:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace kartManiaCommons.Debug
{
	/// <summary>
	/// A simple object for loging stuff
	/// </summary>
	public static class Logger
    {
        public enum ServerLogLevel { Nothing, Subtle, Verbose };
        
        private static ServerLogLevel LogLevel = ServerLogLevel.Verbose;
        private static TextWriter     logger   = Console.Out;
        
        public static void Log(string str, ServerLogLevel level)
        {
            if (logger != null && (int)LogLevel >= (int)level)
            {
                logger.Write(str);
            }
        }

        public static void LogLine(string str, ServerLogLevel level)
        {
            Log(str + "\r\n", level);
        }
        
        public static void LogLine(string str)
        {
        	LogLine(str, ServerLogLevel.Verbose);
        }
    }
}
