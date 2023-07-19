
using Microsoft.EntityFrameworkCore;
using NLSearchWeb.src.Models;

namespace NLSearchWeb.src.Data
{
    public class NLSDbContext : DbContext
    {
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public NLSDbContext(DbContextOptions<NLSDbContext> options) : base(options)
        {

        }
    }
}