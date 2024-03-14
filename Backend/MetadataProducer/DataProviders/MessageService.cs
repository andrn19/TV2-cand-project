namespace TV2.Backend.Services.MetadataProducer.DataProviders;

using ClassLibrary.Classes;
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
        Console.WriteLine("Connecting to rabbit");
        _factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
        _factory.UserName = "guest";
        _factory.Password = "guest";
        _conn = _factory.CreateConnection();
        _channel = _conn.CreateModel();
        _channel.QueueDeclare(queue: "Metadata",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public bool Enqueue(Metadata message)
    {
        var body = Encoding.UTF8.GetBytes(message.Description);
        _channel.BasicPublish(exchange: "",
            routingKey: "Metadata",
            basicProperties: null,
            body: body);
        Console.WriteLine(" [x] Published {0} to RabbitMQ", message.Description);
        return true;
    }
}