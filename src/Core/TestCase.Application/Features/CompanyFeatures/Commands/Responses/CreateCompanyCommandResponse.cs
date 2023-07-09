using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCase.Application.Features.CompanyFeatures.Commands.Responses
{
    public class CreateCompanyCommandResponse
    {
        public Guid Id { get; set; }
        public required string CompanyName { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
    }
}
