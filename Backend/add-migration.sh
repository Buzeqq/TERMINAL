#!/bin/bash

case "$1" in
	"TerminalDbContext") migrationsDir="Data" ;;
	"UserDbContext") migrationsDir="Users" ;;
esac

dotnet ef migrations add $2 -c $1 -s ./src/Terminal.Backend.Api/Terminal.Backend.Api.csproj -p ./src/Terminal.Backend.Infrastructure/Terminal.Backend.Infrastructure.csproj -o ./DAL/Migrations/${migrationsDir}
