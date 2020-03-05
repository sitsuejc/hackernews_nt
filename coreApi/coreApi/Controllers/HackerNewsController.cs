using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreApi.Controllers
{
    [Route("api/[controller]")]
	[Produces("application/json")]
	[ApiController]
    public class HackerNewsController : ControllerBase
    {
    }
}