using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLSearchWeb.src.Data;
using NLSearchWeb.src.NLSE;

namespace NLSearchWeb.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private NLSDbContext _context;

        public SearchController(NLSDbContext c)
        {
            _context = c;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello world");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string query)
        {

            NLSEngine engine = new NLSEngine();

            var result = await engine.ProcessQuery(query);

            return Ok(result);
        }

    }
}