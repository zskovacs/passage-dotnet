#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Passage.Configuration;

public abstract class Errors
{
    public abstract class Token
    {
        public const string CannotDownloadJwks = "Unable to download JWKS due to a network error or incorrect endpoint.";
        public const string VerificationFailed = "JWT verification has failed because of incorrect signature or payload data.";
    }
    public abstract class User
    {
        public const string CannotGet = "Unable to retrieve User information";
        public const string IdentifierNotFound = "The user with the specified identifier could not be found in the system.";
        public const string CannotCreate = "User creation has failed due to validation errors or a system error.";
        public const string CannotUpdate = "User data updating process has failed due to validation errors or a system error.";
        public const string CannotDelete = "The system couldn't proceed with User deletion due to a system error.";
    }

    public abstract class App
    {
        public const string CannotGet = "Getting App information is unsuccessful due to an issue with the Application service.";
    }
    
    public abstract class Config
    {
        public const string MissingAppId = "Your configuration is missing an Application ID (AppID). Include it in the following format: {AppID: YOUR_APP_ID}.";
        public const string MissingApiKey = "Your configuration is missing an ApiKey. Please include it in the following format: {ApiKey: YOUR_API_KEY}.";
    }
}