using System.Net;
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

        public async Task<Result<Company>> CreateCompanyAsync(Company company)
        {
            try
            {
                await _uow.Repository.AddAsync(company);
                await _uow.CommitAsync();

                return company.ToResult();
            }
            catch (Exception ex)
            {
                return Result<Company>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<bool>> DeleteCompanyAsync(Guid id)
        {
            try
            {
                await _uow.Repository.DeleteAsync(id);
                return true.ToResult();
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<List<Company>>> GetCompaniesAsync()
        {
            try
            {
                return (await _uow.Repository.GetAllAsync()).ToResult();
            }
            catch (Exception ex)
            {
                return Result<List<Company>>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<Company>> GetCompanyByIdAsync(Guid id)
        {
            try
            {
                return (await _uow.Repository.GetByIdAsync(id)).ToResult();
            }
            catch (Exception ex)
            {
                return Result<Company>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<bool>> IsPhoneNumberUnique(string phoneNumber)
        {
            var record = await _uow.Repository.FirstOrDefaultAsync(x => x.Phone == phoneNumber);

            if (record is not null)
                return Result<bool>.Failure("Phone Number must be unique", (int)HttpStatusCode.BadRequest);

            return true.ToResult();
        }

        public async Task<Result<bool>> UpdateCompanyAsync(Company company)
        {
            try
            {
                _uow.Repository.Update(company);
                await _uow.CommitAsync();
                return true.ToResult();
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
