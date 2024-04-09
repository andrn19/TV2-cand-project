namespace TV2.Backend.Services.VideoAnalyser.Client;

using System.Net.Http.Headers;
using System.Text.Json;
using Auth;
using Model;
using Utils;
using Interfaces;
using static Consts;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService>? _logger;
    private readonly HttpClient _httpClient;
    
    public AuthService(ILogger<AuthService>? logger = null)
    {
        System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;
        _httpClient = HttpClientUtils.CreateHttpClient();
        _logger = logger;
    }

    /// <summary>
    /// Get Information about the Account
    /// </summary>
    /// <returns>An ARM access token.</returns>
    public async Task<string> AuthenticateAsync()
    {
        try
        {
            var armAccessToken = await AccountTokenProvider.GetArmAccessTokenAsync();
            var accountAccessToken = await AccountTokenProvider.GetAccountAccessTokenAsync(armAccessToken);
            return accountAccessToken;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Get Information about the Account
    /// </summary>
    /// <param name="accountName">The name of the account.</param>
    /// <param name="armAccessToken">An ARM access token.</param>
    /// <returns></returns>
    public async Task<Account> GetAccountAsync(string accountName, string armAccessToken)
    {
        try
        {
            // Set request uri
            var requestUri = $"{AzureResourceManager}/subscriptions/{SubscriptionId}/resourcegroups/{ResourceGroup}/providers/Microsoft.VideoIndexer/accounts/{accountName}?api-version={ApiVersion}";
            var client = HttpClientUtils.CreateHttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", armAccessToken);

            var result = await client.GetAsync(requestUri);

            result.VerifyStatus(System.Net.HttpStatusCode.OK);
            var jsonResponseBody = await result.Content.ReadAsStringAsync();
            var account = JsonSerializer.Deserialize<Account>(jsonResponseBody);
            VerifyValidAccount(account, accountName);
            //Console.WriteLine($"[Account Details] Id:{account.Properties.Id}, Location: {account.Location}");
            return account;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
    
    private static void VerifyValidAccount(Account account,string accountName)
    {
        if (string.IsNullOrWhiteSpace(account.Location) || account.Properties == null || string.IsNullOrWhiteSpace(account.Properties.Id))
        {
            Console.WriteLine($"{nameof(accountName)} {accountName} not found. Check {nameof(SubscriptionId)}, {nameof(ResourceGroup)}, {nameof(accountName)} ar valid.");
            throw new Exception($"Account {accountName} not found.");
        }
    }
}