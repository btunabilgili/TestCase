using FluentValidation;
using TestCase.Application.Features.JobFeatures.Queries.Requests;

namespace TestCase.Application.Validators
{
    public class GetJobsByListingDurationValidator : AbstractValidator<GetJobsByListingDurationQueryRequest>
    {
        public GetJobsByListingDurationValidator()
        {
            RuleFor(x => x.ListingDurationInDays).NotEmpty().WithMessage("Listing duration must not be empty");
        }
    }
}
