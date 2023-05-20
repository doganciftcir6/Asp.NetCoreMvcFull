//Burada RabbitMQ.Client paketine ihtiyacımız var. Terminalden dotnet add package RabbitMQ.Client yazarak yükleyebiliriz.
//eğer bir kuyruğa mesaj bırakmak istiyorsan veya bir kuyruğu tanımlamak istiyorsan channel kullanmalısın.
//bir channel oluşturabilmek içinde connection kullanmalısın.
//bir connection oluşturabilmek içinde connectionFactory kullanmalısın.
//connectionFactory, connection, channel
//bir şeyi consume veya publish edeceksen channel üzerinden edeceksin.
using System.Text;
using RabbitMQ.Client;
//önce connectionFactory oluştur.
var connectionFactory = new ConnectionFactory()
{
    //localhostumuzdaki rabbitmq ile ayağa kaldırıcaz.
    HostName="localhost",
    //localhost değilde uzaktaki bir rabbitmqye bağlanmak için veya dockerize ettim ona bağlanıcam o zaman
    // Uri = new Uri("connectionstringgirilir"),
    // UserName ="usernamebilgisi",
    // Password ="passwordbilgisi"
};
//daha sonra connectionFactory üzerinden connection oluştur.
var connection = connectionFactory.CreateConnection();
//daha sonra connection üzerinden bir channel oluştur.
var channel = connection.CreateModel();
//öncelikle bir kuyruk tanımla
//queue: parametresi oluşacak kuyruğun ismidir. durable: parametresi ise rabbitmqnun restart durumunda kuyruk silinsin mi silinmesin mi durumudur false dersen rabbitmq restart edildiğinde ilgili kuyruk silinecektir true yaparsan kuyruk silinmez. exclusive: parametresi ise bu channel haricinde başka channellerin kullanıp kullanamyacağınızı belirttiğiniz yerdir eğer false denirse başkaları kullanalabilir. autoDelete: parametresi ise son consumer unsubscribe olduğunda ben burdaki kuyruğu sileyim silmeyeyim mi diye soruyor false diyerek silme diyoruz.
channel.QueueDeclare(queue:"hello", durable:false, exclusive:false, autoDelete:false, arguments:null);
//bir kuyruğu tanımladık ancak bir mesaj göndermedik mesaj gönderme işlemini channel üzerinden yapacağız.
//exchange parametresi boş bırakılırsa rabbitmq default bir exchange vardır onu ayağa kaldırır.
//byte dizisi oluşturmamız lazım ki o byte dizisi içerisinde ilgili mesajı gönderebilelim
//routingKey parametresine yukarıda verdiğimiz kuyruğun ismini giriyoruz.
var message = "Hello RabbitMQ";
var byteMessage = Encoding.UTF8.GetBytes(message);
//BasicPublish ile mesajı posta kutusuna atıyoruz.
channel.BasicPublish(exchange:"",routingKey:"hello",basicProperties:null,body:byteMessage);
Console.WriteLine("Mesaj Gönderildi");


