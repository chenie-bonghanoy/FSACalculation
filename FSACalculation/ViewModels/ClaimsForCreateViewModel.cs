using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FSACalculation.ViewModels
{
    public class ClaimsForCreateViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date Submitted")]
        public DateTime DateSubmitted { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Receipt Submitted")]
        public DateTime ReceiptDate { get; set; }
        [Required]
        [MaxLength(10)]
        [DisplayName("Receipt No")]
        public string? ReceiptNo { get; set; }
        [Required]
        [Range(0, 99999999.99)]
        [DisplayName("Receipt Amount")]
        public decimal ReceiptAmount { get; set; }
        [Required]
        [Range(0, 99999999.99)]
        [DisplayName("Claim Amount")]
        public decimal ClaimAmount { get; set; }
        [Required]
        [Range(0, 99999999.99)]
        [DisplayName("Total Claim Amount")]
        public decimal TotalClaimAmount { get; set; }
        [Required]
        [MaxLength(10)]
        [DisplayName("Reference No")]
        public string? ReferenceNo { get; set; }
    }
}
