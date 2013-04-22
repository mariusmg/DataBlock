BookManager
DataBlock Demo Application

This is a small demo application which demonstrates a part of DataBlock capabilities.
In order to run the demo you need to have Visual Studio 2005 or Visual Web Developer 2005 Express Edition

Visual Studio 2005 :
- open the DataBlockDemo.sln file from the subfolder with the language you are interested in (csharp or vb.net)
Make sure that "ui" is the start up projects and "default.aspx" is the default start up file.

Visual Web Developer 2005 Express Edition
- choose File - Open - Website and choose path to the "ui" folder for the language are you interested in (C# or VB.NET).
Make sure that "default.aspx" is the default start up file.


Prepare the database :

1. Sql Server
Please restore the database file (bookmanager.bak) located in \Database subfolder

2. Access
Just see the configuration steps below.

3. MySql
Please run the bookmanager_mysq.sql script. 



Configuration :
Here is how to configure BookManager to run with each of the supported databases:

1. Sql Server :
Please add these 2 lines under the <appSettings> section in the web.config file :
        <add key = "ConnectionString" value = " "/>
	<add key = "ServerType" value ="sqlserver"/>

2. Access
Please modify these 2 lines under the <appSettings> section in the web.config file :
	<add key = "ConnectionString" value = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\bookmanager.mdb;User Id=admin;Password=;"/>
	<add key = "ServerType" value ="access"/>

Make sure you set the Data Source to the actual path for the mdb file.

3. MySql 
Here is how to connect to MySql using the ODBC driver. Make sure you have the latest ODBC driver.
Please modify these 4 lines under the <appSettings> section in the web.config file :
		<add key = "ConnectionString" value = "DRIVER={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=bookmanager;USER=sa;PASSWORD=;"/>
		<add key = "ServerType" value ="mysql"/>
		<add key= "ProviderMySql" value="odbc"/>
		<add key= "ProviderMySqlParameterChar" value="@"/>

Make sure you set the right user name and password.

The C# version of the demo is located in the \C# subfolder. The VB.NET version is located
under the \VB.NET folder.

That's all. If you have any questions please get in touch with us at gmarius@gmail.com
or visit our support forums located at www.voidsoft.ro/forum/
