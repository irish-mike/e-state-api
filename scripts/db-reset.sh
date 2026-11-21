#!/bin/sh
set -eu

cd "$(dirname "$0")/.."

sql_file=$(mktemp)
trap 'rm -f "$sql_file"' 0

export ConnectionStrings__PostgreSql='Host=localhost;Database=e_state_dev;Username=e_state;Password=e_state_dev_password'
export ASPNETCORE_ENVIRONMENT=Development

dotnet tool restore
dotnet ef dbcontext script --project src/EState.Infrastructure --startup-project src/EState.Api --output "$sql_file"

docker compose --file compose.yaml up --detach --wait --wait-timeout 60 postgres

printf '%s\n' 'Recreating the local e_state_dev schema. All existing rows in this schema will be discarded.'
docker compose --file compose.yaml exec -T --env PGCLIENTENCODING=UTF8 postgres \
    psql --username e_state --dbname e_state_dev --set ON_ERROR_STOP=1 --single-transaction \
    --command 'DROP SCHEMA public CASCADE; CREATE SCHEMA public;' --file - < "$sql_file"

printf '%s\n' 'Local database schema recreated from the current EF model.'
