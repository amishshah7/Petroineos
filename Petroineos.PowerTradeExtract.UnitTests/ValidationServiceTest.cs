using Petroineos.PowerTradeExtract.Model;
using Petroineos.PowerTradeExtract.Services;

namespace Petroineos.PowerTradeExtract.UnitTests
{
    [TestClass]
    public class ValidationServiceTest
    {


        private IValidationService _validationService;

        [TestInitialize]
        public void Initialize()
        {
            _validationService = new ValidationService();
        }

        [TestMethod]
        public void CheckIfExtractExists_False()
        {
            var exists = _validationService.CheckIfExtractExists(new ExtractSettings());
            Assert.IsFalse(exists, "File already exists");
        }
    }
}