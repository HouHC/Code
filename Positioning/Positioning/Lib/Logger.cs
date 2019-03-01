using System;

namespace Templete.Positioning.Lib
{
    public static class Log
    {
        public static void WriteInfo(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("TcpInfo");
            log.Info(msg);
        }

        public static void WriteDebug(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("debug");
            log.Debug(msg);
        }

        public static void WriteError(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("error");
            log.Error(msg);
        }
    }
}