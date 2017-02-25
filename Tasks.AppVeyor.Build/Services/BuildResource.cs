namespace Tasks.AppVeyor.Build.Services
{
    public class BuildResource
    {
        public BuildResource(string status, string projectSlug, string buildVersion)
        {
            Status = status;
            ProjectSlug = projectSlug;
            BuildVersion = buildVersion;
        }

        public string Status { get; private set; }
        public string ProjectSlug { get; private set; }
        public string BuildVersion { get; private set; }
    }
}