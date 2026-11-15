#!/bin/sh
set -eu

cd "$(dirname "$0")/.."

docker compose --file compose.yaml up --detach --wait --wait-timeout 60 postgres
docker compose --file compose.yaml exec -T --env PGCLIENTENCODING=UTF8 postgres \
    psql --username estate --dbname estate_dev --set ON_ERROR_STOP=1 --single-transaction --file - < scripts/dev-seed.sql

printf '%s\n' 'Local development data added. Existing seed rows were left unchanged.'
