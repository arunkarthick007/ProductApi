# ProductApi for EG Group

This is a simple RESTful API for managing products, built with ASP.NET Core and Entity Framework Core. The application includes JWT authentication, logging, and a console application to interact with the API.

## Note
Due to time constraints, the project contains hard-coded values for the username, password, and JWT keys. It is recommended to replace these with secure configurations or environment variables or Azure Key vault in a production environment.

## Features

- **Product Management**: List, add, and delete products.
- **JWT Authentication**: Secured endpoints with token-based authentication.
- **Logging**: Uses Serilog for logging application events.
- **Console Application**: A client application to interact with the API.
- **Unit Testing**: Automated tests for the API.

## Technologies Used

- **ASP.NET Core**: For building the web API.
- **Entity Framework Core**: For data access and database management.
- **SQLite**: For database storage.
- **Serilog**: For logging.
- **JWT**: For authentication.
- **XUnit and Moq**: For unit testing.

### Prerequisites

- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- Visual Studio or any IDE

### Setup
1. Clone the repository:
       git clone <repository-url>
       cd ProductApi
2. Open the solution file in Visual Studio 
3. Restore NuGet packages for the entire solution:
    - dotnet restore
4. Run database migrations from the API project:
    -  Before running the application for the first time, ensure to create the initial database migration by executing:
         - dotnet ef migrations add <"Migration name here"> --project ProductApi
    -  dotnet ef database update --project ProductApi
6. Run the API project:
    -  dotnet run --project ProductApi (Please wait for the server setup)
7. Run the Console Application on another terminal
    -  dotnet run --project ProductApiClient
8. Additionally, Swagger will be opened up in the browser to test the APIs interactively.
   
JWT Authentication
Ensure to provide valid credentials when logging into the console application to obtain a JWT token for API requests.
Username : testuser
Password : password
      
###Testing
To run tests for the API project, use:
  - dotnet test --project ProductApiTests

Additional Information:
  Make sure you have the .NET SDK installed on your machine. You can download it from https://dotnet.microsoft.com/en-us/download
