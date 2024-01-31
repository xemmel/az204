using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace myisolatedfunctions
{
    public class myqueuetrigger
    {
        private readonly ILogger<myqueuetrigger> _logger;

        public myqueuetrigger(ILogger<myqueuetrigger> logger)
        {
            _logger = logger;
        }

        [Function(nameof(myqueuetrigger))]
        [QueueOutput("middlequeue", Connection = "thestorageconnection")]
        public async Task<Invoice> RunAsync([QueueTrigger("invoicequeue", Connection = "thestorageconnection")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
            string input = message.MessageText;
            
            return new Invoice
            {
                InvoiceId = input,
                Qty = 42
            };
 
           
        }
    }
}
