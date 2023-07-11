using FluentValidation;
using System.Text.RegularExpressions;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Validators
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommandRequest>
    {
        private readonly IBaseRepository<Company> _repository;
        public CreateCompanyValidator(IBaseRepository<Company> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company Name must not be empty");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address must not be empty");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone must not be empty").MustAsync(BeUniquePhoneNumber).WithMessage("Phone number is already registered").MustAsync(BeValidPhoneNumber).WithMessage("Please enter a valid phone number. '05xx-xxx-xx-xx'");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must not be empty").MustAsync(BeValidEmail).WithMessage("Please enter a valid email. 'test@test.com'");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must not be empty");
        }

        private async Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellation)
        {
            return await _repository.FirstOrDefaultAsync(x => x.Phone == phoneNumber) is null;
        }

        private Task<bool> BeValidPhoneNumber(string phoneNumber, CancellationToken cancellation)
        {
            string pattern = @"^05\d{2}-\d{3}-\d{2}-\d{2}$";
            return Task.FromResult(Regex.IsMatch(phoneNumber, pattern));
        }

        private Task<bool> BeValidEmail(string email, CancellationToken cancellation)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Task.FromResult(Regex.IsMatch(email, pattern));
        }
    }
}
