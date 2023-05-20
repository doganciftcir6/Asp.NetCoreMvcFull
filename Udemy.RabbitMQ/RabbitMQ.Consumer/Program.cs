//gönderilen mesajı okumak
//paketi yükle
//channel = connection = connectionFactory
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};
var connection = connectionFactory.CreateConnection();
var channel = connection.CreateModel();
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea)=>
{
    var byteMessage = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(byteMessage);
    Console.WriteLine("Okunan mesaj: " + message);
};
channel.BasicConsume(queue:"hello",autoAck:true,consumer:consumer);
Console.WriteLine("Mesajlar Okundu");
System.Console.ReadKey();
