using Microsoft.Extensions.Logging;
using MockHttpClient;
using Moq;
using NUnit.Framework;
using TV2.Backend.Services.VideoAnalyser.Client;

namespace TV2.Backend.Services.VideoAnalyser.Tests;

[TestFixture]
public class VideoAnalyserService_AuthServiceTest
{
    private MockHttpClient.MockHttpClient _httpClient;
    private Mock<ILogger<AuthService>> _logger;
    private AuthService _authService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _logger = new Mock<ILogger<AuthService>>();
    }

    [SetUp]
    public void SetUp()
    {
        _httpClient = new MockHttpClient.MockHttpClient();
        _authService = new AuthService(_logger.Object, _httpClient);
    }
    
    [TearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
    }
    
    [Test]
    public async Task GetAccountAsync_ValidAccountName_ReturnsAccount()
    {
        // Arrange
        const string account = """
                               {
                                           "properties": {
                                               "accountId": "Account_Id"
                                           },
                                           "location": "Account_Location"
                                       }
                               """;
        
        var accountName = "valid_account_name";
        
        var url = $"{Consts.AzureResourceManager}/subscriptions/{Consts.SubscriptionId}/resourcegroups/{Consts.ResourceGroup}/providers/Microsoft.VideoIndexer/accounts/{accountName}";
        
        _httpClient.When(url).Then(res => new HttpResponseMessage()
            .WithHeader("ok", "200")
            .WithStringContent(account)
        );
        
        
        // Act
        var result = await _authService.GetAccountAsync(accountName, "accessToken");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Properties.Id, Is.EqualTo("Account_Id"));
        Assert.That(result.Location, Is.EqualTo("Account_Location"));
    }
    
    [Test]
    public void GetAccountAsync_InvalidAccountName_ThrowsException()
    {
        // Arrange
        const string account = """
                               {
                                           "properties": {
                                               "accountId": ""
                                           },
                                           "location": ""
                                       }
                               """;
        
        var accountName = "invalid_account_name";
        
        var url = $"{Consts.AzureResourceManager}/subscriptions/{Consts.SubscriptionId}/resourcegroups/{Consts.ResourceGroup}/providers/Microsoft.VideoIndexer/accounts/{accountName}";
        
        _httpClient.When(url).Then(res => new HttpResponseMessage()
            .WithHeader("ok", "200")
            .WithStringContent(account)
        );
        
        
        // Act
        var exception = Assert.CatchAsync<Exception>(async () =>
            await _authService.GetAccountAsync(accountName, "accessToken"));
        
        // Assert
        Assert.That(exception, Is.TypeOf<Exception>());
        Assert.That(exception.Message, Is.EqualTo($"{nameof(accountName)} {accountName} not found. Check {nameof(Consts.SubscriptionId)}, {nameof(Consts.ResourceGroup)}, {nameof(accountName)} are valid."));
    }
}