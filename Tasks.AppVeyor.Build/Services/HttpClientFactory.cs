using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Tasks.AppVeyor.Build.Configuration;

namespace Tasks.AppVeyor.Build.Services
{
    public class HttpClientFactory
    {
        private readonly IHttpClientConfiguration _configuration;

        public HttpClientFactory(IHttpClientConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpClient Create()
        {
            var uri = new Uri("https://ci.appveyor.com/api/");
            var client = new HttpClient
            {
                BaseAddress = uri
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.ApiToken);
            return client;
        }
    }
}