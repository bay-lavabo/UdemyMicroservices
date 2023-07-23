using StackExchange.Redis;

namespace FreeCourse.Services.Basket.Repository.Context
{
    public class RedisContext
    {
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisContext(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(db);
    }
}
