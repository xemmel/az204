using Azure.Identity;
using Azure.Messaging.ServiceBus;

var credential = new DefaultAzureCredential();



var topicName = "mytopic";
var subscriptionName = "getallmessages";

string serviceBusNamespace = "sdumlcbus";

ServiceBusClient client = 
    new ServiceBusClient(
        fullyQualifiedNamespace: $"{serviceBusNamespace}.servicebus.windows.net",
        credential: credential);

var options = new ServiceBusReceiverOptions
{
    ReceiveMode = ServiceBusReceiveMode.PeekLock
};

var receiver = client.CreateReceiver(
    topicName: topicName, 
    subscriptionName: subscriptionName,
    options: options);

var response = await receiver.ReceiveMessagesAsync(maxMessages: 5);
foreach(var message in response)
{
   var content = message.Body.ToString();
   System.Console.WriteLine($"Received Message: {content}");
};