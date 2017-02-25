using System;

namespace Tasks.AppVeyor.Build.Configuration
{
    public interface IBuildExecutorConfiguration
    {
        TimeSpan Interval { get; }
        TimeSpan Timeout { get; }
    }
}