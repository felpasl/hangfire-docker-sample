#!/bin/bash
set -e
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
	CREATE USER $HANGFIREDB_USER WITH ENCRYPTED PASSWORD '$HANGFIREDB_PASS';
	CREATE DATABASE $HANGFIREDB;
	GRANT ALL PRIVILEGES ON DATABASE $HANGFIREDB TO $HANGFIREDB_USER;
EOSQL