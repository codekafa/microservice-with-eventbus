using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace IdentityService.Api.Core.Extensions
{
    public static class RedisRegistration
    {
        public static ConnectionMultiplexer ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConfig = ConfigurationOptions.Parse(configuration["RedisSetting:ConnectionString"]);
            redisConfig.ResolveDns = true;
            return ConnectionMultiplexer.Connect(redisConfig);
        }
    }
}
