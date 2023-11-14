# bankingCardApplication
This project is made of 1 solutions and 2 projects, the project BankingApp.WebApp is the MVC frontend Web Application adn the BankingApp.Restapi is where the backend and business logic is located.

## Create the Data Base with Migrations ⚒️

In the BankingApp.Restapi there is a folder named 'Migrations' here is where the scripts that create the Data Base are located, to execute that script, you should execute this command in the Nuget Package Manger Console:
``` cli
Update-Database
```
Once executed, the Data Base will be created in your computer.

## Connection to Data Base 📜

Then connectiong to the data base you will need to add a 'ConnectionStrings' with the "Manage User Secrets" property of VS community,
here is an exxample of the string:
``` json
ConnectionStrings:AccountDbConnectionString": "Server=ServerName;Database=AccountDb;Trusted_Connection=True;TrustServerCertificate=true"
```
Replace the 'ServerName' with the name of your server. Then in the Program.cs of the BankingApp.Restapi add the next line:
``` cs
builder.Services.AddDbContext<AccountDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AccountDbConnectionString")));
```
With that the backend should be connected to the Data Base.



