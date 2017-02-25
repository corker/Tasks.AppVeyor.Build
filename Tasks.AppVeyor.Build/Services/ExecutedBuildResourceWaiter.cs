using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using log4net;
using Tasks.AppVeyor.Build.Configuration;

namespace Tasks.AppVeyor.Build.Services
{
    public class ExecutedBuildResourceWaiter : IWaitForExecutedBuildResources
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IBuildExecutorConfiguration _configuration;
        private readonly IProvideUtcDateTime _dateTime;

        private readonly HashSet<string> _waitingStates = new HashSet<string>
        {
             "queued",
             "running"
        };

        private readonly IProvideBuilds _builds;

        public ExecutedBuildResourceWaiter(IBuildExecutorConfiguration configuration, IProvideUtcDateTime dateTime, IProvideBuilds builds)
        {
            _configuration = configuration;
            _dateTime = dateTime;
            _builds = builds;
        }

        public BuildResource Wait(BuildResource build)
        {
            var interval = _configuration.Interval;
            var timeout = _configuration.Timeout;
            Log.Info($"Interval {interval} ");
            Log.Info($"Timeout {timeout} ");
            var timeToStop = _dateTime.Now + timeout;
            while (_waitingStates.Contains(build.Status))
            {
                if (_dateTime.Now > timeToStop)
                {
                    var message = $"Build execution took too much time";
                    throw new TimeoutException(message);
                }
                Thread.Sleep(interval);

                Log.Debug($"Polling build state .. {build.Status}");
                build = _builds.Get(build.ProjectSlug, build.BuildVersion);
            }
            return build;
        }
    }
}