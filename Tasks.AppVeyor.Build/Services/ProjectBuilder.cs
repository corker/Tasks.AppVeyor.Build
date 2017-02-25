using System;
using System.Reflection;
using log4net;
using Tasks.AppVeyor.Build.Configuration;

namespace Tasks.AppVeyor.Build.Services
{
    public class ProjectBuilder : IBuildProjects
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IStartBuilds _builds;
        private readonly IWaitForExecutedBuildResources _waiter;
        private readonly IProjectConfiguration _configuration;

        public ProjectBuilder(IStartBuilds builds, IWaitForExecutedBuildResources waiter, IProjectConfiguration configuration)
        {
            _builds = builds;
            _waiter = waiter;
            _configuration = configuration;
        }

        public void Build()
        {
            Log.Info($"Build for project {_configuration.ProjectSlug} version {_configuration.BuildVersion}. Started.");
            var build = _builds.Start(_configuration.ProjectSlug, _configuration.BuildVersion);
            build = _waiter.Wait(build);
            Log.Debug("Validating build state..");
            if (build.Status != "success")
            {
                var message = $"Build for project {_configuration.ProjectSlug} finished with {build.Status}.";
                throw new IndexOutOfRangeException(message);
            }
            Log.Info($"Build for project {_configuration.ProjectSlug} version {_configuration.BuildVersion}. Finished.");
        }
    }
}