using System.ComponentModel.DataAnnotations;

namespace CashFlowService.Model
{
    public class CashFlow
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Period must be between {1} and {2}")]
        public int Period { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be between {1} and {2}")]
        public decimal Amount { get; set; }
    }
}
