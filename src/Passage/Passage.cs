using System.Net;
using System.Threading.Tasks;

namespace Passage;

public class Passage : IPassage
{
    private readonly string _appId;
    private readonly string _appKey;
    private readonly AuthStrategy _authStrategy;
    
    public Passage(PassageConfig config)
    {
        if (string.IsNullOrEmpty(config?.AppId)) {
            throw new PassageException("A Passage appID is required. Please include {AppID: YOUR_APP_ID}.");
        }
        
        if (string.IsNullOrEmpty(config?.AppKey)) {
            throw new PassageException("A Passage appKey is required. Please include {AppKey: YOUR_APP_KEY}.");
        }

        _authStrategy = config.AuthStrategy;
        _appId = config.AppId;
        _appKey = config.AppKey;
    }
    
    
    public Task<string> ValidateToken(string token)
    {
        
    }
}