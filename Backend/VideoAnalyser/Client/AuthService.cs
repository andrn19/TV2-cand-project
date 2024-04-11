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
    private readonly ILogger<AuthService> _logger;
    private readonly HttpClient _httpClient;
    //private string _armAccessToken;
    
    public AuthService(ILogger<AuthService>? logger = null)
    {
        System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;
        _httpClient = HttpClientUtils.CreateHttpClient();
        
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = factory.CreateLogger<AuthService>();;
    }

    
    public async Task<string> AuthenticateArmAsync()
    {
        try
        {
            var armAccessToken = await AccountTokenProvider.GetArmAccessTokenAsync();
            return armAccessToken;
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception while authenticating ARM:\n{Exception}", ex.Message);
            throw;
        }
    }
    
    
    public async Task<string> AuthenticateAsync(string armAccessToken)
    {
        try
        {
            var accountAccessToken = await AccountTokenProvider.GetAccountAccessTokenAsync(armAccessToken);
            return accountAccessToken;
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception when authenticating access:\n{Exception}", ex.Message);
            throw;
        }
    }

    
    public async Task<Account?> GetAccountAsync(string accountName, string armAccessToken)
    {
        try
        {
            // Set request uri
            var requestUri = $"{AzureResourceManager}/subscriptions/{SubscriptionId}/resourcegroups/{ResourceGroup}/providers/Microsoft.VideoIndexer/accounts/{accountName}?api-version={ApiVersion}";
            //var client = HttpClientUtils.CreateHttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _armAccessToken);
            //var result = await client.GetAsync(requestUri);
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", armAccessToken);
            var result = await _httpClient.GetAsync(requestUri);

            result.VerifyStatus(System.Net.HttpStatusCode.OK);
            var jsonResponseBody = await result.Content.ReadAsStringAsync();
            var account = JsonSerializer.Deserialize<Account>(jsonResponseBody);
            if (account != null)
            {
                VerifyValidAccount(account, accountName);
            }
            return account;
            //Console.WriteLine($"[Account Details] Id:{account.Properties.Id}, Location: {account.Location}");
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception while getting Account:\n{Exception}", ex.Message);
            throw;
        }
        
    }
    
    private static void VerifyValidAccount(Account account, string accountName)
    {
        if (string.IsNullOrWhiteSpace(account.Location) || account.Properties == null || string.IsNullOrWhiteSpace(account.Properties.Id))
        {
            var ex = $"{nameof(accountName)} {accountName} not found. Check {nameof(SubscriptionId)}, {nameof(ResourceGroup)}, {nameof(accountName)} ar valid.";
            throw new Exception(ex);
        }
    }
}