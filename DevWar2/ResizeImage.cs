using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
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

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    inputStream.CopyTo(ms);
            //    var byteArray = ms.ToArray();
            //    imageSmall.Write(byteArray, 0, byteArray.Length);
            //}

            Image<Rgba32> image = Image.Load(inputStream, new JpegDecoder());
            image.Mutate(x => x.Resize(5,10));

            image.Save(imageSmall, new JpegEncoder());
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