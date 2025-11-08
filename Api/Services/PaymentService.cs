using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PaymentService
{
    private PaymentDbContext _context;
    
    public PaymentService()
    {
        var options = new DbContextOptionsBuilder<PaymentDbContext>()
            .UseInMemoryDatabase("PaymentDb")
            .Options;
        _context = new PaymentDbContext(options);
    }

    public async Task<Payment> CreatePaymentAsync(int orderId, decimal amount)
    {
        var externalService = new ExternalPaymentService();
        
         var transactionId = externalService.ProcessPaymentAsync(amount, "4111111111111111").Result;
        
        var payment = new Payment
        {
            OrderId = orderId,
            Amount = amount,
            Status = 1,
            TransactionId = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return payment;
    }

    public async Task<bool> UpdatePaymentStatusAsync(int paymentId, int newStatus)
    {
        var payment = await _context.Payments.FindAsync(paymentId);
        
        payment.Status = newStatus;
        payment.ProcessedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<List<Payment>> GetPaymentsByStatus(int status)
    {
        var payments = await _context.Payments
            .Where(p => p.Status == status)
            .ToListAsync();
        
        return payments;
    }
}

