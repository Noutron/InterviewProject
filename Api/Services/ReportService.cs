using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ReportService
{
    private readonly PaymentDbContext _context;

    public ReportService(PaymentDbContext context)
    {
        _context = context;
    }

    public async Task<string> GeneratePaymentReport()
    {
        var payments = await _context.Payments.ToListAsync();
        
        var report = "";
        report = report + "Payment Report\n";
        report = report + "==============\n";
        
        foreach (var payment in payments)
        {
            report = report + $"Payment ID: {payment.Id}\n";
            report = report + $"Amount: {payment.Amount}\n";
            report = report + $"Status: {payment.Status}\n";
            report = report + "---\n";
        }
        
        return report;
    }
}

