# EcommerceWebsite

## Overview

EcommerceWebsite is an ASP.NET Core MVC application that allows users to browse products, add items to their cart, and manage their purchases. This guide provides instructions on how to set up and run the project locally.

## Prerequisites

- **.NET 6 SDK or later**: [Download here](https://dotnet.microsoft.com/download)
- **SQL Server**: Ensure you have SQL Server installed. You can use SQL Server Express if preferred.
- **Visual Studio 2022** or any other code editor of your choice.

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/EcommerceWebsite.git
cd EcommerceWebsite/EcommerceWebsite
```

### 2. Install Dependencies

Restore the required NuGet packages by running:

```bash
dotnet restore
```

### 3. Configure the Database

Update the `appsettings.json` file with your database connection string:
For SQLITE it's already set

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=EcommerceWebsiteDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Apply Migrations

Ensure Entity Framework Core tools are installed:

```bash
dotnet tool install --global dotnet-ef
```

Apply migrations and create the database:

```bash
dotnet ef database update
```

### 5. Run the Application

Start the application using the following command:

```bash
dotnet run
```

Alternatively, open the project in Visual Studio and press **F5** to run the application.

### 6. Access the Application

Navigate to `https://localhost:5001` or `http://localhost:5000` in your web browser to access the EcommerceWebsite.

## Project Structure

- **Controllers**: Handles HTTP requests and responses.
- **Models**: Defines the data structures.
- **Views**: Contains Razor view files for the UI.
- **wwwroot**: Hosts static files like CSS, JavaScript, and images.
- **Data**: Contains database context and migration files.
- **Services**: (If applicable) Contains business logic and service classes.

## Additional Commands

### Adding a New Migration

To create a new migration:

```bash
dotnet ef migrations add YourMigrationName
```

### Removing the Last Migration

To remove the most recent migration:

```bash
dotnet ef migrations remove
```

### Listing Migrations

To list all migrations:

```bash
dotnet ef migrations list
```

## Troubleshooting

- **EF Core Tools Not Found**: Install them globally using `dotnet tool install --global dotnet-ef`.
- **Database Connection Issues**: Verify that SQL Server is running and the connection string is correct.
- **Missing Packages**: Run `dotnet restore` to restore all dependencies.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any enhancements or bug fixes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
