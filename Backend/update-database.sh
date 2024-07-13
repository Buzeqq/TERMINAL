#!/bin/bash

dotnet ef database update -c $1 -s ./src/Terminal.Backend.Api/Terminal.Backend.Api.csproj -p ./src/Terminal.Backend.Infrastructure/Terminal.Backend.Infrastructure.csproj
