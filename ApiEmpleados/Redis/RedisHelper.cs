using StackExchange.Redis;

namespace ApiEmpleados.Redis
{
    public class RedisHelper
    {
        private readonly IConfiguration configuration;
        public RedisHelper(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public ConfigurationOptions GetRedisConfiguration()
        {
            var options = new ConfigurationOptions
            {
                EndPoints = { Environment.GetEnvironmentVariable("REDIS_ENDPOINT") ?? configuration.GetConnectionString("Redis")?? "" },
                Password = Environment.GetEnvironmentVariable("REDIS_PASSWORD") ?? configuration.GetConnectionString("RedisPassword") ?? "",                
                Ssl = true, // ✅ Enables SSL
                AbortOnConnectFail = false, // ✅ Prevents connection failure
                ConnectTimeout = 10000, // ✅ Increase connection timeout
                SyncTimeout = 10000,   // ✅ Increase sync timeout
                AllowAdmin = true // ✅ Allows advanced operations
            };

            return options;
        }
    }
}
