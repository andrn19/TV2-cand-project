using Microsoft.Extensions.Logging;
using Moq;
using MySql.Data.MySqlClient;
using ThrowawayDb.MySql;
using TV2.Backend.ClassLibrary.Models;
using TV2.Backend.ClassLibrary.Models.Metadata;
using TV2.Backend.Services.DatabaseRegistry.Controllers;
using TV2.Backend.Services.DatabaseRegistry.DataProviders;
using TV2.Backend.Services.DatabaseRegistry.Interfaces;

namespace TV2.Backend.Services.DatabaseRegistry.Tests;

[TestFixture]
public class RegistryService_ControllerTest
{
    private ThrowawayDatabase database;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        database = ThrowawayDatabase.Create(
            "root",
            "root",
            "localhost"
        );

        using var con = new MySqlConnection(database.ConnectionString);
        con.Open();
        var sql = @"CREATE TABLE Host (
                      ID CHAR(36) PRIMARY KEY,
                      Name VARCHAR(500) NOT NULL
                    );";
        
        using var cmd = new MySqlCommand(sql, con);
        cmd.ExecuteNonQuery();
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        database.Dispose();
    }
    
    [SetUp]
    public void Setup()
    {
        database.CreateSnapshot();
    }

    [TearDown]
    public void Cleanup()
    {
        database.RestoreSnapshot();
    }
    
    
    // ===== Get ===== //
    [Test]
    public void GetTest()
    {
        var logger = new Mock<ILogger<DataController>>();
        var consumerRegistry = new ConsumerRegistry();
        var messageService = new Mock<IMessageService>();
        consumerRegistry.setConnectionString(database.ConnectionString);
        var service = new DataController(logger.Object, consumerRegistry, messageService.Object);
        // Arrange
        
        // Act
        var result = service.Get();
        
        // Assert
        Assert.That(result, Is.True);
    }
    
    // ===== CreateEndpoint ===== //
    [Test]
    public void CreateEndpoint_EndpointCreatedSuccessfully()
    {
        // Arrange
        var logger = new Mock<ILogger<DataController>>();
        var consumerRegistry = new ConsumerRegistry();
        var messageService = new Mock<IMessageService>();
        consumerRegistry.setConnectionString(database.ConnectionString);
        var service = new DataController(logger.Object, consumerRegistry, messageService.Object);
        
        // Act
        var noEndpoints = service.ListEndpoints();
        var result = service.CreateEndpoint("Test_Endpoint");
        var testEndpointsCreated = service.ListEndpoints();
        
        // Assert
        Assert.That(result, Is.True);
        Assert.That(noEndpoints, Is.Empty);
        Assert.That(testEndpointsCreated, Is.Not.Empty);
        Assert.That(testEndpointsCreated.First().Name, Is.EqualTo("Test_Endpoint"));
    }
    
    
    
    
    // ===== UpdateEndpoint ===== //
    [Test]
    public void UpdateEndpoint_ValidEndpoint_EndpointUpdatedSuccessfully()
    {
        // Arrange
        var logger = new Mock<ILogger<DataController>>();
        var consumerRegistry = new ConsumerRegistry();
        consumerRegistry.setConnectionString(database.ConnectionString);
        var messageService = new Mock<IMessageService>();
        var service = new DataController(logger.Object, consumerRegistry, messageService.Object);
        
        // Act
        service.CreateEndpoint("Test_Endpoint");
        var testEndpoint = service.ListEndpoints().First();
        var result = service.UpdateEndpoint(new MetadataHost(testEndpoint.Id, "Test_Endpoint_Updated"));
        var testEndpointUpdated = service.ListEndpoints().First();
        
        // Assert
        Assert.That(result, Is.True);
        Assert.That(testEndpoint.Name, Is.EqualTo("Test_Endpoint"));
        Assert.That(testEndpointUpdated.Name, Is.EqualTo("Test_Endpoint_Updated"));
    }
    
    [Test]
    public void UpdateEndpoint_InvalidEndpoint_EndpointNotUpdated()
    {
        // Arrange
        var logger = new Mock<ILogger<DataController>>();
        var consumerRegistry = new ConsumerRegistry();
        consumerRegistry.setConnectionString(database.ConnectionString);
        var messageService = new Mock<IMessageService>();
        var service = new DataController(logger.Object, consumerRegistry, messageService.Object);
        
        // Act
        service.CreateEndpoint("Test_Endpoint");
        var testEndpoint = service.ListEndpoints().First();
        var result = service.UpdateEndpoint(new MetadataHost(new Guid(), "Test_Endpoint_Updated"));
        var testEndpointUpdated = service.ListEndpoints().First();
        
        // Assert
        Assert.That(result, Is.False);
        Assert.That(testEndpoint.Name, Is.EqualTo("Test_Endpoint"));
        Assert.That(testEndpointUpdated.Name, Is.Not.EqualTo("Test_Endpoint_Updated"));
        Assert.That(testEndpointUpdated.Name, Is.EqualTo("Test_Endpoint"));
    }
    
    
    
    
    // ===== DeleteEndpoint ===== //
    [Test]
    public void DeleteEndpoint_ValidEndpoint_EndpointDeletedSuccessfully()
    {
        // Arrange
        var logger = new Mock<ILogger<DataController>>();
        var consumerRegistry = new ConsumerRegistry();
        consumerRegistry.setConnectionString(database.ConnectionString);
        var messageService = new Mock<IMessageService>();
        var service = new DataController(logger.Object, consumerRegistry, messageService.Object);
        
        // Act
        service.CreateEndpoint("Endpoint");
        service.CreateEndpoint("Endpoint_2");
        var endpoints = service.ListEndpoints();
        var endpoint1 = endpoints.FirstOrDefault(endpoint => endpoint.Name == "Endpoint");
        var endpoint2 = endpoints.FirstOrDefault(endpoint => endpoint.Name == "Endpoint_2");
        
        
        
        var result = service.DeleteEndpoint(endpoint2);
        var endpointsAfterDeletion = service.ListEndpoints();
        var endpoint1AfterDeletion = endpointsAfterDeletion.FirstOrDefault(expected => 
            expected.Name == endpoint1.Name && 
            expected.Id == endpoint1.Id);
        var endpoint2AfterDeletion = endpointsAfterDeletion.FirstOrDefault(expected => 
            expected.Name == endpoint2.Name && 
            expected.Id == endpoint2.Id);
        
        
        
        // Assert
        Assert.That(result, Is.True);
        
        //Assert.That(endpoints, Has.Count.EqualTo(2));
        Assert.That(endpoints.Count(), Is.EqualTo(2));
        Assert.That(endpoint1, Is.Not.Null);
        Assert.That(endpoint2, Is.Not.Null);
        
        Assert.That(endpointsAfterDeletion.Count(), Is.EqualTo(1));
        Assert.That(endpoint1AfterDeletion, Is.Not.Null);
        Assert.That(endpoint2AfterDeletion, Is.Null);
    }
    
    [Test]
    public void DeleteEndpoint_InvalidEndpoint_EndpointNotDeleted()
    {
        // Arrange
        var logger = new Mock<ILogger<DataController>>();
        var consumerRegistry = new ConsumerRegistry();
        consumerRegistry.setConnectionString(database.ConnectionString);
        var messageService = new Mock<IMessageService>();
        var service = new DataController(logger.Object, consumerRegistry, messageService.Object);
        
        // Act
        service.CreateEndpoint("Endpoint");
        service.CreateEndpoint("Endpoint_2");
        var endpoints = service.ListEndpoints();
        var endpoint1 = endpoints.FirstOrDefault(endpoint => endpoint.Name == "Endpoint");
        var endpoint2 = endpoints.FirstOrDefault(endpoint => endpoint.Name == "Endpoint_2");
        var endpoint_invalid = new MetadataHost(new Guid(), "Endpoint_Invalid");
        
        
        
        var result = service.DeleteEndpoint(endpoint_invalid);
        var endpointsAfterDeletion = service.ListEndpoints();
        var endpoint1AfterDeletion = endpointsAfterDeletion.FirstOrDefault(expected => 
            expected.Name == endpoint1.Name && 
            expected.Id == endpoint1.Id);
        var endpoint2AfterDeletion = endpointsAfterDeletion.FirstOrDefault(expected => 
            expected.Name == endpoint2.Name && 
            expected.Id == endpoint2.Id);
        
        
        
        // Assert
        Assert.That(result, Is.False);
        
        Assert.That(endpoints.Count(), Is.EqualTo(2));
        Assert.That(endpoint1, Is.Not.Null);
        Assert.That(endpoint2, Is.Not.Null);
        
        Assert.That(endpointsAfterDeletion.Count(), Is.EqualTo(2));
        Assert.That(endpoint1AfterDeletion, Is.Not.Null);
        Assert.That(endpoint2AfterDeletion, Is.Not.Null);
    }
    
    
    

    // ===== AddMetadata ===== //
    [Test]
    public void AddMetadata_ValidEndpoint_MetadataAddedSuccessfully()
    {
        // Arrange
        var logger = new Mock<ILogger<DataController>>();
        var consumerRegistry = new ConsumerRegistry();
        consumerRegistry.setConnectionString(database.ConnectionString);
        //var consumerRegistry = new Mock<IConsumerRegistry>();
        var messageService = new Mock<IMessageService>();
        var service = new DataController(logger.Object, consumerRegistry, messageService.Object);
        
        var video = new Mock<Video>();
        //consumerRegistry.Setup(consumerRegistry => consumerRegistry.Resolve(It.IsAny<Guid>())).Returns((Guid guid) => new MetadataHost(guid, "Host_Name"));
        messageService.Setup(consumerRegistry => consumerRegistry.Enqueue(It.IsAny<string>(), It.IsAny<Video>())).Returns(true);
        
        // Act
        service.CreateEndpoint("Test_Endpoint");
        var endpoints = service.ListEndpoints();
        var response = service.AddMetadata(endpoints.First().Id, video.Object);
        
        // Assert
        Assert.That(response, Is.True);
    }
    
    [Test]
    public void AddMetadata_InvalidEndpoint_MetadataNotAdded()
    {
        // Arrange
        var logger = new Mock<ILogger<DataController>>();
        var consumerRegistry = new ConsumerRegistry();
        consumerRegistry.setConnectionString(database.ConnectionString);
        var messageService = new Mock<IMessageService>();
        var service = new DataController(logger.Object, consumerRegistry, messageService.Object);
        
        var video = new Mock<Video>();
        messageService.Setup(consumerRegistry => consumerRegistry.Enqueue(It.IsAny<string>(), It.IsAny<Video>())).Returns(true);
        
        // Act
        service.CreateEndpoint("Test_Endpoint");
        // Add metadata to random endpoint
        var response = service.AddMetadata(new Guid(), video.Object);
        
        // Assert
        Assert.That(response, Is.False);
    }
}
