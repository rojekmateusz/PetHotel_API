# PetHotel API
### This is a .NET 8.0 web API for managing pet hotels, following the principles of Clean Architecture.

## Project Structure
- PetHotel.API: The main API project. This is the entry point of the application.
- PetHotel.Application: Contains the application logic. This layer is responsible for the application's behavior and policies.
- PetHotel.Domain: Contains enterprise logic and types. This is the core layer of the application.
- PetHotel.Infrastructure: Contains infrastructure-related code such as database and file system interactions. This layer supports the higher layers.

## Packages and Libraries
This project uses several NuGet packages and libraries to achieve its functionality:

- Serilog: This library is used for logging. It provides a flexible and easy-to-use logging API.

- MediatR: This library is used to implement the Command Query Responsibility Segregation (CQRS) pattern. In this solution, commands (which change the state of the system) and queries (which read the state) are separated for clarity and ease of development.

- Entity Framework: This is an open-source ORM framework for .NET. It enables developers to work with data using objects of domain-specific classes without focusing on the underlying database tables and columns where this data is stored.

- Microsoft Identity Package: This package is used for handling user identity in the web API. It provides features such as authentication, authorization, identity, and user access control.

Please refer to the official documentation of each package for more details and usage examples.

## Getting Started
### Prerequisites
.NET 8.0
Visual Studio 2022 or later
### Building
To build the project, open the PetHotel_API.sln file in Visual Studio and build the solution.

## Running
To run the project, set PetHotelAPI.API as the startup project in Visual Studio and start the application.

