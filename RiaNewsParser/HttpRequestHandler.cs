using System.IO;
using System.Net;

namespace RiaNewsParser
{
    /// <summary>  
    /// Возвращает исходный код страницы.
    /// </summary>
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
