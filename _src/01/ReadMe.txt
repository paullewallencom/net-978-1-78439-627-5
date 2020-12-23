In the code zip file you will find the following things:

Chapter1.dacpac file.  This is a database backup.  You can restore it using SQL Server 2014 or later.  
You can use Developer edition or Express.  Just right-click on Database node in SSMS, SQL Server Management Studio 
and select Import Data-Tier application menu.  Just follow wizard steps after that.

You will also find Chapter1 solution.  It contains sample code that shows how to query a database using SQL and ADO.NET.  
You will notice how fragile and more verbose this code is than Entity Framework we will see in chapter2.  
If you are not running SQL Server default instance, you will need to change connection string in app.config file in 
both C# and VB.NET version of this project.
