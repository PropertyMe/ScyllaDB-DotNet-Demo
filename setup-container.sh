#!/bin/bash
# setup a single node
# reactor backend -
#   this updates the advanced socket config.
#   Read more: https://java-driver.docs.scylladb.com/stable/manual/core/configuration/reference/README.html
#   the above readme links to their Java driver, but this was the only reference I could find that referenced roughly what I was looking for
docker run --name scylla -d -p 9042:9042 scylladb/scylla --reactor-backend=epoll
