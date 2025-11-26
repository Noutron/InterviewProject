using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890", IsVip = true, CreatedAt = DateTime.UtcNow },
            new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Phone = "0987654321", IsVip = false, CreatedAt = DateTime.UtcNow }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, CustomerId = 1, TotalAmount = 100.50m, OrderDate = DateTime.UtcNow.AddDays(-5) },
            new Order { Id = 2, CustomerId = 1, TotalAmount = 250.00m, OrderDate = DateTime.UtcNow.AddDays(-3) },
            new Order { Id = 3, CustomerId = 2, TotalAmount = 75.25m, OrderDate = DateTime.UtcNow.AddDays(-1) }
        );

        modelBuilder.Entity<Payment>().HasData(
            new Payment { Id = 1, OrderId = 1, Amount = 100.50m, Status = 1, TransactionId = "TXN001", CreatedAt = DateTime.UtcNow.AddDays(-5) },
            new Payment { Id = 2, OrderId = 2, Amount = 250.00m, Status = 2, TransactionId = "TXN002", CreatedAt = DateTime.UtcNow.AddDays(-3) }
        );
    }
}

