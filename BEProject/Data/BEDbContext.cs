using BEProject.Model;
using Microsoft.EntityFrameworkCore;

namespace BEProject.Data
{
    public class BEDbContext : DbContext
    {
        public BEDbContext(DbContextOptions<BEDbContext> options) : base (options) { }
        public DbSet<Title>? Titles { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
