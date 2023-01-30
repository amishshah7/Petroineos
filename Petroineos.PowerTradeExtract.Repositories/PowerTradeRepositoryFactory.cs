using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Petroineos.PowerTradeExtract.Model;

namespace Petroineos.PowerTradeExtract.Repositories
{
    public class PowerTradeRepositoryFactory: IPowerTradeRepositoryFactory
    {

        public IPowerTradeRepository GetPowerTradeRepository(ExtractEnums.ExtractType extractType)
        {
            switch (extractType)
            {
                case ExtractEnums.ExtractType.PowerTradeInterface:
                    return new InterfacePowerTradeRepository();
                default:
                    throw new InvalidEnumArgumentException("invalid repository.");
            }
        }
    }

    public interface IPowerTradeRepositoryFactory
    {
        IPowerTradeRepository GetPowerTradeRepository(ExtractEnums.ExtractType extractType);
    }
}
