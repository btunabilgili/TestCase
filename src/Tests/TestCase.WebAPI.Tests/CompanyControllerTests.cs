using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestCase.Application.Common;
using TestCase.Application.Features.CompanyFeatures.Commands.Requests;
using TestCase.Application.Features.CompanyFeatures.Commands.Responses;
using TestCase.Application.Features.CompanyFeatures.Queries.Requests;
using TestCase.Application.Features.CompanyFeatures.Queries.Responses;
using TestCase.WebAPI.Controllers;

namespace TestCase.WebAPI.Tests
{
    public class CompanyControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CompanyController _companyController;

        public CompanyControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _companyController = new CompanyController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsOkResult()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var queryResult = new GetCompanyByIdQueryResponse
            {
                Id = companyId,
                CompanyName = "Test Company",
                Address = "Test Address",
                Phone = "Test Phone",
                Email = "Test Email",
                RemainingJobCount = 2
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCompanyByIdQueryRequest>(), CancellationToken.None)).ReturnsAsync(queryResult.ToResult());

            // Act
            var result = await _companyController.GetCompanyById(companyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Result<GetCompanyByIdQueryResponse>>(okResult.Value);

            if (response?.Data is null || !response.IsSuccess)
            {
                Assert.Fail("Response is null");
                return;
            }

            Assert.True(response.IsSuccess, "Unsuccessful");
            Assert.Equal(companyId, response.Data.Id);
            Assert.Equal("Test Company", response.Data.CompanyName);
            Assert.Equal("Test Address", response.Data.Address);
            Assert.Equal("Test Phone", response.Data.Phone);
            Assert.Equal(2, response.Data.RemainingJobCount);
        }

        [Fact]
        public async Task GettAllCompanies_ReturnsOkResult()
        {
            // Arrange
            var queryResult = new List<GetAllCompaniesQueryResponse>
            {
                new GetAllCompaniesQueryResponse
                {
                    Id = Guid.NewGuid(),
                    CompanyName = "Company 1",
                    Address = "Address 1",
                    Phone = "Phone 1",
                    Email = "Email 1",
                    RemainingJobCount = 1
                },
                new GetAllCompaniesQueryResponse
                {
                    Id = Guid.NewGuid(),
                    CompanyName = "Company 2",
                    Address = "Address 2",
                    Phone = "Phone 2",
                    Email = "Email 2",
                    RemainingJobCount = 2
                }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCompaniesQueryRequest>(), CancellationToken.None)).ReturnsAsync(queryResult.ToResult());

            // Act
            var result = await _companyController.GettAllCompanies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Result<List<GetAllCompaniesQueryResponse>>>(okResult.Value);

            if (response?.Data is null || !response.IsSuccess)
            {
                Assert.Fail("Response is null or unsuccessfull");
                return;
            }

            Assert.Equal(2, response.Data.Count);
            Assert.Contains(response.Data, c => c.CompanyName == "Company 1");
            Assert.Contains(response.Data, c => c.CompanyName == "Company 2");
        }

        [Fact]
        public async Task CreateCompany_ReturnsOkResult()
        {
            // Arrange
            var createCompanyCommand = new CreateCompanyCommandRequest
            {
                Password = "New Password",
                CompanyName = "New Company",
                Address = "New Address",
                Phone = "New Phone",
                Email = "New Email"
            };

            var commandResult = new CreateCompanyCommandResponse
            {
                Id = Guid.NewGuid(),
                CompanyName = createCompanyCommand.CompanyName,
                Address = createCompanyCommand.Address,
                Phone = createCompanyCommand.Phone,
                Email = createCompanyCommand.Email,
                RemainingJobCount = 2
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCompanyCommandRequest>(), CancellationToken.None)).ReturnsAsync(commandResult.ToResult());

            // Act
            var result = await _companyController.CreateCompany(createCompanyCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Result<CreateCompanyCommandResponse>>(okResult.Value);

            if (response?.Data is null)
            {
                Assert.Fail("Response is null");
                return;
            }

            Assert.True(response.IsSuccess, "unsuccessful");
            Assert.Equal(createCompanyCommand.CompanyName, response.Data.CompanyName);
            Assert.Equal(createCompanyCommand.Address, response.Data.Address);
            Assert.Equal(createCompanyCommand.Phone, response.Data.Phone);
        }

        [Fact]
        public async Task UpdateCompany_ReturnsOkResult()
        {
            // Arrange
            var updateCompanyCommand = new UpdateCompanyCommandRequest
            {
                Id = Guid.NewGuid(),
                CompanyName = "Updated Company",
                Address = "Updated Address",
                Phone = "Updated Phone",
                Email = "Updated Email"
            };

            var commandResult = new UpdateCompanyCommandResponse
            {
                Id = updateCompanyCommand.Id,
                CompanyName = updateCompanyCommand.CompanyName,
                Address = updateCompanyCommand.Address,
                Phone = updateCompanyCommand.Phone,
                Email = updateCompanyCommand.Email,
                RemainingJobCount = 2
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCompanyCommandRequest>(), CancellationToken.None)).ReturnsAsync(commandResult.ToResult());

            // Act
            var result = await _companyController.UpdateCompany(updateCompanyCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Result<UpdateCompanyCommandResponse>>(okResult.Value);

            if (response?.Data is null)
            {
                Assert.Fail("Response is null");
                return;
            }

            Assert.Equal(updateCompanyCommand.CompanyName, response.Data.CompanyName);
            Assert.Equal(updateCompanyCommand.Address, response.Data.Address);
            Assert.Equal(updateCompanyCommand.Phone, response.Data.Phone);
            Assert.True(response.IsSuccess, "Unsuccessful");
        }
    }
}