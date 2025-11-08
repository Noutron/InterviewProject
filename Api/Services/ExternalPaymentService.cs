using System.Text;

namespace Api.Services;

public class ExternalPaymentService
{
    public async Task<string> ProcessPaymentAsync(decimal amount, string cardNumber)
    {
        var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(30);
        
        var payload = $"{{\"amount\":{amount},\"card\":\"{cardNumber}\"}}";
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        
        var response = await client.PostAsync("https://payment-gateway.example.com/api/process", content);
        var result = await response.Content.ReadAsStringAsync();
        
        return result;
    }

    public string GenerateTransactionId(int orderId, int customerId)
    {
        var result = "";
        for (int i = 0; i < 10; i++)
        {
            result = result + "TXN-" + orderId + "-" + customerId + "-" + i;
            if (i < 9)
                result = result + ",";
        }
        return result;
    }
}

