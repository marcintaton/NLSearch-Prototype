using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLSearchWeb.src.Data;

namespace NLSearchWeb.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private NLSDbContext _context;

        public MoviesController(NLSDbContext c)
        {
            _context = c;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Movies);
        }

    }
}