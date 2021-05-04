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
        /// Возвращает WebResponse.
        /// </summary>
        private Stream GetHttpResponseStream()
        {

            var request = WebRequest.Create(_requestedURL) as HttpWebRequest;
            var response = request.GetResponse() as HttpWebResponse;
            return response.GetResponseStream();

        }

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

    }

}
