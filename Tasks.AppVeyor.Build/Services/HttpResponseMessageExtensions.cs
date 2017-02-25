using System.Net.Http;
using System.Reflection;
using log4net;

namespace Tasks.AppVeyor.Build.Services
{
    public static class HttpResponseMessageExtensions
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Log(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var message = $@"Request succeded.
Request.Url: {response.RequestMessage.RequestUri}
Request.Method: {response.RequestMessage.Method}
Response.StatusCode: {response.StatusCode}
Response.ReasonPhrase: {response.ReasonPhrase}";
                Logger.Debug(message);
            }
            else
            {
                var contents = response.Content.ReadAsStringAsync().Result;
                var message = $@"Request failed.
Request.Url: {response.RequestMessage.RequestUri}
Request.Method: {response.RequestMessage.Method}
Response.StatusCode: {response.StatusCode}
Response.ReasonPhrase: {response.ReasonPhrase}
Response.Content:
{contents}";
                Logger.Warn(message);
            }
        }
    }
}