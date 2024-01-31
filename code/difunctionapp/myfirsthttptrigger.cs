using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace myisolatedfunctions
{
    public class myfirsthttptrigger
    {
        private readonly ILogger _logger;

        public myfirsthttptrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<myfirsthttptrigger>();
        }

        [Function("myfirsthttptrigger")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Az-204!");

            return response;
        }
    }
}
