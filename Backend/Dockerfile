FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.props .
COPY src/Terminal.Backend.Api/*.csproj ./Terminal.Backend.Api/
COPY src/Terminal.Backend.Application/*.csproj ./Terminal.Backend.Application/
COPY src/Terminal.Backend.Core/*.csproj ./Terminal.Backend.Core/
COPY src/Terminal.Backend.Infrastructure/*.csproj ./Terminal.Backend.Infrastructure/

FROM build AS publish
COPY /src .
WORKDIR /src/Terminal.Backend.Api
RUN dotnet publish Terminal.Backend.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Terminal.Backend.Api.dll"]
