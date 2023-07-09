using TestCase.Application.Common;
using TestCase.Domain.Common;
using TestCase.Domain.Entities;

namespace TestCase.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<ServiceResponse<List<Company>>> GetCompaniesAsync();
        Task<ServiceResponse<Company>> GetCompanyByIdAsync(Guid id);
        Task<ServiceResponse<Company>> CreateCompanyAsync(Company company);
        Task<ServiceResponse<bool>> UpdateCompanyAsync(Company company);
        Task<ServiceResponse<bool>> DeleteCompanyAsync(Guid id);
    }
}
