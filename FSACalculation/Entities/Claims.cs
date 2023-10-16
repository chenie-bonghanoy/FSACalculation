using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSACalculation.Entities
{
    public class Claims
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int Status { get; set; }
        public DateTime ReceiptDate { get; set; }
        [StringLength(10)]
        public string? ReceiptNo { get; set; }
        [Precision(10 , 2)]
        public decimal ReceiptAmount { get; set; }
        [Precision(10, 2)]
        public decimal ClaimAmount { get; set; }
        [Precision(10, 2)]
        public decimal TotalClaimAmount { get; set; }
        [StringLength(10)]
        public string? ReferenceNo { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
