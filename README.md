<img src="https://storage.googleapis.com/passage-docs/passage-logo-gradient.svg" alt="Passage logo" style="width:250px;"/>

# passage-dotnet

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
