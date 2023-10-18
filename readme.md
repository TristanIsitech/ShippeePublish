```bash	
# créer projet 
$ dotnet new webapi -o HeroAPI

# package à ajouter via https://www.nuget.org/packages
$ Aspose.Cells
$ AutoMapper
$ AutoMapper.Extensions.Microsoft.DependencyInjection
$ dotnet add package Microsoft.EntityFrameworkCore --version 7.0.1  
$ dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.1  
$ dotnet add package Pomelo.EntityFrameworkCore.MySql --version 7.0.0-alpha.1
$ RestSharp

# migration et update de la bdd
$ dotnet-ef migrations add 
$ dotnet-ef database update

# lancer l'api
$ dotnet run --launch-profile https
```
