namespace Passage.Configuration;

public abstract class Errors
{
    public abstract class Token
    {
        public const string CannotDownloadJwks = "Cannot download JWKS";
        public const string VerificationFailed = "JWT verification failed";
    }
    public abstract class User
    {
        public const string CannotGet = "Cannot get User information";
        public const string IdentifierNotFound = "Could not find user with that identifier.";
        public const string CannotCreate = "Cannot create User";
        public const string CannotUpdate = "Cannot update User";
        public const string CannotDelete = "Cannot delete User";
    }

    public abstract class App
    {
        public const string CannotGet = "Cannot get APP information";
    }
    
    public abstract class Config
    {
        public const string MissingAppId = "A Passage AppId is required. Please include {AppID: YOUR_APP_ID}.";
        public const string MissingApiKey = "A Passage ApiKey is required. Please include {ApiKey: YOUR_API_KEY}.";
    }
}