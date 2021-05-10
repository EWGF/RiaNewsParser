using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RiaNewsParser.Services.HttpRequestServices
{

    public class HttpRequestHandler
    {
        public HttpRequestHandler(string requestedURL)
        {
            _requestedURL = requestedURL;
        }

        private string _requestedURL;

        /// <summary>  
        /// Returns the HTML source code as string with HTML-markups
        /// </summary>
        public async Task<string> GetPageSourceCodeAsync()
        {
            using (var responseStream = await GetHttpResponseStreamAsync())
            {
                using (var sr = new StreamReader(responseStream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Returns the image from URL in base64 format
        /// </summary>
        public async Task<string> GetBase64ImageAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var responseStream = await GetHttpResponseStreamAsync())
                    responseStream.CopyTo(memoryStream);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// Returns WebResponse from requested URL.
        /// </summary>
        private async Task<Stream> GetHttpResponseStreamAsync()
        {
            var request = WebRequest.Create(_requestedURL);
            var response = request.GetResponseAsync();
            //"await request.GetResponseAsync()" is not working here.
            response.Wait();
            return response.Result.GetResponseStream();
        }
    }

}
