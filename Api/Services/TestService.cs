using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class TestService
{
    private readonly PaymentDbContext _context;

    public TestService(PaymentDbContext context)
    {
        _context = context;
    }

    public int ProcessAmount(int amount)
    {
        UpdateAmount(amount);
        return amount;
    }

    private void UpdateAmount(int amount)
    {
        amount = amount + 50;
    }

    public async Task<decimal> ProcessOrderTotal(int orderId)
    {
        var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == orderId);
        int customerId = order.CustomerId;
        UpdateOrder(order, customerId);
        await _context.SaveChangesAsync();
        return order.TotalAmount;
    }

    private void UpdateOrder(Order order, int customerId)
    {
        order.TotalAmount += 50;
        customerId = 0;
    }
}

