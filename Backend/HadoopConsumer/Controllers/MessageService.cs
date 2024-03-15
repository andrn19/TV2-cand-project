namespace TV2.Backend.Services.HadoopConsumer.Controllers;

using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ClassLibrary.Classes;
using ClassLibrary.Models;

public class MessageService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;
    private IConnection _connection;
    private IModel _channel;
    private IConnectionFactory _connectionFactory;
    private readonly RabbitMqSettings _rabbitMqSettings;

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
        
        _channel.QueueDeclare(queue: _rabbitMqSettings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnMessageReceived;

        _channel.BasicConsume(_rabbitMqSettings.QueueName, false, "", false, false, null, consumer);
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
        //await ProcessMessage(scope, message);
        try
        {
            var metadata = JsonSerializer.Deserialize<Metadata>(message);
            Console.WriteLine(message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        _channel.BasicAck(ea.DeliveryTag, false);
    }
}