using System.Collections.Generic;

namespace CashFlowService.Model
{
    public class CashFlowResponse
    {
        public decimal TotalNetPresentValue { get; set; }
        public List<NetPresentValue> NetPresentValues { get; set; }
    }
}
