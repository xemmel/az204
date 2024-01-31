using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace myisolatedfunctions
{
    public class TheGreetingFunction
    {
        private readonly ILogger _logger;
        private readonly IGreetingHandler _greetingHandler;

        public TheGreetingFunction(ILoggerFactory loggerFactory, IGreetingHandler greetingHandler)
        {
            _logger = loggerFactory.CreateLogger<TheGreetingFunction>();
            _greetingHandler = greetingHandler;
        }

        [Function("TheGreetingFunction")]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] 
            HttpRequestData req,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var greeting = await _greetingHandler.GetGreetingAsync(cancellationToken);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString(greeting);

            return response;
        }
    }
}
