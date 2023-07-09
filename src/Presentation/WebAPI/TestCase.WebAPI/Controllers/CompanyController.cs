using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;
using TestCase.Infrastructure.Contexts;

namespace TestCase.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            var result = await _companyService.GetCompanyByIdAsync(id);

            if (!result.IsSuccess)
                return StatusCode((int)HttpStatusCode.InternalServerError, result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GettAllCompanies()
        {
            var result = await _companyService.GetCompaniesAsync();

            if (!result.IsSuccess)
                return StatusCode((int)HttpStatusCode.InternalServerError, result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(Company company)
        {
            var result = await _companyService.CreateCompanyAsync(company);

            if (!result.IsSuccess)
                return StatusCode((int)HttpStatusCode.InternalServerError, result);

            return Ok(result);
        }
    }
}
