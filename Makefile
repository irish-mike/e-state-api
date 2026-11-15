.DEFAULT_GOAL := help
SHELL := sh

.PHONY: help restore build test run db-up db-stop db-reset db-seed

help:
	@echo make restore  - Restore .NET packages and local tools
	@echo make build    - Build the solution
	@echo make test     - Run the existing tests
	@echo make run      - Start PostgreSQL and run the API on http://localhost:5165
	@echo make db-up    - Start PostgreSQL
	@echo make db-stop  - Stop PostgreSQL and keep its data
	@echo make db-reset - Recreate the local schema and discard its data
	@echo make db-seed  - Add development data without duplicating existing seed rows

restore:
	dotnet restore
	dotnet tool restore

build:
	dotnet build

test:
	dotnet test

run: db-up
	dotnet run --project src/EState.Api --launch-profile http

db-up:
	docker compose up -d --wait postgres

db-stop:
	docker compose stop postgres

db-reset:
	sh scripts/db-reset.sh

db-seed:
	sh scripts/db-seed.sh
