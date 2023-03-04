using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit MQ sender";

IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

string exchangeName = "AminulExchange";
string routingKey = "aminul-routing-key";
string queueName = "aminulQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey);

channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    Task.Delay(TimeSpan.FromSeconds(5)).Wait();
    var body = args.Body.ToArray();

    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Receive:{message}");
    channel.BasicAck(args.DeliveryTag, false);
};

string consumerTag = channel.BasicConsume(queueName, false, consumer);
Console.ReadLine();

channel.BasicCancel(consumerTag);
channel.Close();
connection.Close();