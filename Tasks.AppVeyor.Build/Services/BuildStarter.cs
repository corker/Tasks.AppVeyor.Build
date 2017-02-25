using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using log4net;
using Newtonsoft.Json.Linq;

namespace Tasks.AppVeyor.Build.Services
{
    public class BuildStarter : IStartBuilds
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly HttpClient _client;

        public BuildStarter(HttpClient client)
        {
            _client = client;
        }

        public BuildResource Start(string projectSlug, string buildVersion)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "builds");
            var requestContent = new
            {
                accountName = "fenestrae",
                projectSlug,
                branch = "master",
                environmentVariables = new {udocx365_buildVersion = buildVersion}
            };
            var contentString = JObject.FromObject(requestContent).ToString();
            Log.Info($@"Request content:
{contentString}
");
            var content = new StringContent(contentString);
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            request.Content = content;
            var response = _client.SendAsync(request).Result;
            response.Log();
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;
            var jobject = JObject.Parse(json);
            var startedBuildVersion = jobject["version"].Value<string>();
            Log.Info($"NuGet package version is {buildVersion}.");
            Log.Info($"AppVeyor build version is {startedBuildVersion}.");
            var status = jobject["status"].Value<string>();
            return new BuildResource(status, projectSlug, startedBuildVersion);
        }
    }
}