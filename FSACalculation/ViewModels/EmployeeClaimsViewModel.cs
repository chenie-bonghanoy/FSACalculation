using FSACalculation.Entities;
using System.ComponentModel.DataAnnotations;

namespace FSACalculation.ViewModels
{
    public class EmployeeClaimsViewModel
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
        public int ClaimsCount
        {
            get
            {
                return Claims.Count;
            }
        }

        public ICollection<ClaimsForUpdateViewModel> Claims { get; set; } = new List<ClaimsForUpdateViewModel>();
    }
}
