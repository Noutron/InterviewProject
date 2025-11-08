using Api.Data;
using Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseInMemoryDatabase("PaymentDb"));

builder.Services.AddSingleton<PaymentService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<TestService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/orders", async (OrderService orderService) =>
{
    var orders = await orderService.GetAllOrdersWithCustomers();
    return Results.Ok(orders);
});

app.MapGet("/api/orders/{id}", async (int id, OrderService orderService) =>
{
    var order = await orderService.GetOrderById(id);
    return Results.Ok(order);
});

app.MapGet("/api/orders/search", async (string name, OrderService orderService) =>
{
    var orders = await orderService.SearchOrdersByCustomerName(name);
    return Results.Ok(orders);
});

app.MapPost("/api/payments", async (CreatePaymentRequest request, PaymentService paymentService) =>
{
    var payment = await paymentService.CreatePaymentAsync(request.OrderId, request.Amount);
    return Results.Ok(payment);
});

app.MapPut("/api/payments/{id}/status", async (int id, UpdateStatusRequest request, PaymentService paymentService) =>
{
    var result = await paymentService.UpdatePaymentStatusAsync(id, request.Status);
    return Results.Ok(result);
});

app.MapGet("/api/payments/status/{status}", async (int status, PaymentService paymentService) =>
{
    var payments = await paymentService.GetPaymentsByStatus(status);
    return Results.Ok(payments);
});

app.MapGet("/api/reports/payments", async (ReportService reportService) =>
{
    var report = await reportService.GeneratePaymentReport();
    return Results.Ok(report);
});

app.MapGet("/api/test/amount", (int value, TestService testService) =>
{
    var result = testService.ProcessAmount(value);
    return Results.Ok(result);
});

app.MapGet("/api/test/order/{orderId}", async (int orderId, TestService testService) =>
{
    var total = await testService.ProcessOrderTotal(orderId);
    return Results.Ok(total);
});

app.Run();

record CreatePaymentRequest(int OrderId, decimal Amount);
record UpdateStatusRequest(int Status);