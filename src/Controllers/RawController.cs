using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLSearchWeb.src.Data;

namespace NLSearchWeb.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RawController : ControllerBase
    {
        private NLSDbContext _context;

        public RawController(NLSDbContext c)
        {
            _context = c;
        }

        // public async Task<List<T>> GetRaw<T>(string query)
        // {
        //     var result = await _context.Database.ExecuteSqlRawAsync(query);
        // }

    }
}