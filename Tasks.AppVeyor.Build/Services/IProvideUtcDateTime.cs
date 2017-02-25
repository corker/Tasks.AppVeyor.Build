using System;

namespace Tasks.AppVeyor.Build.Services
{
    public interface IProvideUtcDateTime
    {
        DateTime Now { get; }
    }
}