using Microsoft.Extensions.Logging;
using MockHttpClient;
using Moq;
using NUnit.Framework;
using TV2.Backend.Services.VideoAnalyser.Client;
using TV2.Backend.Services.VideoAnalyser.Client.Model;

namespace TV2.Backend.Services.VideoAnalyser.Tests;

[TestFixture]
public class VideoAnalyserService_AnalyserServiceTest
{
    [Test]
    public async Task UploadUrlAsync_ValidUrl_ReturnsId()
    {
        // Arrange
        var httpClient = new MockHttpClient.MockHttpClient();
        var logger = new Mock<ILogger<AnalyserService>>();
        var analyserService = new AnalyserService(logger.Object, httpClient);
        
        var accountProperties = new AccountProperties(){Id = "Account_Id"};
        var account = new Account() {Properties = accountProperties, Location = "Account_Location"};
        
        const string index = """
                             {
                                         "id": "04fe00b74f"
                                     }
                             """;
        
        var url = $"{Consts.ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos";
        
        httpClient.When(url).Then(res => new HttpResponseMessage()
                .WithHeader("ok", "200")
                .WithStringContent(index)
            );
        
        
        // Act
        var result = await analyserService.UploadUrlAsync("https://example.com","videoName", account, "accessToken");
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("04fe00b74f"));
    }
    
    
    [Test]
    public async Task UploadUrlAsync_InvalidUrl_ThrowsException()
    {
        // Arrange
        var httpClient = new MockHttpClient.MockHttpClient();
        var logger = new Mock<ILogger<AnalyserService>>();
        var analyserService = new AnalyserService(logger.Object, httpClient);
        
        var accountProperties = new AccountProperties(){Id = "Account_Id"};
        var account = new Account() {Properties = accountProperties, Location = "Account_Location"};
        
        var index = @"{
            ""id"": ""04fe00b74f""
        }";
        
        var url = $"{Consts.ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos";
        
        httpClient.When(url).Then(res => new HttpResponseMessage()
                .WithHeader("ok", "200")
                .WithStringContent(index)
            );
        
        
        // Act
        var exception = Assert.CatchAsync<Exception>(async () =>
            await analyserService.UploadUrlAsync("This is an invalid URL", "videoName", account, "accessToken"));
        
        // Assert
        Assert.That(exception, Is.TypeOf<ArgumentException>());
        Assert.That(exception.Message, Is.EqualTo("VideoUrl or LocalVidePath are invalid"));
    }
    
    
    [Test]
    public async Task UploadUrlAsync_InvalidAccount_ThrowsException()
    {
        // Arrange
        var httpClient = new MockHttpClient.MockHttpClient();
        var logger = new Mock<ILogger<AnalyserService>>();
        var analyserService = new AnalyserService(logger.Object, httpClient);
        
        var accountProperties = new AccountProperties(){Id = "Account_Id"};
        var account = new Account() {Properties = accountProperties, Location = "Account_Location"};
        
        var index = @"{
            ""id"": ""04fe00b74f""
        }";
        
        var url = $"{Consts.ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos";
        
        httpClient.When(url).Then(res => new HttpResponseMessage()
                .WithHeader("ok", "200")
                .WithStringContent(index)
            );
        
        
        // Act
        var exception = Assert.CatchAsync<Exception>(async () =>
            await analyserService.UploadUrlAsync("https://example.com", "videoName", null, "accessToken"));
        
        // Assert
        Assert.That(exception, Is.TypeOf<NullReferenceException>());
    }
    
    
    [Test]
    public async Task WaitForIndexAsync_ProcessingStateSuccess_ReturnsIndexObject()
    {
        // Arrange
        var httpClient = new MockHttpClient.MockHttpClient();
        var logger = new Mock<ILogger<AnalyserService>>();
        var analyserService = new AnalyserService(logger.Object, httpClient);
        
        var accountProperties = new AccountProperties(){Id = "Account_Id"};
        var account = new Account() {Properties = accountProperties, Location = "Account_Location"};
        
        var index = @"{
            ""id"": ""04fe00b74f"",
            ""state"": ""Processed""
        }";
        
        var url = $"{Consts.ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos/04fe00b74f/Index";
        
        httpClient.When(url).Then(res => new HttpResponseMessage()
                .WithHeader("ok", "200")
                .WithStringContent(index)
            );
        
        
        // Act
        var result = await analyserService.WaitForIndexAsync("04fe00b74f", account,  "accessToken", "schema");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.State, Is.EqualTo(ProcessingState.Processed.ToString()));
    }
    
    
    [Test]
    public async Task WaitForIndexAsync_ProcessingStateFailed_ThrowsException()
    {
        // Arrange
        var httpClient = new MockHttpClient.MockHttpClient();
        var logger = new Mock<ILogger<AnalyserService>>();
        var analyserService = new AnalyserService(logger.Object, httpClient);
        
        var accountProperties = new AccountProperties(){Id = "Account_Id"};
        var account = new Account() {Properties = accountProperties, Location = "Account_Location"};
        
        var index = @"{
            ""id"": ""04fe00b74f"",
            ""state"": ""Failed""
        }";
        
        var url = $"{Consts.ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos/04fe00b74f/Index";
        
        httpClient.When(url).Then(res => new HttpResponseMessage()
                .WithHeader("ok", "200")
                .WithStringContent(index)
            );
        
        
        // Act
        var exception = Assert.CatchAsync<Exception>(async () =>
            await analyserService.WaitForIndexAsync("04fe00b74f", account,  "accessToken", "schema"));
        
        // Assert
        Assert.That(exception, Is.TypeOf<Exception>());
        Assert.That(exception.Message, Is.EqualTo(index));
    }
}