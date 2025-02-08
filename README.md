# FullStackCodingChallenge

This project was deployed using Docker Compose to set up an SQL Server engine and a .NET Core 8 Web API project. The API exposes services organized into layered architecture and is hosted on Azure for cloud-based deployment.

SQL Server Setup with Docker Compose: The database engine was deployed using Docker Compose. This allowed for an isolated, easily configurable SQL Server environment for managing the backend data.

.NET Core 8 Web API: A .NET Core 8 Web API project was created, where business logic and data access are clearly separated into different layers, making the application easier to maintain and scale.

Layered Architecture: The project follows a layered architecture, including:

Presentation Layer: Exposes the API endpoints.
Service Layer: Contains business logic.
Data Access Layer: Handles communication with the database.
ADO.NET for Data Access: The ADO.NET framework is used for accessing the SQL Server database. It provides a low-level approach for database operations, ensuring efficient connection management and query execution.

Stored Procedures: The application also leverages stored procedures for specific database operations, encapsulating SQL logic and improving performance by reducing client-server round trips.

Repository Design Pattern: The Repository Pattern is implemented to abstract data access. This pattern provides a clean separation between the business logic and data access code, making the application more modular and easier to test.

Azure Deployment: The entire solution, including the SQL Server database and .NET Core Web API, is deployed on Azure. Azure provides scalability, high availability, and easy management for hosting both the application and database, ensuring optimal performance and security

![desi](https://github.com/user-attachments/assets/14ce024a-ccba-4375-9c0d-12700f76cc2d)



![clip](https://github.com/user-attachments/assets/900d7c82-e33f-4539-8d85-44239ea394a1)
