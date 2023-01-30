using System;

namespace Petroineos.PowerTradeExtract.Logging
{
    public class LoggingService: ILoggingService
    {
        public void LogException(Exception exception)
        {
            //throw new NotImplementedException();
        }

        public void LogInfo(string extractionStarted)
        {
            //throw new NotImplementedException();
        }
    }

    public interface ILoggingService
    {
        void LogException(Exception exception);
        void LogInfo(string extractionStarted);
    }
}
