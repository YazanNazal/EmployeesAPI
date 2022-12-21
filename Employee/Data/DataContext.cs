using Microsoft.EntityFrameworkCore;

namespace EmployeeMgt.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext>options):base(options) { }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Company>? Companies { get; set; }


    }
}
