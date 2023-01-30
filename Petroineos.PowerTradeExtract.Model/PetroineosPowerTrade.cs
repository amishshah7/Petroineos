using System;
using System.Collections.Generic;
using System.Text;

namespace Petroineos.PowerTradeExtract.Model
{

    public class PowerTradeExtract
    {
        public IEnumerable<PetroineosPowerTrade> PowerTrade { get; set; }
        public ExtractEnums.ExtractType ExtractType { get; set; }
        public DateTime ExtractDate { get; set; }
    }

    public class PetroineosPowerTrade
    {
        public DateTime Date { get; set; }
        public IEnumerable<PetroineosPowerPeriod> PowerPeriods { get; set; }
    }

    public class PetroineosPowerPeriod
    {
        public int Period { get; set; }
        public double Volume { get; set; }
        public string LocalTime { get; set; }
    }

    public class PowerTradeExtractResponse
    {
        public PetroineosPowerTrade PowerTrade { get; set; }
        public string FileName { get; set; }

    }
}
