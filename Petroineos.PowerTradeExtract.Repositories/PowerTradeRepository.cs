using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Petroineos.PowerTradeExtract.Model;
using Services;

namespace Petroineos.PowerTradeExtract.Repositories
{
    public abstract class PowerTradeRepository
    {
        public abstract Model.PowerTradeExtract Extract(DateTime extractDate);
    }

    public class InterfacePowerTradeRepository : PowerTradeRepository, IPowerTradeRepository
    {

        public override Model.PowerTradeExtract Extract(DateTime extractDate)
        {
            var powerService = new PowerService();
            var powerTrades = powerService.GetTrades(extractDate);
            // todo: transform dll model into PowerTradeExtract
            //todo: set localtime property as well based on interval
            return new Model.PowerTradeExtract();
        }


        //todo: use async method always where possible. if required to sync then .Result can be used of async output
        public Task<Model.PowerTradeExtract> ExtractAsync(DateTime extractDate)
        {
            var powerService = new PowerService();
            var powerTrades = powerService.GetTradesAsync(extractDate);
            // todo: transform dll model into PowerTradeExtract
            //todo: set localtime property as well based on interval
            return Task.FromResult(new Model.PowerTradeExtract());
        }
    }

    public interface IPowerTradeRepository
    {
        Model.PowerTradeExtract Extract(DateTime extractDate);
        Task<Model.PowerTradeExtract> ExtractAsync(DateTime extractDate);
    }
}
