using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace DevWar2
{
    public class ResizeImage
    {
        [FunctionName("ResizeImage")]
        public static void Run(
            [BlobTrigger("photos/{name}", Connection = "StorageConnection")] Stream image,
            [Blob("resized-photos/{name}", FileAccess.Write, Connection = "StorageConnection")] Stream imageSmall,
            TraceWriter log)
        {
            log.Info("Resized");

            using (MemoryStream ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var byteArray = ms.ToArray();
                imageSmall.Write(byteArray, 0, byteArray.Length);
            }
            //var imageBuilder = ImageResizer.ImageBuilder.Current;
            //var size = imageDimensionsTable[ImageSize.Small];

            //imageBuilder.Build(image, imageSmall,
            //    new ResizeSettings(size.Item1, size.Item2, FitMode.Max, null), false);

            //image.Position = 0;
            //size = imageDimensionsTable[ImageSize.Medium];

            //imageBuilder.Build(image, imageMedium,
            //    new ResizeSettings(size.Item1, size.Item2, FitMode.Max, null), false);
        }
    }
}