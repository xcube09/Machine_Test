# How To Setup

In Order to test Application follow the steps below:

1.) Pull the source from github into Visual Studio 2019 <br/>
2.) Build and restore packages <br/>
3.) Setup sql server database and run the included CreateMasterTable.sql script to generate tables and data <br/>
4.) Publish the AbcCompany.SqlDatabase project to the same database as in no 3 above <br/>
5.) modify the connectionstring in AbcCompany.Web - appsettings.development.json file to connect to the database <br/>
6.) Run the application. <br/>


# Tech Stack Used
1.) asp.net core 3.1 - Razor pages & Web api <br/>
2.) Knockoutjs - a light weight javascript framework for model binding <br/>
3.) MSSQL Server <br/>

4.) Dapper - light weight and efficient ORM that uses raw SQL <br/>
5.) Twitter Bootstrap 4 <br/>

# Design Pattern
A combination of Clean architecture, CQRS, Mediator pattern and SOLID principles 
