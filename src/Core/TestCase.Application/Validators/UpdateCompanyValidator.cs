using FluentValidation;
using System.Text.RegularExpressions;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Validators
{
    public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyCommandRequest>
    {
        private readonly IBaseRepository<Company> _repository;
        public UpdateCompanyValidator(IBaseRepository<Company> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty").MustAsync(CompanyExists).WithMessage("Company not found.");
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company Name must not be empty");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address must not be empty");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone must not be empty").MustAsync(async (model, phone, cancellationToken) => await BeUniquePhoneNumber(model.Phone!, model.Id)).WithMessage("Phone number is already registered").MustAsync(BeValidPhoneNumber).WithMessage("Please enter a valid phone number. '05xx-xxx-xx-xx'");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must not be empty").MustAsync(BeValidEmail).WithMessage("Please enter a valid email. 'test@test.com'");
        }

        private async Task<bool> CompanyExists(Guid companyId, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(companyId) is not null;
        }

        private async Task<bool> BeUniquePhoneNumber(string phoneNumber, Guid id)
        {
            return await _repository.FirstOrDefaultAsync(x => x.Phone == phoneNumber && x.Id != id) is null;
        }

        private Task<bool> BeValidPhoneNumber(string? phoneNumber, CancellationToken cancellation)
        {
            string pattern = @"^05\d{2}-\d{3}-\d{2}-\d{2}$";
            return Task.FromResult(Regex.IsMatch(phoneNumber!, pattern));
        }

        private Task<bool> BeValidEmail(string? email, CancellationToken cancellation)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Task.FromResult(Regex.IsMatch(email!, pattern));
        }
    }
}
