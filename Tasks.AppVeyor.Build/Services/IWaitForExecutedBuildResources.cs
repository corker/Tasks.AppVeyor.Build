namespace Tasks.AppVeyor.Build.Services
{
    public interface IWaitForExecutedBuildResources
    {
        BuildResource Wait(BuildResource build);
    }
}