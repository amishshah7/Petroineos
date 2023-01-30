using System;
using System.Collections.Generic;
using System.Text;
using Petroineos.PowerTradeExtract.Model;

namespace Petroineos.PowerTradeExtract.Services
{
    public class ValidationService: IValidationService
    {
        public bool CheckIfExtractExists(ExtractSettings extractSettings)
        {
            //todo: check if file exists in the output folder location
            return false;
        }
    }

    public interface IValidationService
    {
        bool CheckIfExtractExists(ExtractSettings extractSettings);
    }
}
