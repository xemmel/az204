using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace myisolatedfunctions
{
    public class FinalInvoiceProcess
    {
        private readonly ILogger<FinalInvoiceProcess> _logger;

        public FinalInvoiceProcess(ILogger<FinalInvoiceProcess> logger)
        {
            _logger = logger;
        }

        [Function(nameof(FinalInvoiceProcess))]
        [QueueOutput("finalqueue", Connection = "thestorageconnection")]
        public Invoice Run(
            [QueueTrigger("middlequeue", Connection = "thestorageconnection")] 
            Invoice invoice)
        {
            //_logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
            invoice.Qty++;
            return invoice;
        }
    }
}
