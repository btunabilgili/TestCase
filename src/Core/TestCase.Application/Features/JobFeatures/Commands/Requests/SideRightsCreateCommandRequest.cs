using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCase.Domain.Enums;

namespace TestCase.Application.Features.JobFeatures.Commands.Requests
{
    public class SideRightsCreateCommandRequest
    {
        public required SideRightTypes SideRight { get; set; }
    }
}
