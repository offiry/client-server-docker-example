using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options, IConfiguration configuration, IWebHostEnvironment webHostEnvironment) : base(options)
        {
            _configuration = configuration;

            if (webHostEnvironment.EnvironmentName == Environments.Production)
            {
                Database.Migrate();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(string.Format("Data Source={0}", _configuration.GetSection("ClientDatabaseLocation")));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
