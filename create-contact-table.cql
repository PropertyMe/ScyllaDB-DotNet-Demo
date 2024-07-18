CREATE KEYSPACE demo_keyspace
WITH replication ={'class': 'SimpleStrategy', 'replication_factor': 3}
AND durable_writes = true;