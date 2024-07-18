#!/bin/bash
cat create-contact-table.cql | docker exec -it scylla cqlsh