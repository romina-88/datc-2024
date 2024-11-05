using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace console_storage
{
    class Program
    {
        private static CloudTableClient tableClient;
        private static CloudTable studentsTable;
        static void Main(string[] args)
        {
            Task.Run(async () => { await Initialize(); })
                .GetAwaiter()
                .GetResult();
        }

        static async Task Initialize()
        {
            string storageConnectionString = "<storage-connection-string>";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            tableClient = account.CreateCloudTableClient();

            studentsTable = tableClient.GetTableReference("studentii");

            await studentsTable.CreateIfNotExistsAsync();

            //await AddNewStudent();
          //  await EditStudent();
           // await GetAllStudents();


        }

        private static async Task GetAllStudents()
        {
            Console.WriteLine("Universitate\tCNP\tNume\tEmail\tNumar telefon\tAn");
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>(); //.Where(TableQuery.GenerateFilterCondition("FirstName", QueryComparisons.Equal, "Istvan"));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> resultSegment = await studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                foreach (StudentEntity entity in resultSegment.Results)
                {
                    Console.WriteLine("{0}\t{1}\t{2} {3}\t{4}\t{5}\t{6}", entity.PartitionKey, entity.RowKey, entity.FirstName, entity.LastName,
                    entity.Email, entity.PhoneNumber, entity.Year);
                }
            } while (token != null);
        }

      private static async Task AddNewStudent()
        {
            var student = new StudentEntity("UPT", "2971022253123");
            student.FirstName = "Maria";
            student.LastName = "Popescu";
            student.Email = "maria@gmail.com";
            student.Year = 4;
            student.PhoneNumber = "0711592830942";
            student.Faculty = "AC";

            /* var student2 = new StudentEntity("UCDC", "2971022253123");
            student2.FirstName = "Maria";
            student2.LastName = "Popescu";
            student2.Email = "maria@gmail.com";
            student2.Year = 4;
            student2.PhoneNumber = "0711592830942";
            student2.Faculty = "AC";*/


            var insertOperation = TableOperation.Insert(student);
            // var insertOperation2 = TableOperation.Insert(student2);

            await studentsTable.ExecuteAsync(insertOperation);
           // await studentsTable.ExecuteAsync(insertOperation2);
        }
 

        

     private static async Task EditStudent()
        {
            var student = new StudentEntity("UPT", "1951014203123");
            student.FirstName = "Marius";
            student.Year = 4;
            student.ETag = "*";
            var editOperation = TableOperation.Merge(student);

            await studentsTable.ExecuteAsync(editOperation);
        } 
    }
}