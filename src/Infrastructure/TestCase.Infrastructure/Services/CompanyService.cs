using TestCase.Application.Common;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;
using TestCase.Infrastructure.Contexts;

namespace TestCase.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork<TestCaseContext, Company> _uow;
        public CompanyService(IUnitOfWork<TestCaseContext, Company> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<ServiceResponse<Company>> CreateCompanyAsync(Company company)
        {
            try
            {
                await _uow.Repository.AddAsync(company);
                await _uow.CommitAsync();

                return company.ToServiceResponse();
            }
            catch (Exception ex)
            {
                return ex.ToServiceResponse<Company>();
            }
        }

        public async Task<ServiceResponse<bool>> DeleteCompanyAsync(Guid id)
        {
            try
            {
                await _uow.Repository.DeleteAsync(id); 
                return true.ToServiceResponse();
            }
            catch (Exception ex)
            {
                return ex.ToServiceResponse<bool>();
            }
        }

        public async Task<ServiceResponse<List<Company>>> GetCompaniesAsync()
        {
            try
            {
                return (await _uow.Repository.GetAllAsync()).ToServiceResponse();
            }
            catch (Exception ex)
            {
                return ex.ToServiceResponse<List<Company>>();
            }
        }

        public async Task<ServiceResponse<Company>> GetCompanyByIdAsync(Guid id)
        {
            try
            {
                return (await _uow.Repository.GetByIdAsync(id)).ToServiceResponse();
            }
            catch (Exception ex)
            {
                return ex.ToServiceResponse<Company>();
            }
        }

        public async Task<ServiceResponse<bool>> UpdateCompanyAsync(Company company)
        {
            try
            {
                _uow.Repository.Update(company);
                await _uow.CommitAsync();
                return true.ToServiceResponse();
            }
            catch (Exception ex)
            {
                return ex.ToServiceResponse<bool>();
            }
        }
    }
}
