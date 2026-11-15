.DEFAULT_GOAL := help

ifeq ($(OS),Windows_NT)
POWERSHELL ?= powershell.exe
else
POWERSHELL ?= pwsh
endif

.PHONY: help restore build test run db-up db-stop db-reset

help:
	@echo make restore  - Restore .NET packages and local tools
	@echo make build    - Build the solution
	@echo make test     - Run the existing tests
	@echo make run      - Start PostgreSQL and run the API on http://localhost:5165
	@echo make db-up    - Start PostgreSQL
	@echo make db-stop  - Stop PostgreSQL and keep its data
	@echo make db-reset - Recreate the local schema and discard its data

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
	$(POWERSHELL) -NoProfile -File scripts/Reset-Database.ps1
