using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Models;

//install dotnet add package Microsoft.Azure.WebJobs.Extensions.Storage
//dotnet add package Microsoft.Azure.WebJobs.Extensions.Tables

namespace Company.Function
{
    public class QueueTrigger1
    {
        [FunctionName("QueueTrigger1")]
        [return: Table("studenti")]
        public static StudentEntity Run([QueueTrigger("myqueue-items", Connection = "datc6b853_STORAGE")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
           // var student = JsonConvert.DeserializeObject<StudentEntity>(myQueueItem);

            //return student;
            return JsonConvert.DeserializeObject<StudentEntity>(myQueueItem);
        }
    }
}
