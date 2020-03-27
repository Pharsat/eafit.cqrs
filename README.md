# Maestro REST API

##   Secrets Configuration

This configuration is used to deploy the project into the local environment. The project has been updated to manage the user secrets. 
This action generates a JSON file into this path "C:\Users\[User]\AppData\Roaming\Microsoft\UserSecrets\[SecretsId]" where [user] is the user pc user and [SecretsId] 
is the id provided by the visual studio. 

In this JSON file we have to create the variables that we have to use into the application. 

More information: [Safe storage of app secrets in development in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows)
|Secret Name|Description|Example Value|
|--|--|--|--|
|connectionString|The connection string to the Maestro Database|[pcName]\\SQLEXPRESS\\EYPCSDB