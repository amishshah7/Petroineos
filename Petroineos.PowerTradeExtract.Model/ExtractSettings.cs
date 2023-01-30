using System;

namespace Petroineos.PowerTradeExtract.Model
{
    public class ExtractSettings
    {
        public string Location { get; set; }
        public int FrequencyInMinutes { get; set; }
        public string OutputDateFormat { get; set; }
    }
}
