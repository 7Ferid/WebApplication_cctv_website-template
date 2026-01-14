using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Reflection;
using WebApplication_cctv_website_template.Models;

namespace WebApplication_cctv_website_template.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Position> Postitions { get; set; }


       
    }
}
