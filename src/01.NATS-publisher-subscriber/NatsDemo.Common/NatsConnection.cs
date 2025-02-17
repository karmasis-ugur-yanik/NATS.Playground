using NATS.Client;

namespace NatsDemo.Common
{
    public static class NatsConnection
    {
        public static IConnection GetConnection()
        {
            var opts = ConnectionFactory.GetDefaultOptions();
            opts.Url = Environment.GetEnvironmentVariable("NATS_URL") ?? "nats://nats-1:4222";
            opts.MaxReconnect = 3;
            opts.ReconnectWait = 2000;

            return new ConnectionFactory().CreateConnection(opts);
        }
    }
}
