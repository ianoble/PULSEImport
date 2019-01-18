using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace PULSEImport
{
    /// <summary>
    /// Helper methods for working with log4net
    /// </summary>
    public class Log4NetHelper
    {
        /// <summary>
        /// Logs error messages given error code and message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="internalIdentifier">GUID</param>
        /// <param name="externalIdentifier">External identifier.</param>
        public static void LogError(ILog logger, string errorCode, string errorMessage, string internalIdentifier = "", string externalIdentifier = "")
        {
            if (logger.IsErrorEnabled && !string.IsNullOrEmpty(errorMessage))
            {
                GlobalContext.Properties["ErrorCode"] = errorCode;
                GlobalContext.Properties["InternalIdentifier"] = internalIdentifier;
                GlobalContext.Properties["ExternalIdentifier"] = externalIdentifier;
                logger.Error(errorMessage);
            }
        }

        /// <summary>
        /// Logs error messages given error code, exception, and optionally an error message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="internalIdentifier">GUID</param>
        /// <param name="externalIdentifier">External identifier.</param>
        public static void LogError(ILog logger, string errorCode, Exception ex, string errorMessage = "", string internalIdentifier = "", string externalIdentifier = "")
        {
            if (logger.IsErrorEnabled && ex != null)
            {
                GlobalContext.Properties["ErrorCode"] = errorCode;
                GlobalContext.Properties["InternalIdentifier"] = internalIdentifier;
                GlobalContext.Properties["ExternalIdentifier"] = externalIdentifier;
                logger.Error((string.IsNullOrEmpty(errorMessage) ? ex.Message : errorMessage), ex);
            }
        }

        /// <summary>
        /// Logs error messages given error code, exception, and optionally an error message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="internalIdentifier">GUID</param>
        /// <param name="externalIdentifier">External identifier.</param>
        public static void LogError(ILog logger, Exception ex, string errorMessage = "", string internalIdentifier = "", string externalIdentifier = "")
        {
            if (logger.IsErrorEnabled && ex != null)
            {
                GlobalContext.Properties["InternalIdentifier"] = internalIdentifier;
                GlobalContext.Properties["ExternalIdentifier"] = externalIdentifier;
                logger.Error((string.IsNullOrEmpty(errorMessage) ? ex.Message : errorMessage), ex);
            }
        }

        /// <summary>
        /// Logs debug messages
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="debugMessage">A composite format string.</param>
        /// <param name="args">The object(s) to format.</param>
        public static void LogDebug(ILog logger, string debugMessage, params object[] args)
        {
            LogDebug(logger, string.Format(debugMessage, args));
        }

        /// <summary>
        /// Logs debug messages
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="debugMessage">The debug message.</param>
        /// <param name="ex">The ex.</param>
        public static void LogDebug(ILog logger, string debugMessage, Exception ex = null)
        {
            if (logger.IsDebugEnabled && !string.IsNullOrEmpty(debugMessage))
            {
                if (ex != null)
                {
                    logger.Debug(debugMessage, ex);
                }
                else
                {
                    logger.Debug(debugMessage);
                }
            }
        }

        /// <summary>
        /// Logs info messages.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="infoMessage">The info message.</param>
        /// <param name="ex">The ex.</param>
        public static void LogInfo(ILog logger, string infoMessage, Exception ex = null)
        {
            if (logger.IsInfoEnabled && !string.IsNullOrEmpty(infoMessage))
            {
                if (ex != null)
                {
                    logger.Info(infoMessage, ex);
                }
                else
                {
                    logger.Info(infoMessage);
                }
            }
        }
    }
}
