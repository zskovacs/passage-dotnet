<img src="https://storage.googleapis.com/passage-docs/passage-logo-gradient.svg" alt="Passage logo" style="width:250px;"/>

[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/zskovacs/passage-dotnet/build.yml?branch=main)](https://github.com/zskovacs/passage-dotnet/actions)
[![GitHub](https://img.shields.io/github/license/zskovacs/passage-dotnet?style=flat-square)](https://github.com/zskovacs/passage-dotnet/blob/main/LICENSE)
[![Downloads](https://img.shields.io/nuget/dt/passage.net?style=flat-square)](https://www.nuget.org/packages/Passage.NET/)
[![NuGet current](https://img.shields.io/nuget/v/Passage.NET?label=NuGet)](https://www.nuget.org/packages/Passage.NET)

# Passage.NET

This dotnet SDK allows for verification of server-side authentication for applications using [Passage](https://passage.id)

## 💾 Installation
Install this package using nuget.

```dotnetcli
dotnet add pacakge Passage.NET
```

## 💉 Dependency injection
If you want to integrate with HttpClientFactory and Microsoft.Extensions.DependencyInjection
```dotnetcli
dotnet add pacakge Passage.Net.Extensions.DependencyInjection
```
Usage:
```csharp
//...
builder.Services.AddPassage(options =>
{
    options.AppId = "YOUR_APP_ID";
    options.ApiKey = "YOUR_API_KEY";
});
//...
var app = builder.Build();
//...
app.MapGet("/getapp", async (IPassage passage) =>
{
    var appinfo = await passage.App.Get();
    return appinfo;
})
```

## 📒 Usage

### Validate JWT
You need to provide Passage with your App ID in order to verify the JWTs.
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID"
}

var passage = new Passage(config);

var subject = await passage.Session.ValidateToken("JWT_TO_VALIDATE");
```

### Logout
You can revoke refresh token from the given user
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID"
}

var passage = new Passage(config);

var subject = await passage.Session.RevokeRefreshToken("USER_ID");
```

### Retrieve App Info
To retrieve information about an app, you should use the passage.GetApp() function.
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID",
    ApiKey = "YOUR_API_KEY"
}

var passage = new Passage(config);

var passageApp = await passage.App.Get();
```

### Retrieve User Information By Identifier
To retrieve information about a user, you should use the passage.GetUserByIdentifier("your@email.cc") function.
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID",
    ApiKey = "YOUR_API_KEY"
}

var passage = new Passage(config);

var passageUser = await passage.User.GetByIdentifier("user@email.cc");
```

### Retrieve User Information By User Id
To retrieve information about a user, you should use the passage.GetUser("USER_ID") function.
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID",
    ApiKey = "YOUR_API_KEY"
}

var passage = new Passage(config);

var passageUser = await passage.User.Get("USER_ID");
```

### Create a User
You can also create a Passage user by providing an email or phone (phone number must be a valid E164 phone number).
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID",
    ApiKey = "YOUR_API_KEY"
}

var passage = new Passage(config);

var createRequest = new CreateUserRequest()
{
    Email = "user@email.cc",
    //note that user_metadata is an optional field and is defined in your Passage App settings (Registration Fields).
    User_metadata = new
    {
        example = 123
    }
};
    
var passageUser = await passage.User.Create(createRequest);
```

### Update a User
You can update a user attributes
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID",
    ApiKey = "YOUR_API_KEY"
}

var passage = new Passage(config);

var updateRequest = new UpdateUserRequest()
{
    Email = "user@email.cc",
    //note that user_metadata is an optional field and is defined in your Passage App settings (Registration Fields).
    User_metadata = new
    {
        example = 1234
    }
};

var passageUser = await passage.User.Update("USER_ID",updateRequest);
```

### Delete a User
To delete a Passage user, you will need to provide the userID, and corresponding app credentials.
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID",
    ApiKey = "YOUR_API_KEY"
}

var passage = new Passage(config);

await passage.User.Delete("USER_ID");
```

### Activate a User
You can activate a user manually
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID",
    ApiKey = "YOUR_API_KEY"
}

var passage = new Passage(config);

var passageUser= await passage.User.Activate("USER_ID");
```

### Deactivate a User
You can deactivate a user manually
```csharp
var config = new PassageConfig()
{
    AppId = "YOUR_APP_ID",
    ApiKey = "YOUR_API_KEY"
}

var passage = new Passage(config);

var passageUser= await passage.User.Deactivate("USER_ID");
```