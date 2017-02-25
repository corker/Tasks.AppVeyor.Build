namespace Tasks.AppVeyor.Build
{
    public class ProgramConfigurationFactory
    {
        public static ProgramConfiguration Create(ProgramOptions options)
        {
            var apiToken = options.ApiToken;
            var projectSlug = options.ProjectName.ToLowerInvariant().Replace(".", "-");
            var buildVersion = options.BuildVersion;
            return new ProgramConfiguration(apiToken, projectSlug, buildVersion);
        }
    }
}