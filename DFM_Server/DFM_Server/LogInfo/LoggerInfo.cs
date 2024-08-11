using NLog;

////////////////////////////////////////////////////////////////////////////////
//
//  COPYRIGHT (c) 2024 ADT, INC.
//
//  This software is the property of ADT Industries, Inc.
//  Any reproduction or distribution to a third party is
//  strictly forbidden unless written permission is given by an
//  authorized agent of ADT.
//
//  DESCRIPTION
//		Definition Logger class for tracking info and error flow
//
//
//	Date		Name								Description
//	----------------------------------------------------------------------------
// 2024         Tzion
//
//=============================================================================

namespace LogInfo
{
    // Logger class, using it to create app.log file
    public class LoggerInfo
    {
        private static Logger ?loggerInfo;

        // Private constructor
        private LoggerInfo() { }

        public static Logger GetLogger()
        {
            if (loggerInfo == null)
            {
                LogManager.LoadConfiguration("NLog.config");
                loggerInfo = LogManager.GetCurrentClassLogger();
                loggerInfo.Info("********************Logger Instance created********************");
            }

            return loggerInfo;
        }
    }
}
