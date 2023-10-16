using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FSACalculation.ViewModels
{
    public class ClaimsViewModel
    {
        public int ClaimId { get; set; }

        [Required]
        [DisplayName("Date Submitted")]
        public DateTime DateSubmitted { get; set; }

        public int Status { get; set; }

        [Required]
        [DisplayName("Receipt Date")]
        public DateTime ReceiptDate { get; set; }

        [Required]
        [DisplayName("Receipt No")]
        public string? ReceiptNo { get; set; }

        [Required]
        [DisplayName("Receipt Amount")]
        public decimal ReceiptAmount { get; set; }

        [Required]
        [DisplayName("Claim Amount")]
        public decimal ClaimAmount { get; set; }

        [Required]
        [DisplayName("Total Claim Amount")]
        public decimal TotalClaimAmount { get; set; }

        [Required]
        [DisplayName("Reference No")]
        public string? ReferenceNo { get; set; }

        public int EmpId { get; set; }
    }
}
