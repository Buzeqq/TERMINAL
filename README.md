<img width="1669" src="https://user-images.githubusercontent.com/46223928/229259166-1ce9f748-09c8-43f7-869c-b083d848f624.svg" alt="Terminal logo"/>

# Welcome

Welcome to the TERMINAL project! Terminal stands for mul**T**ifunctional databas**E** fo**R** infor**M**ation gather**I**ng i**N** **A** scientific research **L**aboratory. This system is build for the purpose of managing the data of a scientific research laboratory, and it is designed to be used by the laboratory's staff, researchers, and students. The system is built using the ASP .NET Core framework, and Angular 18.

## Table of Contents

1. [Project Architecture](#project-architecture)
2. [Getting Started](#getting-started)
3. [Building and running](#building-and-running)
4. [Testing](#testing)
5. [License](#license)

[//]: # (5. [Contributing]&#40;#contributing&#41;)

## Project Architecture

## Getting Started

### Prerequisites
To develop the application you must first install the following tools:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js v20.16](https://nodejs.org/) (which includes npm)
- [Angular CLI v18](https://angular.dev/tools/cli)
- [Docker](https://docs.docker.com/get-docker/) (for running database locally)

## Building and running

1. Clone the repository
2. Start the database container with docker
```bash
docker run -p 5432:5432 -v /path/to/database/volume/:/var/lib/postgresql/data --env POSTGRES_PASSWORD=root --env POSTGRES_DB=terminal --env POSTGRES_USER=root --name terminal.database --pull missing postgres 
```

3. Navigate to the `Backend` directory and apply the database migrations
```bash
./update-database.sh UserDbContext
./update-database.sh TerminalDbContext
```

4. To run backend, swagger ui is available at http://localhost:5006/swagger/index.html
```bash
dotnet run
```

5. Navigate to the `Frontend` directory and install the dependencies
```bash
npm ci
```

6. To run frontend, then head to https://localhost:4200
```bash
npm run start
```
If you are using JetBrains IDE's there are run configurations available for the backend, frontend and docker.

## Testing
1. Make sure you are able to build the project
2. Navigate to the `Backend` directory and run the tests
```bash
dotnet test
```
3. Navigate to the `Frontend` directory and run the tests
```bash
npm run test
```

[//]: # (## Contributing)

[//]: # ()
[//]: # (Contributions are welcome! Please read our [contributing guidelines]&#40;CONTRIBUTING.md&#41; for more details.)

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
