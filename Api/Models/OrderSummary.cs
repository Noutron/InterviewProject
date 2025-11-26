namespace Api.Models;

public record OrderSummary(
    int OrderId,
    decimal OrderTotal,
    decimal Discount,
    decimal FinalAmount
);

