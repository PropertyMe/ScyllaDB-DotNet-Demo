-- an example keyspace for a fictional company called "carepet"
-- that sells sensor collars for pets
create keyspace if not exists carepet
with replication = { 'class': 'NetworkTopologyStrategy', 'replication_factor': '3' }
and durable_writes = true;