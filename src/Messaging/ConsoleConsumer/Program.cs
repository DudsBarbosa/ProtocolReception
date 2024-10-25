using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.Infrastructure.Messaging.Consumer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json; // Add this using directive

// Existing code...

var factory = new ConnectionFactory { HostName = "host.docker.internal" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "protocols",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
var message = string.Empty;
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    message = Encoding.UTF8.GetString(body);

    Console.WriteLine($" [x] Received {message}");
};

channel.BasicConsume(queue: "protocols",
                     autoAck: true,
                     consumer: consumer);

// Manage consumer message (validate and log and save in database)
var objRabbitMQProtocolConsumer = new RabbitMQProtocolConsumer();
var managedConsumerMessage = objRabbitMQProtocolConsumer.ManageConsumerMessage(message);
Console.WriteLine(managedConsumerMessage);
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();