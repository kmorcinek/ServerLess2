
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace DevWar2
{
    public static class CreateEntry
    {
        [FunctionName("CreateEntry")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                PhotoOrder orderData = JsonConvert.DeserializeObject<PhotoOrder>(requestBody);
                //orderData.PartitionKey = System.DateTime.UtcNow.DayOfYear.ToString();
                //orderData.RowKey = orderData.FileName;
                //ordersTable.Add(orderData);

                return (ActionResult)new OkObjectResult($"Order processed: {orderData.FileName}");
            }
            catch (System.Exception ex)
            {
                //log.Error("Something went wrong", ex);
                return new BadRequestObjectResult("Something went wrong");
            }

            return (ActionResult)new OkObjectResult($"Order processed");

            //string name = req.Query["name"];

            //string requestBody = new StreamReader(req.Body).ReadToEnd();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //return name != null
            //    ? (ActionResult)new OkObjectResult($"Hello, {name}")
            //    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }

    public class PhotoOrder : TableEntity
    {
        public string CustomerEmail { get; set; }
        public string FileName { get; set; }
        public int RequiredHeight { get; set; }
        public int RequiredWidth { get; set; }
    }
}
