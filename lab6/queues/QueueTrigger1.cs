using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class QueueTrigger1
    {
        private readonly ILogger<QueueTrigger1> _logger;

        public QueueTrigger1(ILogger<QueueTrigger1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(QueueTrigger1))]
        [return: Table("studenti")]
        public void Run([QueueTrigger("myqueue-items", Connection = "lab6datc_STORAGE")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
            var student = JsonConvert.DeserializeObject<StudentEntity>(myQueueItem);

            return student;
        
        }
    }
}
