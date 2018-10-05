# Toggler (Feature Flags) Service Sample application for .Net Core 


## Basic requirements

This service must be able to register new toggles that can be used by one or more services/applications  with a hierarchy, as for example:   
- Toggle named ​ `isButtonBlue` ​ with value ​ `true` ​ can be used by all services.   
- Toggle named ​ `isButtonBlue` with value ​ `false` ​ can only be used by the service ABC overriding  the value of the toggle mentioned above.
- Toggle named ​ `isButtonGreen` ​ with value ​ `true` ​ can only be used by the service ABC   
- Toggle named ​ `isButtonRed` ​ with value ​ `true` ​ can be used by all services except the service ABC

When the services/applications request their toggles, they will only provide their identifiers and version.

The company has multiple development teams around the world, and with the different time-zones they  will not be able to be in contact with you, so the service must have a clear documentation for them to  use.   

There will be a team that will maintain this service, and they must be able to easily change the code and  deploy this service with confidence that they will not break any contract with the other services in the  platform.   
  
Only users with the Admin role can create and update a toggle.

This endpoint must be protected and only the services with the correct authorization can access.

This service must be able to broadcast to every service/application that a toggle has changed its value  without having to know who those same services/applications are.  


## Getting started

This project was generated with [Dotnet CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x) SDK version 2.1.403 and [Dotnet Boxed Api Template](https://github.com/Dotnet-Boxed/Templates)

1. Install the latest [.NET Core SDK](https://dot.net).
2. Run `dotnet new --install "Boxed.Templates::*"` to install the project template.
3. Run `dotnet new api --help` to see how to select the feature of the project.
5. Run `dotnet new api --name "ToggleService"` along with any other custom options to create a project from the template.

It uses C# 7.3 language features and SDK-style projects, so you'll need any edition of Visual Studio 2017 (15.8.6 or above) or Visual Studio Code to open and compile the project. The free Community Edition will work.

### Requirements

- [.Net Core 2.1.5](https://www.microsoft.com/net/download/dotnet-core/2.1)
- [Visual Studio 2017 15.8.6 or above or Visual Studio Code latest](https://visualstudio.microsoft.com/)


### Development server

Run `dotnet run` for a dev server. Navigate to `https://localhost:5000/`.

### Build

Run `dotnet build` to build the project.

### Running unit tests

Run `dotnet test` in the Test project directory

### How to contribute?

Please see

[Dotnet Namein Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)

### Used dependency


### Further help and info

- [.Net Core](https://docs.microsoft.com/en-us/dotnet/core/)
- [Asp.Net Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2)


## More Resource

- https://docs.microsoft.com/en-us/azure/devops/learn/devops-at-microsoft/progressive-experimentation-feature-flags
- https://martinfowler.com/articles/feature-toggles.html
- https://docs.microsoft.com/en-us/azure/devops/learn/devops-at-microsoft/progressive-experimentation-feature-flags
- https://www.youtube.com/watch?v=MCudGFoqadk
