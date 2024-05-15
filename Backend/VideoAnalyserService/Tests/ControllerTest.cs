using Moq;
using NUnit.Framework;
using TV2.Backend.ClassLibrary.Models.Metadata;
using TV2.Backend.Services.VideoAnalyser.Client.Model;
using TV2.Backend.Services.VideoAnalyser.Controllers;
using TV2.Backend.Services.VideoAnalyser.Interfaces;
using Index = TV2.Backend.Services.VideoAnalyser.Client.Model.Index;

namespace TV2.Backend.Services.VideoAnalyser.Tests;

[TestFixture]
public class ControllerTest
{
    private Mock<IAuthService> _authService;
    private Mock<IAnalyserService> _analyserService;
    private Mock<ILogger<VideoAnalyserController>> _logger;
    private VideoAnalyserController _controller;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _authService = new Mock<IAuthService>();
        _analyserService = new Mock<IAnalyserService>();
        _logger = new Mock<ILogger<VideoAnalyserController>>();

        // Mock the necessary methods of AuthService and AnalyserService
        _authService.Setup(x => x.AuthenticateArmAsync()).ReturnsAsync("arm_token");
        _authService.Setup(x => x.AuthenticateAsync(It.IsAny<string>())).ReturnsAsync("account_token");
        _authService.Setup(x => x.GetAccountAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Account());
        
        _controller = new VideoAnalyserController(_logger.Object, _authService.Object, _analyserService.Object);
    }
    
    [Test]
    public void UploadFootageTest()
    {
        // Arrange
        _analyserService.Setup(x => x.UploadUrlAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Account>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
            .ReturnsAsync("Id");
        
        // Act
        var result = _controller.UploadFootage("footage_url", "footage_name").Result;
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("Id"));
    }
    
    [Test]
    public void GetMetadataTest()
    {
        // Arrange
        _analyserService.Setup(x => x.WaitForIndexAsync(It.IsAny<string>(), It.IsAny<Account>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Mock.Of<Index>(index => index.Videos == new List<Video> { Mock.Of<Video>() }));
        
        // Act
        var result = _controller.GetMetadata("your_footage_id", "your_schema").Result;
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Video>());
    }
    
}