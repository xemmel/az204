using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureServices(s => s
        .AddFunctionServices())
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();

