using AutoMapper;
using Azure.Core;
using FSACalculation.Data.DBContext;
using FSACalculation.Data.Entities;
using FSACalculation.Data.Services;
using FSACalculation.Models;
using FSACalculation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using NuGet.Common;
using NuGet.Packaging.Rules;
using NuGet.Protocol;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace FSACalculation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClaimsRepository _repository;
        private readonly UserManager<UserLogin> _userManager;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public IMapper _mapper { get; }

        public HomeController(ILogger<HomeController> logger, 
            IClaimsRepository repository, 
            IMapper mapper, 
            UserManager<UserLogin> userManager, 
            IConfiguration configuration)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;



            _httpClient = new HttpClient();
            _baseUrl = _configuration.GetValue<string>("ConnectionStrings:BaseUrl");
            _httpClient.BaseAddress = new Uri(_baseUrl);

            

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Claims()
        {
            //return RedirectToAction("logout", "account");
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", TempData.Peek("JWToken").ToString());

                var user = _userManager.FindByNameAsync(this.User.Identity.Name).Result;
                var empId = user.empId;
                var emp = HttpContext.User.Identity;

                if (user.isAdmin == 1)
                {
                    return RedirectToAction("AdminApproval", "Home");
                }
                HttpResponseMessage response;
                
                response = _httpClient.GetAsync($"api/empclaims/{empId}").Result;

                string content = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
                var result = JsonConvert.DeserializeObject<EmployeeClaimsViewModel>(content);

                HttpContext.Session.SetString("content", content);

                return View(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateClaim(int id, ClaimsForCreateViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", TempData.Peek("JWToken").ToString());

                    var empId = id;

                    string data = HttpContext.Session.GetString("content").ToString();
                    EmployeeClaimsViewModel empClaims = JsonConvert.DeserializeObject<EmployeeClaimsViewModel>(data);

                    var FSABalance = empClaims.FSAAmount;

                    if (FSABalance > 0 && Convert.ToDecimal(viewModel.TotalClaimAmount) > FSABalance)
                    {
                        ViewBag.ResponseStatus = 0;
                        ViewBag.UserMessage = $"Your claim amount exceeded your available FSA Balance. Claimable FSA amount is {FSABalance}";
                        return View();
                    }

                    if (FSABalance == 0)
                    {
                        ViewBag.ResponseStatus = 0;
                        ViewBag.UserMessage = $"You have reached maximum claims. Claimable FSA amount is {FSABalance}";
                        return View();
                    }
                    var coverageYear = Convert.ToInt32(empClaims.CoverageYear);
                    var claimRequestYear = viewModel.ReceiptDate.Year;

                    if (claimRequestYear > coverageYear)
                    {
                        viewModel.Status = (int)Status.StatusType.Denied;
                    }

                    var values = JsonConvert.SerializeObject(viewModel);

                    var content = new StringContent(values, System.Text.Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync($"/api/empclaims/{empId}/claims", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.ResponseStatus = 0;
                        ViewBag.UserMessage = "An error occured while saving your claims. Please try again.";
                    }
                    ViewBag.ResponseStatus = 1;
                    ViewBag.UserMessage = "Claim successfully submitted.";
                    ModelState.Clear();

                    return RedirectToAction("Claims");

                }

                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult CreateClaim(int id)
        {
            return View(); 
        }

        [HttpPost]
        public async Task<ActionResult> UpdateClaim(string id, string claimId, ClaimsForUpdateViewModel viewModel) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", TempData.Peek("JWToken").ToString());

                    string data = HttpContext.Session.GetString("content").ToString();
                    EmployeeClaimsViewModel empClaims = JsonConvert.DeserializeObject<EmployeeClaimsViewModel>(data);

                    var FSABalance = empClaims.FSAAmount;

                    if (FSABalance > 0 && Convert.ToDecimal(viewModel.TotalClaimAmount) > FSABalance)
                    {
                        ViewBag.ResponseStatus = 0;
                        ViewBag.UserMessage = $"Your claim amount exceeded your available FSA Balance. Claimable FSA amount is {FSABalance}";
                        return View();
                    }

                    if (FSABalance == 0)
                    {
                        ViewBag.ResponseStatus = 0;
                        ViewBag.UserMessage = $"You have reached maximum claims. Claimable FSA amount is {FSABalance}";
                        return View();
                    }
                    var coverageYear = Convert.ToInt32(empClaims.CoverageYear);
                    var claimRequestYear = viewModel.ReceiptDate.Year;

                    if (claimRequestYear > coverageYear)
                    {
                        viewModel.Status = (int)Status.StatusType.Denied;
                    }

                    var values = JsonConvert.SerializeObject(viewModel);

                    var content = new StringContent(values, System.Text.Encoding.UTF8, "application/json");

                    var response = await _httpClient.PutAsync($"/api/empclaims/{id}/claims", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.ResponseStatus = 0;
                        ViewBag.UserMessage = "An error occured while updating your claims. Please try again.";
                        return View();
                    }
                    ViewBag.ResponseStatus = 1;
                    ViewBag.UserMessage = "Claim successfully updated.";
                    ModelState.Clear();

                    return RedirectToAction("Claims");

                }
                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult UpdateClaim(string id, string claimId)
        {
            try
            {
                ModelState.Clear();

                string data = HttpContext.Session.GetString("content");
                EmployeeClaimsViewModel claim = JsonConvert.DeserializeObject<EmployeeClaimsViewModel>(data);
                var selectedClaim = claim.Claims.Where(c => c.ClaimId == Convert.ToInt16(claimId)).FirstOrDefault();

                return View(selectedClaim);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ActionResult> DeleteClaim(string id, string claimid)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", TempData.Peek("JWToken").ToString());

                var response = await _httpClient.DeleteAsync($"/api/empclaims/{id}/claims/{claimid}");
                if (!response.IsSuccessStatusCode)
                {
                    TempData["DeleteMessage"] = "An error occured while deleting your claim. Please try again.";
                    TempData["DeleteStatus"] = 0;
                    return RedirectToAction("Claims");
                }
                TempData["DeleteStatus"] = 1;
                TempData["DeleteMessage"] = "Successfully deleted your claim.";
                return RedirectToAction("Claims");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ActionResult> UpdateStatus(int empId, int claimId, int approvalCode)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", TempData.Peek("JWToken").ToString());

                string data = HttpContext.Session.GetString("content");
                IEnumerable<ClaimsViewModel> claim = JsonConvert.DeserializeObject<IEnumerable<ClaimsViewModel>>(data);
                var selectedClaim = claim.Where(c => c.ClaimId == Convert.ToInt16(claimId) && c.EmpId == empId).FirstOrDefault();
                selectedClaim.Status = approvalCode;

                var values = JsonConvert.SerializeObject(selectedClaim);

                var content = new StringContent(values, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/empclaims/{empId}/claims/{claimId}", content);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["StatusUpdateCode"] = 0;
                    TempData["StatusUpdate"] = "An error occured while updating your claims. Please try again.";
                    return RedirectToAction("AdminApproval");
                }
                TempData["StatusUpdateCode"] = 1;
                TempData["StatusUpdate"] = "The selected claim is successfully updated.";
                ModelState.Clear();

                return RedirectToAction("AdminApproval");
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        public async Task<ActionResult> AdminApproval()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", TempData.Peek("JWToken").ToString());

                HttpResponseMessage response;
                response = _httpClient.GetAsync($"api/empclaims").Result;
                string content = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
                var result = JsonConvert.DeserializeObject<IEnumerable<ClaimsViewModel>>(content);

                HttpContext.Session.SetString("content", content);

                return View(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}