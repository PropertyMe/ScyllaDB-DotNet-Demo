-- create a new keyspace
-- durable writes 'true' enables node replication (recommended)
-- later swapping to NetworkTopologyStrategy is difficult, but locally this is quite simple
-- anecdotally, there does seem to be a sizeable perf diff using simple vs. network
create keyspace if not exists local_dev 
with replication ={'class': 'SimpleStrategy', 'replication_factor': 3}
and durable_writes = true;  
