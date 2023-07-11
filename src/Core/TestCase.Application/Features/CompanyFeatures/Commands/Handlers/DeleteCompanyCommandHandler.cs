using AutoMapper;
using FluentValidation;
using MediatR;
using System.Net;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;

namespace TestCase.Application.Features.CompanyFeatures.Commands.Handlers
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommandRequest, Result<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<DeleteCompanyCommandRequest> _validator;
        public DeleteCompanyCommandHandler(IUnitOfWork uow, 
            IValidator<DeleteCompanyCommandRequest> validator)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<bool>> Handle(DeleteCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result<bool>.Failure(string.Join(",", validationResult.Errors), (int)HttpStatusCode.BadRequest);

            await _uow.CompanyRepository.DeleteAsync(request.Id);
            await _uow.SaveChangesAsync();

            return true.ToResult();
        }
    }
}
