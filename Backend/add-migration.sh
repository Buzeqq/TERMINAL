#!/bin/bash

dotnet ef migrations add $2 -c $1 -s ./src/Terminal.Backend.Api/Terminal.Backend.Api.csproj -p ./src/Terminal.Backend.Infrastructure/Terminal.Backend.Infrastructure.csproj
