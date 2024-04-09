using TV2.Backend.Services.VideoAnalyser.Client.Model;

namespace TV2.Backend.Services.VideoAnalyser.Interfaces;

public interface IAuthService
{
    Task<string> AuthenticateAsync();
    Task<Account> GetAccountAsync(string accountName, string armAccessToken);
}