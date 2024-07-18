using Cassandra;

namespace ScyllaDBDemo.Repositories
{
    public class ScyllaConnect
    {
        private ICluster _cluster;
        public ScyllaConnect() {
            _cluster = Cluster.Builder()
                              .AddContactPoint("127.0.0.1")
                              .WithPort(9042)
                              .Build();
        }
        
        public Cassandra.ISession GetSession()
        {
            return _cluster.Connect("local_dev");
        }

    }
}
