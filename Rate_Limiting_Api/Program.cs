using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("Basic", _options =>
    {
        _options.Window = TimeSpan.FromSeconds(15);
        _options.PermitLimit = 12;
        _options.QueueLimit = 8;
        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;

    });

    options.AddFixedWindowLimiter("Fixed", _options =>
    {
        _options.Window = TimeSpan.FromSeconds(12);
        _options.PermitLimit = 4;
        _options.QueueLimit = 2;
        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    options.AddSlidingWindowLimiter("Sliding", _options =>
    {
        _options.Window = TimeSpan.FromSeconds(12);
        _options.PermitLimit = 4;
        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        _options.QueueLimit = 2;
        _options.SegmentsPerWindow = 2;
    });

    options.AddTokenBucketLimiter("Token", _options =>
    {
        _options.TokenLimit = 4;
        _options.TokensPerPeriod = 4;
        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        _options.QueueLimit = 2;
        _options.ReplenishmentPeriod = TimeSpan.FromSeconds(12);
    });

    options.AddConcurrencyLimiter("Conccurency", _options =>
    {
        _options.PermitLimit = 4;
        _options.QueueLimit = 2;
        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

});
 
var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRateLimiter();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
