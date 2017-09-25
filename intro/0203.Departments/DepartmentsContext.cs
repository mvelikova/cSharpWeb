using Microsoft.EntityFrameworkCore;

namespace _0203.Departments
{
    public class DepartmentsContext : DbContext
    {
        private DbSet<Employee> Employees { get; set; }
        private DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne<Employee>(emp => emp.Manager)
                .WithMany(m => m.Subordinates)
                .HasForeignKey(emp => emp.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(
                @"Server=.;Database=DepartmentsDb;Integrated Security=True;");
        }
    }
}
