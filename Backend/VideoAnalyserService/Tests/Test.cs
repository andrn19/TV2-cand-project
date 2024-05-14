using Moq;
using NUnit.Framework;
using TV2.Backend.ClassLibrary.Models.Metadata;
using TV2.Backend.Services.VideoAnalyser.Client.Model;
using TV2.Backend.Services.VideoAnalyser.Controllers;
using TV2.Backend.Services.VideoAnalyser.Interfaces;
using Index = TV2.Backend.Services.VideoAnalyser.Client.Model.Index;

namespace TV2.Backend.Services.VideoAnalyser.Tests;

[TestFixture]
public class Test
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        
    }

    [Test]
    public void AddExcludedAIsTest()
    {
        
    }
    
    [Test]
    public void UploadFootageTest()
    {
        // Arrange
        var authService = new Mock<IAuthService>();
        var analyserService = new Mock<IAnalyserService>();
        var logger = new Mock<ILogger<VideoAnalyserController>>();

        // Mock the necessary methods of AuthService and AnalyserService
        authService.Setup(x => x.AuthenticateArmAsync()).ReturnsAsync("arm_token");
        authService.Setup(x => x.AuthenticateAsync(It.IsAny<string>())).ReturnsAsync("account_token");
        authService.Setup(x => x.GetAccountAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Account());
        
        analyserService.Setup(x => x.UploadUrlAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Account>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
            .ReturnsAsync("Id");
        
        var controller = new VideoAnalyserController(logger.Object, authService.Object, analyserService.Object);
        
        // Act
        var result = controller.UploadFootage("footage_url", "footage_name").Result;
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("Id"));
    }
    
    [Test]
    public void GetMetadataTest()
    {
        // Arrange
        var authService = new Mock<IAuthService>();
        var analyserService = new Mock<IAnalyserService>();
        var logger = new Mock<ILogger<VideoAnalyserController>>();

        // Mock the necessary methods of AuthService and AnalyserService
        authService.Setup(x => x.AuthenticateArmAsync()).ReturnsAsync("arm_token");
        authService.Setup(x => x.AuthenticateAsync(It.IsAny<string>())).ReturnsAsync("account_token");
        authService.Setup(x => x.GetAccountAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Account());
        analyserService.Setup(x => x.WaitForIndexAsync(It.IsAny<string>(), It.IsAny<Account>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Mock.Of<Index>(index => index.Videos == new List<Video> { Mock.Of<Video>() }));

        var controller = new VideoAnalyserController(logger.Object, authService.Object, analyserService.Object);

        // Act
        var result = controller.GetMetadata("your_footage_id", "your_schema").Result;
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Video>());
    }
    
}