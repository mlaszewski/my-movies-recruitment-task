# MyMovies Recruitment task

Before you run the project you need to install:
- Visual Studio 2022
- .NET Core 7.0.14
- Microsoft.EntityFrameworkCore 7.0.14
- Microsoft.EntityFrameworkCore.Design 7.0.14
- Microsoft.EntityFrameworkCore.SqlServer 7.0.14
- SQL Server 2022 (Express)
- Nodejs
- NPM

## Installation

1. Clone the repository to destination folder
2. run API
- open the "MyMoviesAPI" project/solution with Visual Studio 2022
- in package manager go to project folder
- run the command to set up a database
```bash
dotnet ef database update
```
- run and build the project with Visual Studio
- you can test the api on https://localhost:7053/swagger/index.html
3. run Vue Application
- open "my-movies-client" folder with cmd
- run command to install packages
```bash
npm install
```
- run application in developement
```bash
npm run serve
```
