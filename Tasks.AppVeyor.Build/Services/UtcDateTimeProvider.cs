using System;

namespace Tasks.AppVeyor.Build.Services
{
    public class UtcDateTimeProvider : IProvideUtcDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}