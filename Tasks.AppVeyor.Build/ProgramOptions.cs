using CommandLine;

namespace Tasks.AppVeyor.Build
{
    public class ProgramOptions
    {
        [Option(Required = true, HelpText = "AppVeyor API token")]
        public string ApiToken { get; set; }

        [Option(Required = true)]
        public string ProjectName { get; set; }

        [Option(Required = true)]
        public string BuildVersion { get; set; }
    }
}