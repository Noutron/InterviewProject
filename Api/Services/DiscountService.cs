namespace Api.Services;

public class DiscountService
{
    public decimal CalculateLoyaltyDiscount(decimal orderTotal, int totalOrderCount, bool isVipCustomer)
    {
        if (orderTotal <= 0)
            return 0;

        decimal discountPercentage = 0;

        if (isVipCustomer)
            discountPercentage += 10;

        if (totalOrderCount >= 20)
            discountPercentage += 15;
        else if (totalOrderCount >= 10)
            discountPercentage += 10;
        else if (totalOrderCount >= 5)
            discountPercentage += 5;

        discountPercentage = Math.Min(discountPercentage, 25);

        return orderTotal * discountPercentage / 100;
    }
}

