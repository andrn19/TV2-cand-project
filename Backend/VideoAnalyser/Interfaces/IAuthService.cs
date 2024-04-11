using TV2.Backend.Services.VideoAnalyser.Client.Model;

namespace TV2.Backend.Services.VideoAnalyser.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Get Information about the Account
    /// </summary>
    /// <returns>An ARM access token.</returns>
    Task<string> AuthenticateAsync(string armAccessToken);
    
    
    
    /// <summary>
    /// Get Information about the Account
    /// </summary>
    /// <param name="accountName">The name of the account.</param>
    /// <param name="armAccessToken">An ARM access token.</param>
    /// <returns>An account object</returns>
    Task<Account?> GetAccountAsync(string accountName, string armAccessToken);
}