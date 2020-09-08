using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiControllerBase : Controller
    {
        protected IConfiguration _config;
    }
}
