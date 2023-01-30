using Petroineos.PowerTradeExtract.Model;
using Petroineos.PowerTradeExtract.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petroineos.PowerTradeExtract.UnitTests
{
    [TestClass]
    public class CalculationServiceTest
    {
        private ICalculationService _calculationService;

        [TestInitialize]
        public void Initialize()
        {
            _calculationService = new CalculationService();
        }

        [TestMethod]
        [DataRow("2015-04-01 23:32:30", "PowerPosition_201541_2332.csv")]
        [DataRow("2015-12-31 12:16:30", "PowerPosition_20151231_1216.csv")]
        public void GetFileName_YMDFormat(string inputDate, string expectedFileName)
        {
            var extractDate = DateTime.ParseExact(inputDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var fileName = _calculationService.GetFileName(extractDate);
            Assert.AreEqual( expectedFileName, fileName, "File name is incorrect");
        }
    }
}
