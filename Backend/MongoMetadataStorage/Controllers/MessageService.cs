namespace TV2.Backend.Services.MongoMetadataStorage.Controllers;

using ClassLibrary.Models.Metadata;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ClassLibrary.Models;
using MongoDB.Driver;

public class MessageService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;
    private IConnection _connection;
    private IModel _channel;
    private IConnectionFactory _connectionFactory;
    private readonly RabbitMqSettings _rabbitMqSettings;
    private readonly MongoClient _dbClient;
    private static IMongoCollection<Video> _metadataCollection;
    private string _queueName;

    public MessageService(ILogger<MessageService> logger, IOptions<RabbitMqSettings> rabbitMqSettings, IServiceProvider serviceProvider)
    {
        // Create HostedService
        _serviceProvider = serviceProvider;
        _logger = logger;
        
        // Get settings from configuration file and instantiate connection
        _rabbitMqSettings = rabbitMqSettings.Value;
        _connectionFactory = _rabbitMqSettings.CreateConnectionFactory();
        _connection = _connectionFactory.CreateConnection();
        _channel?.Dispose();
        _channel = _connection.CreateModel();
        
        _channel.ExchangeDeclare(exchange: "Metadata", type: ExchangeType.Direct);
        
        // declare a server-named queue
        _queueName = _channel.QueueDeclare().QueueName;
        
        string mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        if (mongoConnectionString == null)
        {
            Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
            Environment.Exit(0);
        }
        _dbClient = new MongoClient(mongoConnectionString);
        var metadataDatabase = _dbClient.GetDatabase("metadata_storage");
        _metadataCollection = metadataDatabase.GetCollection<Video>("Metadata");
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _channel.QueueBind(queue: _queueName,
            exchange: "Metadata",
            routingKey: _rabbitMqSettings.RoutingKey);
        
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnMessageReceived;
        
        _channel.BasicConsume(queue:_queueName, autoAck: false, consumer: consumer);
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _connection?.Close();
        return Task.CompletedTask;
    }
    
    protected virtual async void OnMessageReceived(object model, BasicDeliverEventArgs ea)
    {
        var message = Encoding.UTF8.GetString(ea.Body.ToArray());
        using var scope = _serviceProvider.CreateScope();
        try
        {
            var metadata = JsonSerializer.Deserialize<Video>(message);
            await _metadataCollection.InsertOneAsync(metadata);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        _channel.BasicAck(ea.DeliveryTag, false);
    }
}