# Stores Service


Forms and reports application spanning across stock, logistics and materials handling .

## Solution summary
This services provides forms and reporting for existing data and code in the LinnApps Oracle database. 

## Component technologies
* The backend service is dotnet core C# using NancyFx web framework.
* The GUI client is built with React/Redux and managed with npm and webpack.
* Persistence is to Oracle database via EF Core.
* Continuous deployment via Docker container to AWS ECS using Travis CI.

## Local running and Testing
### C# service
* Restore nuget packages.  
* Run C# tests as preferred. 
* run or debug the Service.Host project to start the backend.
### Client
* `npm install` to install npm packages.
* `npm start` to run client locally on port 3000.
* `npm test` to run javascript tests.
