using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSession1912.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebApiSession1912.data
{
    public class HRContext:IdentityDbContext<ApplicationUser>
    {
        IConfiguration configuration;

        public HRContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("hrstring"));
            optionsBuilder.UseSqlServer("data source=localhost; initial catalog=WebApiIdentitysession1912; integrated security=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
