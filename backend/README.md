# Mon Chenil - Backend

## Setup

### Prerequisites

- .NET 8

### Installation

1. Clone the repo
2. Install the Entity Framework CLI: `dotnet tool install --global dotnet-ef`
3. Run the migrations: `dotnet ef database update --project MonChenil`

### Running the app

- Run the app: `dotnet run --project MonChenil`
- The app will be available at `http://localhost:5088`
- The Swagger UI will be available at `http://localhost:5088/swagger`

## Development

- Run the tests: `dotnet test`
