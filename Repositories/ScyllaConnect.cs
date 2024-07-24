using Cassandra;

namespace ScyllaDBDemo.Repositories
{
    public class ScyllaConnect
    {
        private readonly Cluster _cluster = Cluster.Builder()
            .AddContactPoint("127.0.0.1")
            .WithPort(9042)
            .Build();

        public Cassandra.ISession GetSession() => _cluster.Connect();
    }
}
