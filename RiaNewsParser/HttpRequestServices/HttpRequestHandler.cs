using System;
using System.IO;
using System.Net;

namespace RiaNewsParser.HttpRequestServices
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
        public string GetPageSourceCode()
        {
            var responseStream = GetHttpResponseStream();
            using (StreamReader sr = new StreamReader(responseStream))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// Returns the image from URL in base64 format
        /// </summary>
        public string GetBase64Image()
        {
            using (var memoryStream = new MemoryStream())
            {
                GetHttpResponseStream().CopyTo(memoryStream);
                byte[] res = memoryStream.ToArray();
                return Convert.ToBase64String(res);
            }
        }

        /// <summary>
        /// Returns WebResponse from requested URL.
        /// </summary>
        private Stream GetHttpResponseStream()
        {
            var request = WebRequest.Create(_requestedURL) as HttpWebRequest;
            var response = request.GetResponse() as HttpWebResponse;
            return response.GetResponseStream();
        }
    }

}
