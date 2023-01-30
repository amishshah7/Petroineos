using System;
using System.Collections.Generic;
using System.Text;

namespace Petroineos.PowerTradeExtract.Services
{
    public class NotificationService: INotificationService
    {
        public void NotifyExportResult(bool exportSuccess)
        {
            //throw new NotImplementedException();
        }
    }

    public interface INotificationService
    {
        void NotifyExportResult(bool exportSuccess);
    }
}
