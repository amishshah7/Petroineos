using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petroineos.PowerTradeExtract.Model;

namespace Petroineos.PowerTradeExtract.Services
{
    public class CalculationService: ICalculationService
    {
        public DateTime GetExtractDate(ExtractSettings extractSettings)
        {
            //todo: may be utc or zone specific logic
            return DateTime.Now;
        }

        public PowerTradeExtractResponse CalculatePowerTrade(Model.PowerTradeExtract powerTradeExtract)
        {
            //todo: write logic here to merge the periods and add volume

            var extractResponse = new PowerTradeExtractResponse();
            extractResponse.FileName = GetFileName(powerTradeExtract.ExtractDate);
            extractResponse.PowerTrade = powerTradeExtract.PowerTrade.FirstOrDefault();
            

            return extractResponse;
        }

        public string GetFileName(DateTime extractDate)
        {
            return
                $"PowerPosition_{extractDate.Year}{extractDate.Month}{extractDate.Day}_{extractDate.Hour}{extractDate.Minute}.csv";
        }
    }

    public interface ICalculationService
    {
        DateTime GetExtractDate(ExtractSettings extractSettings);
        PowerTradeExtractResponse CalculatePowerTrade(Model.PowerTradeExtract powerTradeExtract);
        string GetFileName(DateTime extractDate);
    }
}
