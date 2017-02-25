using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Tasks.AppVeyor.Build.Services
{
    public class BuildResourceProvider : IProvideBuilds
    {
        private readonly HttpClient _client;

        public BuildResourceProvider(HttpClient client)
        {
            _client = client;
        }

        public BuildResource Get(string projectSlug, string buildVersion)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"projects/fenestrae/{projectSlug}/build/{buildVersion}");
            var response = _client.SendAsync(request).Result;
            response.Log();
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;
            var jobject = JObject.Parse(json)["build"];
            var status = jobject["status"].Value<string>();
            return new BuildResource(status, projectSlug, buildVersion);
        }
    }
}