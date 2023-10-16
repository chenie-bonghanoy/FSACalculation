using FSACalculation.DBContext;
using FSACalculation.Entities;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FSACalculation.Services
{
    public class ClaimsRepository : IClaimsRepository
    {
        private readonly FSAInfoContext _context;

        public ClaimsRepository(FSAInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddClaims(int empId, Claims claims)
        {
            var emp = await GetEmployeeByIdAsync(empId);

            if (emp != null)
            {
                emp.Claims.Add(claims);
            }
        }

        public void DeleteClaims(Claims claim)
        {
            _context.Claims.Remove(claim);
        }

        public async Task<bool> EmployeeExistAsync(int empId)
        {
            return await _context.Employees.AnyAsync(e => e.Id == empId); 
        }

        public async Task<IEnumerable<Claims?>> GetAllClaimsAsync()
        {
            return await _context.Claims.ToListAsync();
        }

        public async Task<Claims?> GetClaim(int empId, int claimId)
        {
            return await _context.Claims.Where(c => c.EmployeeId == empId && c.ID == claimId).FirstOrDefaultAsync();
        }

        public async Task<Employee?> GetClaimsAsync(int empId)
        {
            return await _context.Employees.Include(c => c.Claims).Where(e => e.Id == empId).FirstOrDefaultAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int empId)
        {
            return await _context.Employees.Include(c => c.Claims).Where(e => e.Id == empId).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
