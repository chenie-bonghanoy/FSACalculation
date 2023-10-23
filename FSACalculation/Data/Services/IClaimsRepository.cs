using FSACalculation.Data.Entities;
using FSACalculation.ViewModels;
using System.Security.Claims;

namespace FSACalculation.Data.Services
{
    public interface IClaimsRepository
    {
        Task<Employee?> GetClaimsAsync(int empId);

        Task<bool> EmployeeExistAsync(int empId);

        Task AddClaims(int empId, Claims claims);

        Task<Employee?> GetEmployeeByIdAsync(int empId);

        void DeleteClaims(Claims claim);

        Task<bool> SaveChangesAsync();

        Task<Claims?> GetClaim(int empId, int claimId);

        Task<IEnumerable<Claims?>> GetAllClaimsAsync();

    }
}
