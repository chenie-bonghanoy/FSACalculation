using AutoMapper;
using FSACalculation.Data.Entities;
using FSACalculation.Data.Services;
using FSACalculation.Models;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSACalculation.APIControllers
{
    [ApiController]
    [Route("api/empclaims")]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
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
            try
            {
                var result = await _repository.GetAllClaimsAsync();

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<ClaimsViewModel>>(result));
            }
            catch (Exception)
            {
                return BadRequest($"Failed to return requested items.");
            }
        }

        [HttpGet("{empId}")]
        public async Task<ActionResult<EmployeeClaimsViewModel>> GetClaims(int empId)
        {
            try
            {
                if (!await _repository.EmployeeExistAsync(empId))
                {
                    return NotFound();
                }

                var result = await _repository.GetClaimsAsync(empId);

                if (result == null) return NotFound();

                return Ok(_mapper.Map<EmployeeClaimsViewModel>(result));
            }
            catch (Exception)
            {
                return BadRequest($"Failed to return requested items.");
            }
        }

        [HttpPost("{empId}/claims")]
        public async Task<ActionResult<Claims>> CreateClaims(int empId, ClaimsForCreateViewModel claims)
        {
            try
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
            catch (Exception)
            {
                return BadRequest($"Failed to create items.");
            }
        }
        [HttpPut("{empId}/claims")]
        public async Task<ActionResult<Claims>> UpdateClaims(int empId, ClaimsForUpdateViewModel claims)
        {
            try
            {
                if (!await _repository.EmployeeExistAsync(empId))
                {
                    return NotFound();
                }

                var empClaim = await _repository.GetClaim(empId, claims.ClaimId);

                if (empClaim == null) return NotFound();

                _mapper.Map(claims, empClaim);

                await _repository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest($"Failed to update items.");
            }
        }

        [HttpPut("{empId}/claims/{claimId}")]
        public async Task<ActionResult> AdminApprovalAsync(int empId, int claimId, ClaimsViewModel viewModel)
        {
            try
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
            catch (Exception)
            {
                return BadRequest($"Failed to update items.");
            }
        }

        [HttpDelete("{empId}/claims/{claimId}")]
        public async Task<ActionResult> DeleteClaims(int empId, int claimId)
        {
            try
            {
                if (!await _repository.EmployeeExistAsync(empId))
                {
                    return NotFound();
                }

                var empClaim = await _repository.GetClaim(empId, claimId);

                if (empClaim == null) return NotFound();

                _repository.DeleteClaims(empClaim);

                await _repository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest($"Failed to delete items.");
            }
        }
    }
}
