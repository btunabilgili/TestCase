using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestCase.Application.Features.JobFeatures.Commands.Requests;
using TestCase.Application.Features.JobFeatures.Queries.Requests;

namespace TestCase.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;
        public JobController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(Guid id)
        {
            var result = await _mediator.Send(new GetJobByIdQueryRequest
            {
                Id = id
            });

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [HttpGet("listing-duration/{duration}")]
        public async Task<IActionResult> GetJobsByListingDuration(int duration)
        {
            var result = await _mediator.Send(new GetJobsByListingDurationQueryRequest
            {
                ListingDurationInDays = duration
            });

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetJobsByCompanyId(Guid companyId)
        {
            var result = await _mediator.Send(new GetJobsByComapnyIdQueryRequest 
            { 
                ComapnyId = companyId
            });

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(JobCreateCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateJob(JobUpdateCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteJob(JobDeleteCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }
    }
}
