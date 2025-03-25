# PetHotel API
### This is a .NET 8.0 web API for managing pet hotels, following the principles of Clean Architecture.

## Project Structure
- src/PetHotel: The main API project. This is the entry point of the application.
- src/PetHotel.Application: Contains the application logic. This layer is responsible for the application's behavior and policies.
- src/PetHotel.Domain: Contains enterprise logic and types. This is the core layer of the application.
- src/PetHotel.Infrastructure: Contains infrastructure-related code such as database and file system interactions. This layer supports the higher layers.

## Packages and Libraries
This project uses several NuGet packages and libraries to achieve its functionality:

- Serilog: This library is used for logging. It provides a flexible and easy-to-use logging API.

- MediatR: This library is used to implement the Command Query Responsibility Segregation (CQRS) pattern. In this solution, commands (which change the state of the system) and queries (which read the state) are separated for clarity and ease of development.

- Entity Framework: This is an open-source ORM framework for .NET. It enables developers to work with data using objects of domain-specific classes without focusing on the underlying database tables and columns where this data is stored.

- Microsoft Identity Package: This package is used for handling user identity in the web API. It provides features such as authentication, authorization, identity, and user access control.

Please refer to the official documentation of each package for more details and usage examples.

## 🌟 Live Demo
You can test the API at:
</br>
`<link>`: https://pethotel-api-dev-a7echzexcwe5hwea.polandcentral-01.azurewebsites.net

API documentation (Swagger) is available at:
</br>
`<link>`: https://pethotel-api-dev-a7echzexcwe5hwea.polandcentral-01.azurewebsites.net/swagger

### Quick Test Guide
1. Visit the Swagger documentation at the demo site
2. Register a new account using the `/api/Account/register` endpoint
3. Login using `/api/Account/login` to receive JWT token
4. Authorize in Swagger using the received token (click "Authorize" button)
5. Start testing other endpoints!

Note: The demo site is hosted on Azure Web App and represents the development environment.


## 🌟 Features

### Reservation Management
- Create, view, update and delete pet stay reservations
- Add additional services to reservations
- Manage reservation status and dates
- View reservation history

### Pet Management
- Register new pets
- Update pet information
- View pet profiles
- Track pet stay history

### Owner Management
- Create and manage owner accounts
- View owner's pets
- Track owner's reservation history

### Hotel Services
- Manage available services
- Set service prices
- Add service descriptions
- Track service usage

### Security
- JWT-based authentication
- Role-based authorization
- Secure endpoints
- Owner/Hotel staff permission system

## 🛠️ Technologies

- .NET 8
- ASP.NET Core
- Entity Framework Core
- SQL Server
- AutoMapper
- FluentValidation
- xUnit
- Moq

## 🏗️ Architecture

- Clean Architecture
- CQRS Pattern
- Repository Pattern

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Installation
1. Clone the repository
```bash
git clone https://github.com/yourusername/PetHotel_API.git
```

2. Navigate to project directory
```bash
cd PetHotel_API
```

3. Restore dependencies
```bash
dotnet restore
```

4. Update database
```bash
dotnet ef database update
```

5. Run the application
```bash
dotnet run
```

## 🔑 API Authentication

The API uses JWT Bearer authentication. To access protected endpoints:
1. Register a new user account
2. Login to receive JWT token
3. Include the token in request headers

## 📝 API Documentation

API documentation is available at `/swagger` endpoint when running the application.

## ✅ Testing

To run the tests:
```bash
dotnet test
```

## 🔄 CI/CD

The project uses GitHub Actions for CI/CD pipeline:
- Automated builds
- Unit tests execution
- Deployment to Azure Web App

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

