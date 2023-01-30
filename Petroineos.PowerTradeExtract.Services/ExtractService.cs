using System;
using System.Linq.Expressions;
using Petroineos.PowerTradeExtract.Logging;
using Petroineos.PowerTradeExtract.Model;
using Petroineos.PowerTradeExtract.Repositories;

namespace Petroineos.PowerTradeExtract.Services
{
    public class ExtractService: IExtractService
    {
        private readonly ICalculationService _calculationService;
        private readonly IValidationService _validationService;
        private readonly IPowerTradeRepositoryFactory _powerTradeRepositoryFactory;
        private readonly IExportService _exportService;
        private readonly INotificationService _notificationService;
        private readonly ILoggingService _loggingService;

        public ExtractService(ICalculationService calculationService, IValidationService validationService, IPowerTradeRepositoryFactory powerTradeRepositoryFactory,
            IExportService exportService, INotificationService notificationService, ILoggingService loggingService)
        {
            _calculationService = calculationService;
            _validationService = validationService;
            _powerTradeRepositoryFactory = powerTradeRepositoryFactory;
            _exportService = exportService;
            _notificationService = notificationService;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Process power trades to generate extract. Settings to come from config file
        /// </summary>
        /// <param name="extractSettings">Pass settings from config file of windows service</param>
        public bool ExtractPowerTrades(ExtractSettings extractSettings)
        {
            try
            {
                _loggingService.LogInfo("extraction started");
                // check if extract exists
                var extractExists = _validationService.CheckIfExtractExists(extractSettings);
                if (extractExists)
                    return true; // or go to complete/notify process


                // calculate date for extract
                var extractDate = _calculationService.GetExtractDate(extractSettings);

                // get output from repository. use factory pattern. wrap up external dll interface to remove dependency
                var repository =
                    _powerTradeRepositoryFactory.GetPowerTradeRepository(extractSettings.ExtractType);
                var powerTradeExtract = repository.Extract(extractDate);


                // calc output from cal service. ie merge results etc
                var powerTradeExtractResponse =
                    _calculationService.CalculatePowerTrade(powerTradeExtract);


                // send output to export service
                var exportSuccess = _exportService.ExportPowerTrade(powerTradeExtractResponse, extractSettings.Location);


                // notify failure/success
                _notificationService.NotifyExportResult(exportSuccess);

                // complete process
                _loggingService.LogInfo("extraction completed");
                return exportSuccess;
            }
            catch (Exception exception)
            {
                _loggingService.LogException(exception);
                // can send exception to notification service
                //_notificationService.NotifyExportException(exception);
                return false;
            }

            
        }
    }

    public interface IExtractService
    {
        bool ExtractPowerTrades(ExtractSettings extractSettings);
    }
}
