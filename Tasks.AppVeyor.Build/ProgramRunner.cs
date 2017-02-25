using Tasks.AppVeyor.Build.Services;

namespace Tasks.AppVeyor.Build
{
    public class ProgramRunner
    {
        private readonly IBuildProjects _project;

        public ProgramRunner(IBuildProjects project)
        {
            _project = project;
        }

        public void Run()
        {
            _project.Build();
        }
    }
}