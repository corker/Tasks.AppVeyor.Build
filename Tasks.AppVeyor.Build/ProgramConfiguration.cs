using System;
using System.Configuration;
using Tasks.AppVeyor.Build.Configuration;

namespace Tasks.AppVeyor.Build
{
    public class ProgramConfiguration : IHttpClientConfiguration, IProjectConfiguration, IBuildExecutorConfiguration
    {
        public ProgramConfiguration(string apiToken, string projectSlug, string buildVersion)
        {
            ApiToken = apiToken;
            ProjectSlug = projectSlug;
            BuildVersion = buildVersion;
        }

        public TimeSpan Interval
            => GetTimeSpanFromAppSettings("UDocx365.Tasks.AppVeyor.Build.Interval");

        public TimeSpan Timeout
            => GetTimeSpanFromAppSettings("UDocx365.Tasks.AppVeyor.Build.Timeout");

        public string ApiToken { get; }

        public string ProjectSlug { get; }

        public string BuildVersion { get; }

        private static TimeSpan GetTimeSpanFromAppSettings(string key)
        {
            var stringValue = GetValueFromAppSettings(key);
            var value = TimeSpan.Parse(stringValue);
            return value;
        }

        private static string GetValueFromAppSettings(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = $"{key} is not defined in AppSettings section of the application configuration file.";
                throw new InvalidOperationException(message);
            }
            return value;
        }
    }
}