using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProtocolReception.ApplicationCore.Interfaces;
using ProtocolReception.ApplicationCore.Services;
using ProtocolReception.Infrastructure.Repositories;
using ProtocolReception.Infrastructure.Repositories.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Existing code...

public class Program
{
    private static ProtocolLogService? _logService;
    private static IProtocolLogRepository? _protocolLogRepository;
    private static ProtocolService? _protocolService;
    private static IProtocolRepository? _protocolRepository;
    private static ProtocolContext? _protocolContext;

    private static void Main(string[] args)
    {
        //setup our DI
        var services = new ServiceCollection()
            .AddScoped<ProtocolLogService>()
            .AddScoped<ProtocolService>()
            .AddScoped<IProtocolRepository, ProtocolRepository>()
            .AddScoped<IProtocolLogRepository, ProtocolLogRepository>()
            .AddDbContext<ProtocolContext>(options => options.UseSqlServer("Server=host.docker.internal,1433;Database=Documents;User Id=;Password=;TrustServerCertificate=True"));

        var serviceProvider = services.BuildServiceProvider();
        _logService = serviceProvider.GetService<ProtocolLogService>();
        _protocolService = serviceProvider.GetService<ProtocolService>();
        _protocolLogRepository = serviceProvider.GetService<IProtocolLogRepository>();
        _protocolRepository = serviceProvider.GetService<IProtocolRepository>();
        _protocolContext = serviceProvider.GetService<ProtocolContext>();


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
        string message = string.Empty;
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            message = Encoding.UTF8.GetString(body);

            Console.WriteLine($" [x] Received {message}");
        };

        channel.BasicConsume(queue: "protocols",
                             autoAck: true,
                             consumer: consumer);

        var protocolLogService = serviceProvider.GetService<ProtocolLogService>();
        if (protocolLogService != null)
        {
            var task = protocolLogService.ManageConsumerMessage(message);
            Console.WriteLine($" [x] Managed message: {task.Result}");
        }
        else
        {
            Console.WriteLine(" [x] ProtocolLogService is not available.");
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
