using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace DevWar2
{
    public class ResizeImage
    {
        [FunctionName("ResizeImage")]
        public static void Run(
            [BlobTrigger("photos/{name}", Connection = "StorageConnection")] Stream inputStream,
            [Blob("resized-photos/{name}", FileAccess.Write, Connection = "StorageConnection")] Stream imageSmall,
            TraceWriter log)
        {
            log.Info("Resized");

            Image<Rgba32> image = Image.Load(inputStream, new JpegDecoder());
            image.Mutate(x => x.Resize(5,10));

            image.Save(imageSmall, new JpegEncoder());
        }
    }
}