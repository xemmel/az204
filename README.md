# Azure Developer
## Morten la Cour


### Function App Local

1. Create new Function app 

```powershell

func init theisolatedfunctions --worker-runtime dotnetIsolated

```

2. Create HttpTrigger Function

```powershell

func new --name MyFirstHttpTrigger --template HttpTrigger

```

3. Func start -> Verify in Postman

4. DI

```csharp

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s => 
            s
            .AddScoped<IGreeter,DummyGreeter>()
            )
    .Build();

host.Run();



public interface IGreeter
{
    Task<string> GetGreetingAsync(CancellationToken cancellationToken = default);
}



internal class DummyGreeter : IGreeter
{
    public async Task<string> GetGreetingAsync(CancellationToken cancellationToken = default)
    {
        return "Hello from DI function";
    }
}


namespace theisolatedfunctions
{
    public class MyFirstHttpTrigger
    {
        private readonly ILogger _logger;
        private readonly IGreeter _greeter;

        public MyFirstHttpTrigger(ILoggerFactory loggerFactory, IGreeter greeter)
        {
            _logger = loggerFactory.CreateLogger<MyFirstHttpTrigger>();
            _greeter = greeter;
        }

        [Function("MyFirstHttpTrigger")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "something/new")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            string greeting = await _greeter.GetGreetingAsync();
            response.WriteString(greeting);

            return response;
        }
    }
}

```


### Queue Triggers

1. Insert a *Storage Account* Connection-String in local.settings "AzureWebJobsStorage"
2. New QueueTrigger

```powershell
func new -n MyQueueTrigger -t QueueTrigger

```

```csharp

    public class MyQueueTrigger
    {
        private readonly ILogger _logger;

        public MyQueueTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MyQueueTrigger>();
        }

        [Function("MyQueueTrigger")]
        [QueueOutput("onpremoutput", Connection = "ExternalStorage")]
        public Family Run([QueueTrigger("onpreminput", Connection = "ExternalStorage")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            Person person = new Person
            {
                Name = myQueueItem,
                Age = 30
            };
            Person person2 = new Person
            {
                Name = myQueueItem,
                Age = 50
            };
            var people = new Person[] { person, person2 };
            var family = new Family {People = people};
            return family;
        }
    }

    public record Family
    {
        public ICollection<Person> People { get; set; }
    }

    public record Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}

```

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "...",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "ExternalStorage" : "...."
    }
}


```
