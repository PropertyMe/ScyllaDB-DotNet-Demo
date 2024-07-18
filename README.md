# ScyllaDB-DotNet-Demo
Team Six ScyllaDB Research

## Scyalla DB Documentation
https://propertyme.atlassian.net/wiki/spaces/TS/pages/3206938666/Setup+ScyllaDb+locally


## Generate DB Query

CREATE KEYSPACE local_dev WITH replication ={'class': 'SimpleStrategy', 'replication_factor': 3} and durable_writes = true;

Use local_dev;

CREATE TABLE contact (
	id UUID PRIMARY KEY,
	username TEXT,
	email TEXT,
	address TEXT
);

INSERT INTO contact (id, username, email, address)
VALUES (uuid(), 'dev user', 'dev@propertyme.com', '1 George Street, Sydney, NSW 2000');