-- create a new keyspace
-- note that we have a replication factor of 1, as only 1 node is configured (at min. 3 is recommended but this is fine for demo purposes),
-- durable writes 'true' enables node replication (recommended)
create keyspace if not exists local_dev 
with replication ={'class': 'SimpleStrategy', 'replication_factor': 3}
and durable_writes = true;
