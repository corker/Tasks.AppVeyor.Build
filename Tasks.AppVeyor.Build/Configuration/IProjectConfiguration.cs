namespace Tasks.AppVeyor.Build.Configuration
{
    public interface IProjectConfiguration
    {
        string ProjectSlug { get; }
        string BuildVersion { get; }
    }
}