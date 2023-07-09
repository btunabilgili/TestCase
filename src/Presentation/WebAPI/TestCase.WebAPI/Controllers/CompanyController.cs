﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Features.CompanyFeatures.Queries.Requests;
using TestCase.Application.Interfaces;
using TestCase.Domain.Entities;
using TestCase.Infrastructure.Contexts;

namespace TestCase.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompanyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            var result = await _mediator.Send(new GetCompanyByIdQueryRequest
            {
                Id = id
            });

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GettAllCompanies()
        {
            var result = await _mediator.Send(new GetAllCompaniesQueryRequest());

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(UpdateCompanyCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }
    }
}
