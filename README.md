# Machine_Test

In Order to test Application follow the steps below:

1.) Pull the source from github into Visual Studio 2019
2.) Build and restore packages
3.) Setup sql server database and run the included CreateMasterTable.sql script to generate tables and data
4.) Publish the AbcCompany.SqlDatabase project to the same database as in no 3 above
5.) modify the connectionstring in AbcCompany.Web - appsettings.development.json file to connect to the database
6.) Run the application.
