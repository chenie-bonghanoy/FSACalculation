using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FSACalculation.ViewModels
{
    public class ClaimsForUpdateViewModel
    {
        public int ClaimId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date Submitted")]
        public DateTime DateSubmitted { get; set; }

        public int Status { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Receipt Date")]
        public DateTime ReceiptDate { get; set; }

        [Required]
        [DisplayName("Receipt No")]
        [MaxLength(10)]
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
        [DisplayName("Reference No")]
        [MaxLength(10)]
        public string? ReferenceNo { get; set; }
    }
}
