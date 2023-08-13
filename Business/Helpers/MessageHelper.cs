using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Business.Helpers;

public class MessageHelper
{
    public static Dictionary<string, IModel> _channels = new();

    public static IModel CreateChannel()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
        };

        var connection = factory.CreateConnection();
        return connection.CreateModel();
    }

    public static IModel CreateMessage<T>(string title, T model)
    {
        var channel = CreateChannel();
        _channels[title] = channel;

        var json = JsonConvert.SerializeObject(model);
        var body = Encoding.UTF8.GetBytes(json);

        channel.QueueDeclare(title, false, false, false, null);

        channel.BasicPublish(exchange: "",
            routingKey: title,
            basicProperties: null,
            body: body);
        return channel;
    }

    public static EventingBasicConsumer ReceiveMessage(IModel channel)
    {
        var consumer = new EventingBasicConsumer(channel);

        channel.BasicConsume(queue: "requestreport", autoAck: true, consumer: consumer);

        return consumer;
    }
}