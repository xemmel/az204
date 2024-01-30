

### Create function App Project

```powershell

func init myisolatedfunctions --worker-runtime dotnetIsolated --target-framework net8.0


```

### Create Http Trigger Function

In Project Folder

```powershell

func new -n myfirsthttptrigger -t HttpTrigger

```

### Run Function App Locally

```powershell

func start

```


#### Create Queue Trigger

```powershell

func new -n queuetrigger -t QueueTrigger

```

#### Set up Queue Trigger

```csharp

        [Function(nameof(myqueuetrigger))]
        public void Run([QueueTrigger("invoicequeue", Connection = "thestorageconnection")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }

```

### local.settings.json

```json

    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "thestorageconnection" : "DefaultEndpointsProtocol=https;......"
    }

```

### Queue Trigger with Queue output

```csharp

        [Function(nameof(myqueuetrigger))]
        [QueueOutput("middlequeue", Connection = "thestorageconnection")]
        public async Task<string> RunAsync([QueueTrigger("invoicequeue", Connection = "thestorageconnection")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
            string input = message.MessageText;
            string output = $"The message: {input} has been processed!";

            return output;
        }

```