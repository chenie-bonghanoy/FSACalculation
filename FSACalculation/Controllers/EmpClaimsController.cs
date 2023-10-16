using AutoMapper;
using FSACalculation.Entities;
using FSACalculation.Models;
using FSACalculation.Services;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FSACalculation.Controllers
{
    [ApiController]
    [Route("api/empclaims")]
    public class EmpClaimsController : ControllerBase
    {
        private readonly IClaimsRepository _repository;
        private readonly IMapper _mapper;

        public EmpClaimsController(IClaimsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimsViewModel>>> GetAllClaims()
        {
            var result = await _repository.GetAllClaimsAsync();

            if (result == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ClaimsViewModel>>(result));
        }

        [HttpGet("{empId}")]
        public async Task<ActionResult<EmployeeClaimsViewModel>> GetClaims(int empId)
        {
            if (!await _repository.EmployeeExistAsync(empId))
            {
                return NotFound();
            }

            var result = await _repository.GetClaimsAsync(empId);

            if (result == null) return NotFound();

            return Ok(_mapper.Map<EmployeeClaimsViewModel>(result));
        }

        [HttpPost("{empId}/claims")]
        public async Task<ActionResult<Claims>> CreateClaims(int empId, ClaimsForCreateViewModel claims)
        {
            if (!await _repository.EmployeeExistAsync(empId))
            {
                return NotFound();
            }

            var empClaims = _mapper.Map<Claims>(claims);

            await _repository.AddClaims(empId, empClaims);

            await _repository.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{empId}/claims")]
        public async Task<ActionResult<Claims>> UpdateClaims(int empId, ClaimsForUpdateViewModel claims)
        {
            if (!await _repository.EmployeeExistAsync(empId))
            {
                return NotFound();
            }

            var empClaim = await _repository.GetClaim(empId, claims.ClaimId);

            if (empClaim == null) return NotFound();

            _mapper.Map(claims, empClaim);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{empId}/claims/{claimId}")]
        public async Task<ActionResult> AdminApprovalAsync(int empId, int claimId, ClaimsViewModel viewModel) 
        {
            if (!await _repository.EmployeeExistAsync(empId))
            {
                return NotFound();
            }

            var employee = await _repository.GetEmployeeByIdAsync(empId);
            var mappedEmployee = _mapper.Map<EmployeeViewModel>(employee);
            var updatedEmployee = mappedEmployee;

            if (viewModel.Status == (int)Status.StatusType.Approved)
            {
                updatedEmployee.FSAAmount -= viewModel.TotalClaimAmount;
            }

            _mapper.Map(updatedEmployee, employee);

            var empClaim = await _repository.GetClaim(empId, claimId);

            if (empClaim == null) return NotFound();
            _mapper.Map(viewModel, empClaim);

            await _repository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{empId}/claims/{claimId}")]
        public async Task<ActionResult> DeleteClaims(int empId, int claimId) 
        {
            if (!await _repository.EmployeeExistAsync(empId))
            {
                return NotFound();
            }

            var empClaim = await _repository.GetClaim(empId, claimId);
            
            if (empClaim == null) return NotFound();

            _repository.DeleteClaims(empClaim);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
