using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQReceiver;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit MQ receiver";

IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

string exchangeName = "AminulExchangeTwo";
string routingKey = "aminul-routing-key";
string queueName = "bbl";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey);

channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
    var body = args.Body.ToArray();

    string message = Encoding.UTF8.GetString(body);
    var data = JsonSerializer.Deserialize<List<AttendanceData>>(message);
    foreach (var item in data)
    {
        Console.WriteLine($"\t|{item.UserId} | {item.Date}");
        Console.WriteLine("\t|-----------------------------");
    }
    channel.BasicAck(args.DeliveryTag, false);
};

string consumerTag = channel.BasicConsume(queueName, false, consumer);
Console.ReadLine();

channel.BasicCancel(consumerTag);
channel.Close();
connection.Close();