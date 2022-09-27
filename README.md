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

