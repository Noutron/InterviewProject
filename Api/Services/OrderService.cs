using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class OrderService
{
    private readonly PaymentDbContext _context;
    private readonly DiscountService _discountService;

    public OrderService(PaymentDbContext context, DiscountService discountService)
    {
        _context = context;
        _discountService = discountService;
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
    
    public async Task<OrderSummary> GetOrderSummary(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        var customerOrderCount = await _context.Orders
            .CountAsync(o => o.CustomerId == order.CustomerId);

        var discount = _discountService.CalculateLoyaltyDiscount(
            order.TotalAmount,
            customerOrderCount,
            order.Customer.IsVip
        );

        return new OrderSummary(
            order.Id,
            order.TotalAmount,
            discount,
            order.TotalAmount - discount
        );
    }
}

