using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Petroineos.PowerTradeExtract.Model;

namespace Petroineos.PowerTradeExtract.Services
{
    public class ExportService : IExportService
    {
        public bool ExportPowerTrade(PowerTradeExtractResponse powerTradeExtractResponse,
            string location)
        {
            //todo: use any 3rd party to export. eg csvhelper. I have used SpreadSheetGear very extensively recently.
            // export hear names too and use tostring extension method to convert int to string
            using (var file = File.CreateText($"{location}/{powerTradeExtractResponse.FileName}"))
            {
                file.Write("Local Time");
                file.Write(',');
                file.Write("Volume");
                file.WriteLine();
                foreach (var powerPeriod in powerTradeExtractResponse.PowerTrade.PowerPeriods)
                {
                    //if (String.IsNullOrEmpty(arr)) continue;
                    //file.Write(arr[0]);
                    //todo: use local time here instead of period
                    file.Write(powerPeriod.Period);
                    file.Write(',');
                    file.Write(powerPeriod.Volume);
                    file.WriteLine();
                }
            }

            return true;
        }
    }

    public interface IExportService
    {
        bool ExportPowerTrade(PowerTradeExtractResponse powerTradeExtractResponse, string location);
    }
}
