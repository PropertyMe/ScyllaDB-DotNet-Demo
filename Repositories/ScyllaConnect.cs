using Cassandra;

namespace ScyllaDBDemo.Repositories
{
    public class ScyllaConnect
    {
        private ICluster _cluster;
        public ScyllaConnect() {
            _cluster = Cluster.Builder()
                            .AddContactPoint("192.168.1.103")
                            .Build();
        }
        
        public Cassandra.ISession GetSession()
        {
            return _cluster.Connect("local_dev");
        }

    }
}
