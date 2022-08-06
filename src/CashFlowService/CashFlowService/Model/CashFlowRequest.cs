using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashFlowService.Model
{
    public class CashFlowRequest
    {
        [Required]
        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Initial investment must be greater than zero")]
        public decimal InitialInvestment { get; set; }
        [Required]
        [Range(1, (double)decimal.MaxValue, ErrorMessage ="Discount rate must be greater than zero")]
        public decimal DiscountRate { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Cashflows must have at least one cash flow")]
        public List<CashFlow> CashFlows { get; set; }
    }
}
