using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Domain.Dto.Responses;

namespace TestProject.WebAPI.Controllers
{
    [ApiController]
    public class HealthCheckController : ApiControllerBase
    {
        [HttpGet("/HealthCheck")]
        public async Task<ActionResult<HealthCheckResponse>> HealthCheck()
        {
            return Ok(new HealthCheckResponse { IsHealthy = 1 });
        }
    }
}
