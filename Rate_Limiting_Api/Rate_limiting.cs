using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Rate_Limiting_Api
{
    public static class Rate_limiting
    {
        public static void AddAplicationService(this IServiceCollection services)
        {

            services.AddRateLimiter(options =>
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
        } 
        }
}
