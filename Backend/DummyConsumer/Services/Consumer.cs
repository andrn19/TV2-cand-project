namespace DummyConsumer.Services;

using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class Consumer
{
    public void ConsumeMessages()
    {
        ConnectionFactory factory = new ConnectionFactory() {HostName = "rabbitmq", Port = 5672};
        factory.UserName = "guest";
        factory.Password = "guest";
        IConnection conn = factory.CreateConnection();
        IModel channel = conn.CreateModel();
        channel.QueueDeclare(queue: "Metadata",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received from Rabbit: {0}", message);
            //channel.BasicAck(ea.DeliveryTag, false);
        };
        channel.BasicConsume(queue: "Metadata",
            autoAck: true,
            consumer: consumer);
    }
}