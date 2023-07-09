﻿using TestCase.Application.Common;
using TestCase.Domain.Common;
using TestCase.Domain.Entities;

namespace TestCase.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<Result<List<Company>>> GetCompaniesAsync();
        Task<Result<Company>> GetCompanyByIdAsync(Guid id);
        Task<Result<Company>> CreateCompanyAsync(Company company);
        Task<Result<bool>> UpdateCompanyAsync(Company company);
        Task<Result<bool>> DeleteCompanyAsync(Guid id);
        Task<Result<bool>> IsPhoneNumberUnique(string phoneNumber);
    }
}
