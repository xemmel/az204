using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace myisolatedfunctions
{
    public class ProcessSplit
    {
        private readonly ILogger<ProcessSplit> _logger;

        public ProcessSplit(ILogger<ProcessSplit> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ProcessSplit))]
        public ResponseMessageType Run([QueueTrigger("splitqueue", Connection = "thestorageconnection")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
            string[] parts = message.MessageText.Split('|');
            string string1 = parts[0];
            string string2 = parts[1];
            
            //Write Blob -> Content
            //SDK

            //Write Queue -> meta (blob name)
            //SDK
            
            var response = new ResponseMessageType
            {
                Output1 = string1,
                Output2 = string2
            };
            return response;
        }
    }

    public class ResponseMessageType
    {
        [QueueOutput("splitoutput1", Connection = "thestorageconnection")]
        public string Output1 { get; set; }

        [QueueOutput("splitoutput2", Connection = "thestorageconnection2")]
        public string Output2 { get; set; }

    }
}
