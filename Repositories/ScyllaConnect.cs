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
        public void Insert<T>(string[] columns, object[] values)
        {
            var fields = string.Join(", ", columns); 
            var valueParams = string.Join(", ", columns.Select(c => $":{c}")); 

            var preparedStatement = Session.Prepare($"insert into {GetTableName<T>()} ({fields}) values ({valueParams})");
            var boundStatement = preparedStatement.Bind(values);

            Session.Execute(boundStatement);
        }

        /// <summary>
        /// Similar to the above insert, provides a simpler interface for prepared and batched insert statements
        /// <seealso cref="PreparedStatement"/>
        /// <seealso cref="BatchStatement"/>
        /// </summary>
        public void BatchInsert<T>(string[] columns, object[][] values)
        {
            var fields = string.Join(", ", columns);
            var valueParams = string.Join(", ", columns.Select(c => $":{c}")); 

            var preparedStatement = Session.Prepare($"insert into {GetTableName<T>()} ({fields}) values ({valueParams})");
            var boundBatchStatement = new BatchStatement();

            foreach (var vals in values) boundBatchStatement.Add(preparedStatement.Bind(vals));

            Session.Execute(boundBatchStatement);
        }
        
        // we could configure this to automatically page as well
        public IEnumerable<T> Select<T>()
        {
            var mapper = new Mapper(Session);
            return mapper.Fetch<T>($"select * from {GetTableName<T>()}");
        }

        // this is a quick way to add a simple where clause, but not a good one
        public IEnumerable<T> Select<T>(string whereClause, object whereParam)
        {
            var mapper = new Mapper(Session);
            return mapper.Fetch<T>($"select * from {GetTableName<T>()} where {whereClause}", whereParam);
        }
        
        public IEnumerable<T> Select<T>(string whereClause, object[] whereParams)
        {
            var mapper = new Mapper(Session);
            return mapper.Fetch<T>($"select * from {GetTableName<T>()} where {whereClause}", whereParams);
        }

        private static string GetTableName<T>()
        {
            var val = RegexPatterns.SplitCamelCase(typeof(T).Name);

            return val.Length > 1
                ? string.Join("_", val.Select(s => s.ToLowerInvariant()))
                : val[0].ToLowerInvariant();
        }
    }
}