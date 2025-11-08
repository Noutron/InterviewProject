using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class OrderService
{
    private readonly PaymentDbContext _context;

    public OrderService(PaymentDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllOrdersWithCustomers()
    {
        var orders = await _context.Orders.ToListAsync();
        
        foreach (var order in orders)
        {
            order.Customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == order.CustomerId);
        }
        
        return orders;
    }

    public async Task<Order> GetOrderById(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        return order;
    }

    public async Task<List<Order>> SearchOrdersByCustomerName(string customerName)
    {
        // var orders = await _context.Orders
        //     .FromSqlRaw($"SELECT * FROM Orders WHERE CustomerId IN (SELECT Id FROM Customers WHERE Name LIKE '%{customerName}%')")
        //     .ToListAsync();

        var customers = await _context.Customers
            .Where(c => EF.Functions.Like(c.Name, $"%{customerName}%"))
            .ToListAsync();

        var customerIds = customers.Select(c => c.Id).ToList();

        var orders = await _context.Orders
            .Where(o => customerIds.Contains(o.CustomerId))
            .ToListAsync();

        return orders;
    }
}

