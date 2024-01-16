# ToDoList Backend Web API 🚀

This repository contains the evolving backend code for a ToDoList application developed in C# using the .NET framework. The project serves as the dynamic backbone for managing and organizing tasks in a ToDoList, offering a robust and scalable solution. Please note that the API is currently in active development, and the code is subject to frequent changes and continuous improvement.

<p align="left">
   <img alt="License" src="https://img.shields.io/github/license/Ileriayo/markdown-badges?style=for-the-badge">
   <img alt="csharp" src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white">
   <img alt="dotnet" src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white">
   <img alt="visualstudio" src="https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white">
   <img alt="sql" src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white">
</p>

## Features ✨

- **RESTful API:** Utilizing HTTP methods to create, read, update, and delete tasks.
- **Data Storage:** Integration with a database to persistently store ToDoList items.
- **CQRS Pattern:** Leveraging the Command Query Responsibility Segregation pattern for a more scalable and maintainable architecture.
- **MediatR:** Implementation of MediatR for handling command and query pipelines, promoting a clean and modular codebase.
- **FluentValidation:** Ensuring robust input validation to maintain data integrity.
- **Migrations:** Employing migrations for seamless control and versioning of database schema changes.

## Technologies Used 🛠️

- **C#:** Primary programming language for the backend logic.
- **.NET Core:** Framework providing cross-platform compatibility and high performance.
- **Entity Framework Core:** ORM for database interaction and management.
- **ASP.NET Core Web API:** Building the RESTful API endpoints.
- **Dapper:** Utilizing [Dapper](https://github.com/DapperLib/Dapper) for more heavyweight queries, providing efficient and high-performance data access through raw SQL queries.

## Getting Started 🚦

1. Clone the repository: `git clone https://github.com/fabriciopgl/ToDoList.git`
2. Configure your database connection in the `appsettings.json` file.
3. Apply database migrations (make sure to check the [Entity Framework Migrations documentation](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) for more details) by running:
   
   ```bash
   dotnet ef database update

4. Run the application using Visual Studio or the command line.

Feel free to explore, contribute, and adapt this ToDoList backend for your specific needs. If you have any questions or suggestions, please open an issue or submit a pull request. Happy coding! 🚧
