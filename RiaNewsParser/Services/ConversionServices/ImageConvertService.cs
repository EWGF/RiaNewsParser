using RiaNewsParser.Services.HttpRequestServices;
using System.Collections.Generic;

namespace RiaNewsParser.Services.ConversionServices
{
    /// <summary>
    /// Converts and images from URLs to base64 format.
    /// </summary>
    public static class ImageConvertService
    {
        public static List<string> GetBase64Images(IEnumerable<string> urls)
        {
            var imagesBase64 = new List<string>();
            foreach (var item in urls)
            {
                var a = new HttpRequestHandler(item).GetBase64ImageAsync();
                imagesBase64.Add(a.Result);
            }
            return imagesBase64;
        }
    }
}
