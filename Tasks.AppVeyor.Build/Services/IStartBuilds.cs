namespace Tasks.AppVeyor.Build.Services
{
    public interface IStartBuilds
    {
        BuildResource Start(string projectSlug, string buildVersion);
    }
}