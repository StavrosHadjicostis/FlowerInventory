# Flower Inventory
Setup Instructions
-To begin clone the repository to your computer.
-Then open the SQL Server Management Studio and run the Create_FlowerInventoryDB.sql script to create the Database.
-Open the project and run the dotnet run command to run the application
-Open your browser and navigate to https://localhost:5043



Description of Implementation
-Firstly i created the SQL script for the creation of the DB
-Then i Downloaded SQL Server and SSMS and created the DB
-Using scaffold i created the Models and DBContext of the Application
-Proceed to Backend and Frontend development
-Implemented Testing


Challenges Faced
-Needed to intall and get familiar with many new technologies (SQL Server, .Net)
-It was a bit challenging setting up the SQL Server since i haven't done this before
-Repopulating Categories on Validation Failures: When ModelState was invalid, the categories list had to be reloaded in the controller before returning the view to prevent runtime errors.
-Structuring tests for maintainability and easy mocking of database contexts.


Technologies Used
-ASP.NET Core MVC
-SQL Server
-Entity Framework
-Html, css, javascript, bootstrap




<!-- Open a terminal to the project folder and run the following command dotnet ef dbcontext scaffold "Server=localhost\SQLEXPRESS;Database=FlowerInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -->



