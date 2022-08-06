using CashFlowService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlowService.ProcessorManager
{
    public class CashFlowManager : ICashFlowManager
    {
        public async Task<CashFlowResponse> CalculateNetPresentValueAsync(CashFlowRequest cashFlowRequest)
        {
            if (cashFlowRequest == null) throw new Exception("CashFlowRequest is null.");

            var cashFlowResponse = new CashFlowResponse();

            var netPresentValue = await GetAllCalculatedValue(cashFlowRequest.CashFlows, cashFlowRequest.DiscountRate);

            cashFlowResponse.NetPresentValues = netPresentValue;

            cashFlowResponse.TotalNetPresentValue = netPresentValue.Sum(npv => npv.PresentValue);

            return cashFlowResponse;
        }

        private async Task<List<NetPresentValue>> GetAllCalculatedValue(List<CashFlow> cashFlowList, decimal discountRate)
        {
            return await Task.Run(() =>
            {
                return cashFlowList.Select(cf =>
                {
                    return new NetPresentValue
                    {
                        Period = cf.Period,
                        CashFlowAmount = cf.Amount,
                        PresentValue = CalculateNetPresentValue(cf, discountRate)
                    };

                }).ToList();

            });
        }

        private decimal CalculateNetPresentValue(CashFlow cashflow, decimal discountRate)
        {
            var discount = 1 + (double)(discountRate / 100);

            var ratePerPeriod = Math.Pow(discount, (double)cashflow.Period);

            return (decimal)((double)cashflow.Amount / ratePerPeriod);                      
        }
    }
}
