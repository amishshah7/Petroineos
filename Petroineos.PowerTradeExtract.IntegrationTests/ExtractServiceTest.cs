using Moq;
using Petroineos.PowerTradeExtract.Logging;
using Petroineos.PowerTradeExtract.Model;
using Petroineos.PowerTradeExtract.Repositories;
using Petroineos.PowerTradeExtract.Services;

namespace Petroineos.PowerTradeExtract.IntegrationTests
{
    [TestClass]
    public class ExtractServiceTest
    {

        private IExportService _exportService;
        private Mock<ILoggingService> _loggingServiceMock;
        private ICalculationService _calculationService;
        private IValidationService _validationService;
        private INotificationService _notificationService;
        private IExtractService _extractService;
        private Mock<IPowerTradeRepository> _powerTradeRepositoryMock;
        private Mock<InterfacePowerTradeRepository> _interfaceMock;
        private Mock<IPowerTradeRepositoryFactory> _factoryMock;

        //ToDo: Also use teardown to clean up
        [TestInitialize]
        public void Initialize()
        {
            _loggingServiceMock = new Mock<ILoggingService>();
            _powerTradeRepositoryMock = new Mock<IPowerTradeRepository>();
            _interfaceMock = new Mock<InterfacePowerTradeRepository>();
            _factoryMock = new Mock<IPowerTradeRepositoryFactory>();
            _exportService = new ExportService();
            _calculationService = new CalculationService();
            _notificationService = new NotificationService();
            _validationService = new ValidationService();
            _notificationService = new NotificationService();
            _extractService = new ExtractService(_calculationService, _validationService, _factoryMock.Object,
                _exportService, _notificationService,
                _loggingServiceMock.Object);
        }



        [TestMethod]
        public void ExtractPowerTrades_Success()
        {

            string fileLocation = GetFileLocation();
            var extractSettings = new ExtractSettings
            {
                ExtractType = ExtractEnums.ExtractType.PowerTradeInterface,
                Location = fileLocation
            };

            
            var powerTradeExtract = GetPowerTradeExtract();
            _powerTradeRepositoryMock.Setup(p => p.Extract(It.IsAny<DateTime>())).Returns(powerTradeExtract);
            _factoryMock.Setup(f => f.GetPowerTradeRepository(It.IsAny<ExtractEnums.ExtractType>()))
                .Returns(_powerTradeRepositoryMock.Object);
            var extractResult = _extractService.ExtractPowerTrades(extractSettings);
            Assert.IsTrue(extractResult, "Extract is not successful");
            // todo: check actual content of the file as well
        }

        private string GetFileLocation()
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
            path = Path.Combine(path, $"TestOutput");

            return path;
        }


        [TestMethod]
        public void ExtractPowerTrades_Fail()
        {
        }

        [TestMethod]
        public void ExtractPowerTrades_Exception()
        {
        }

        private Model.PowerTradeExtract GetPowerTradeExtract()
        {
            
            var period1 = new List<PetroineosPowerPeriod>();
            for (int i = 1; i < 25; i++)
            {
                period1.Add(new PetroineosPowerPeriod { Period = i, Volume = 100});
            }

            var period2 = new List<PetroineosPowerPeriod>();
            for (int i = 1; i < 12; i++)
            {
                period2.Add(new PetroineosPowerPeriod { Period = i, Volume = 50 });
            }
            for (int i = 12; i < 25; i++)
            {
                period2.Add(new PetroineosPowerPeriod { Period = i, Volume = -20 });
            }

            var powerTrade1 = new PetroineosPowerTrade
            {
                Date = new DateTime(2015, 04, 01),
                PowerPeriods = period1
            };

            var powerTrade2 = new PetroineosPowerTrade
            {
                Date = new DateTime(2015, 04, 01),
                PowerPeriods = period2
            };

            var powerTradeExtract = new Model.PowerTradeExtract
            {
                ExtractType = ExtractEnums.ExtractType.PowerTradeInterface,
                PowerTrade = new List<PetroineosPowerTrade>() {powerTrade1, powerTrade2},
                ExtractDate = DateTime.Now
            };
            return powerTradeExtract;
        }
    }
}