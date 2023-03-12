using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RabbitMQSender
{
    public class RabbitMQService
    {
        public IConnection CreateConnection()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
            factory.ClientProvidedName = "Rabbit MQ sender";

            IConnection connection = factory.CreateConnection();
            return connection;
        }

        public IModel CreateChannel(IConnection connection, string exchangeName = "", string routingKey = "", string queueName = "")
        {
            IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey);

            return channel;
        }

        public void PublishMessage(IModel channel, IConnection connection, string exchangeName, string routingKey,
            dynamic messageBody)
        {
            int counter = 0;
            var stringMessage = JsonSerializer.Serialize(messageBody);
            Thread.Sleep(1000);
            Console.WriteLine($"Sending message {stringMessage}");
            byte[] message = Encoding.UTF8.GetBytes($"{stringMessage}");
            channel.BasicPublish(exchangeName, routingKey, null, message);
            counter++;

        }
    }
}
