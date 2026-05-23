# SE Lab Assignment 2 - QT2
# Exercise 4 (ASP.NET Core MVC) & Exercise 5 (xUnit Tests)

## Team Members
- Huỳnh Nhật Huy (523c0012)
- Bùi Quang Huy (523v0002)

## Links
- GitHub Repository: [Insert your GitHub Link Here]
- Video Demo: [Insert Video Link Here if applicable]

## Prerequisites
- .NET SDK (6.0 or newer)
- SQLite

## How to Run the Application
1. Open a terminal and navigate to the WebMVC_Core project directory.
2. The database is pre-configured to use SQLite and will automatically seed 20 Items and 20 Agents upon the first run.
3. If the database file (LabAssignment.db) is missing, generate it by running:
   dotnet ef database update
4. Start the application by running:
   dotnet run
5. Open your web browser and navigate to the localhost URL provided in the terminal (e.g., https://localhost:5001).

## How to Run the Tests
1. Navigate to the LabTests project directory.
2. Run the unit tests using:
   dotnet test