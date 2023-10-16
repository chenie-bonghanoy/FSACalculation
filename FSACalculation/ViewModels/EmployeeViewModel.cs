using System.ComponentModel.DataAnnotations;

namespace FSACalculation.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(200)]

        public string FirstName { get; set; }

        public decimal FSAAmount { get; set; }

        public string CoverageYear { get; set; }
    }
}
