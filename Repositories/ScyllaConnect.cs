using Cassandra;
using Cassandra.Mapping;
using ISession = Cassandra.ISession;

namespace ScyllaDBDemo.Repositories
{
    public class ScyllaConnect
    {
        private readonly Cluster _cluster = Cluster.Builder()
            .AddContactPoint("127.0.0.1")
            .WithPort(9042)
            .Build();

        private ISession? _session;
        
        public ISession Session => _session ??= _cluster.Connect();

        public ScyllaConnect() { }
        
        /// <summary>
        /// Provides a simpler interface for prepared insert statements
        /// <seealso cref="PreparedStatement"/>
        /// </summary>
        public void Insert(string tableName, string[] columns, object[] values)
        {
            var fields = string.Join(", ", columns); 
            var valueParams = string.Join(", ", columns.Select(c => $":{c}")); 

            var preparedStatement = Session.Prepare($"insert into {tableName} ({fields}) values ({valueParams})");
            var boundStatement = preparedStatement.Bind(values);

            Session.Execute(boundStatement);
        }

        /// <summary>
        /// Similar to the above insert, provides a simpler interface for prepared and batched insert statements
        /// <seealso cref="PreparedStatement"/>
        /// <seealso cref="BatchStatement"/>
        /// </summary>
        public void BatchInsert(string tableName, string[] columns, object[][] values)
        {
            var fields = string.Join(", ", columns);
            var valueParams = string.Join(", ", columns.Select(c => $":{c}")); 

            var preparedStatement = Session.Prepare($"insert into {tableName} ({fields}) values ({valueParams})");
            var boundBatchStatement = new BatchStatement();

            foreach (var vals in values) boundBatchStatement.Add(preparedStatement.Bind(vals));

            Session.Execute(boundBatchStatement);
        }
        
        // we could configure this to automatically page as well
        public IEnumerable<T> Select<T>(string tableName)
        {
            var mapper = new Mapper(Session);
            return mapper.Fetch<T>($"select * from {tableName}");
        }

        // this is a quick way to add a simple where clause, but not a good one
        public IEnumerable<T> Select<T>(string tableName, string whereClause, object whereParam)
        {
            var mapper = new Mapper(Session);
            return mapper.Fetch<T>($"select * from {tableName} where {whereClause}", whereParam);
        }
    }
}
