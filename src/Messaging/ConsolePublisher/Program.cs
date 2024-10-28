using System.Text;
using ProtocolReception.Infrastructure.Messaging.Publisher;
using RabbitMQ.Client;

internal class Program
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory { HostName = "host.docker.internal" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "protocols",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var message = RabbitMQProtocolPublisher.MockProtocol();
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: "protocols",
                             basicProperties: null,
                             body: body);
        Console.WriteLine($" [x] Sent {message}");

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}