using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashFlowService.Model
{
    public class CashFlowRequest
    {
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Discount rate must be between {1} and {2}.")]
        public decimal DiscountRate { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Cashflows must have at least one cash flow")]
        public List<CashFlow> CashFlows { get; set; }
    }
}
