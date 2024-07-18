#!/bin/bash
# setup a single node
docker run --name scylla -d -p 9042:9042 scylladb/scylla
