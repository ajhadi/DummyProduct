# DummyProduct Commerce

Dummy Product E-Commerce

## Getting Started

### Dependencies

You need to install:
* [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [Visual Studio](https://visualstudio.microsoft.com/downloads/)

### Installing

1. **Clone this project**
   - Clone the repository to your local machine.

2. **Create the Database**
   - Open SQL Server Management Studio (SSMS) or your preferred SQL Server tool.
   - Create a new database with the name `DummyProduct`:
     ```sql
     CREATE DATABASE DummyProduct;
     ```

3. **Open the Project in Visual Studio**
   - Open Visual Studio.
   - Select `Open a project or solution`.
   - Navigate to the folder where you cloned the project, and select the `.sln` file to open the project.

4. **Update the Connection String**
   - Open the `appsettings.json` file in your project.
   - Locate the `ConnectionStrings` section and update the `DummyProductDb` connection string to match your SQL Server connection details. It should look something like this:
     ```json
     "ConnectionStrings": {
       "DummyProductDb": "Server=YOUR_SERVER_NAME;Database=DummyProduct;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
     }
     ```

5. **Update the Database**
   - Open the `Package Manager Console` in Visual Studio by navigating to `Tools` > `NuGet Package Manager` > `Package Manager Console`.
   - Run the following command to apply migrations and update the database schema:
     ```
     Update-Database
     ```

6. **Restore and Build the Project**
   - Restore all necessary dependencies by running:
     ```
     dotnet restore
     ```
   - Build the project by running:
     ```
     dotnet build
     ```
   - Alternatively, you can use the Visual Studio menu: select `Build` and then `Build Solution`.

7. **Run the Program**
   - To run the program, use the following command in the `Package Manager Console`:
     ```
     dotnet run
     ```
   - Or, you can click the `Run` or `Start` button in Visual Studio.

8. **Access the Application**
   - Once the application is running, open your browser and access the application at:
     ```
     https://localhost:7065
     ```

### Author

Ahmad Hadi Jaelani (Aj Hadi)

[@ajhadi](https://github.com/ajhadi)
