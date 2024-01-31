using Azure.Identity;
using Azure.Messaging.ServiceBus;




//if (env == LOCAL) 

var credential = new DefaultAzureCredential();



var topicName = "mytopic";



string serviceBusNamespace = "sdumlcbus";

ServiceBusClient client =
    new ServiceBusClient(
        fullyQualifiedNamespace: $"{serviceBusNamespace}.servicebus.windows.net",
        credential: credential);



var sender = client.CreateSender(topicName);
int count = int.Parse(args[1]);
for (int i = 0; i < count; i++)
{
    var message = new ServiceBusMessage($"Hello from .NET Program. {i + 1}");
    message.ApplicationProperties["country"] = args[0];
    await sender.SendMessageAsync(message);
    System.Console.WriteLine("Message sent");

}
