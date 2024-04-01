<img src="https://storage.googleapis.com/passage-docs/passage-logo-gradient.svg" alt="Passage logo" style="width:250px;"/>

[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/zskovacs/passage-dotnet/build.yml?branch=main)](https://github.com/zskovacs/passage-dotnet/actions)
[![GitHub](https://img.shields.io/github/license/zskovacs/passage-dotnet?style=flat-square)](https://github.com/zskovacs/passage-dotnet/blob/main/LICENSE)
[![Downloads](https://img.shields.io/nuget/dt/passage.net?style=flat-square)](https://www.nuget.org/packages/Passage.NET/)
[![NuGet current](https://img.shields.io/nuget/v/Passage.NET?label=NuGet)](https://www.nuget.org/packages/Passage.NET)

# Passage.NET

This dotnet SDK allows for verification of server-side authentication for applications using [Passage](https://passage.id)

Install this package using nuget.

```dotnetcli
dotnet add pacakge Passage.NET
```

## Validate JWT
You need to provide Passage with your App ID in order to verify the JWTs.
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID"
}

var passage = new Passage(config);

var subject = await passage.ValidateToken("JWT_TO_VALIDATE");
```

## Retrieve App Info
To retrieve information about an app, you should use the passage.GetApp() function.

```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID"
}

var passage = new Passage(config);

var passageApp = await passage.GetApp();
```
