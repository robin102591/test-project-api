using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashFlowService.Model
{
    public class CashFlowResponse
    {
        public decimal TotalNetPresentValue { get; set; }
        public List<NetPresentValue> NetPresentValues { get; set; }
    }
}
