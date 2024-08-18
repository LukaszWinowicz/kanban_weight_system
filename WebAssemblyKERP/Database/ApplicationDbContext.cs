using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAssemblyKERP.Models;

namespace WebAssemblyKERP.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
