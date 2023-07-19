using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLSearchWeb.src.Data;
using NLSearchWeb.src.Models;

namespace NLSearchWeb.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private NLSDbContext _context;

        public AlertsController(NLSDbContext c)
        {
            _context = c;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alerts);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Alert alert)
        {
            alert.TimeCreated = DateTime.Now;

            _context.Alerts.Add(alert);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}