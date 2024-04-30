namespace TV2.Backend.Services.DatabaseRegistry.DataProviders;

using System.Text.Json;
using ClassLibrary.Models.Metadata;
using Interfaces;
using RabbitMQ.Client;
using System.Text;


public class MessageService : IMessageService
{
    ConnectionFactory _factory;
    IConnection _conn;
    IModel _channel;
    
    public MessageService()
    {
        _factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
        _factory.UserName = "username";
        _factory.Password = "password";
        _conn = _factory.CreateConnection();
        _channel = _conn.CreateModel();
        _channel.ExchangeDeclare(exchange: "Metadata", type: ExchangeType.Direct);
    }

    public bool Enqueue(string route, Video video)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(video));
        _channel.BasicPublish(exchange: "Metadata",
            routingKey: route,
            basicProperties: null,
            body: body);
        return true;
    }
}
