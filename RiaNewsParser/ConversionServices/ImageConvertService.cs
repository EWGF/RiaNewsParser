using RiaNewsParser.HttpRequestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiaNewsParser.ConversionServices
{
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
