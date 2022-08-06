using CashFlowService.Model;
using System.Threading.Tasks;

namespace CashFlowService.ProcessorManager
{
    public interface ICashFlowManager
    {
        Task<CashFlowResponse> CalculateNetPresentValueAsync(CashFlowRequest cashFlowRequest);
    }
}
