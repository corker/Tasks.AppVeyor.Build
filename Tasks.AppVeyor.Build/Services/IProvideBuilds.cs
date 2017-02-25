namespace Tasks.AppVeyor.Build.Services
{
    public interface IProvideBuilds
    {
        BuildResource Get(string projectSlug, string buildVersion);
    }
}