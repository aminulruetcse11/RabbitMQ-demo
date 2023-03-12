using RabbitMQ.Client;
using RabbitMQSender;

var rabbitMQService = new RabbitMQService();
string exchangeName = "AminulExchangeTwo";
string routingKey = "aminul-routing-key";
string queueName = "viva";
IConnection connection = rabbitMQService.CreateConnection();
IModel channel = rabbitMQService.CreateChannel(connection,exchangeName,routingKey,queueName);
IModel channelbbl = rabbitMQService.CreateChannel(connection,exchangeName,"bbl-routing-key","bbl");

var data = new AttendanceRepository().GetData();
rabbitMQService.PublishMessage(channel, connection, exchangeName, routingKey, data);
rabbitMQService.PublishMessage(channelbbl, connection, exchangeName, routingKey, data);


Console.ReadLine();

channel.Close();


connection.Close();



