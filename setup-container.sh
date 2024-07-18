#!/bin/bash
# setup a single node
# reactor backend updates the advanced socket config. Read more: https://java-driver.docs.scylladb.com/stable/manual/core/configuration/reference/README.html
docker run --name scylla -d -p 9042:9042 scylladb/scylla --reactor-backend=epoll
