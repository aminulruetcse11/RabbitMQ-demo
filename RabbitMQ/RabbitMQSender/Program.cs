using RabbitMQ.Client;
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
int counter = 0;
while (true)
{
    Thread.Sleep(1000);
    byte[] message = Encoding.UTF8.GetBytes($"alpha {counter}");
    channel.BasicPublish(exchangeName, routingKey, null, message);
    counter++;
}
channel.Close();
connection.Close();