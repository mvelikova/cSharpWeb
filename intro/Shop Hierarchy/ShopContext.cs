using Microsoft.EntityFrameworkCore;

namespace ShopHierarchy
{
    public class ShopContext : DbContext
    {
        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(
                @"Server=.;Database=ShopDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasOne(s => s.Salesman)
                .WithMany(c => c.Customers)
                .HasForeignKey(c => c.SalesmanId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(c => c.CustomerId);

            modelBuilder.Entity<Review>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(c => c.CustomerId);

            modelBuilder.Entity<OrdersItems>()
                .HasKey(sc => new {sc.OrderId, sc.ItemId});

            modelBuilder.Entity<OrdersItems>()
                .HasOne<Order>(sc => sc.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(sc => sc.OrderId);

            modelBuilder.Entity<OrdersItems>()
                .HasOne<Item>(sc => sc.Item)
                .WithMany(o => o.Orders)
                .HasForeignKey(sc => sc.ItemId);
        }
    }
}