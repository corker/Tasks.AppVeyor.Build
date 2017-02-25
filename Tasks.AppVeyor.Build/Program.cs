using System;
using System.Reflection;
using Autofac;
using CommandLine;
using log4net;
using log4net.Config;
using Tasks.AppVeyor.Build.Configuration;

namespace Tasks.AppVeyor.Build
{
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            Log.Info("Program started.");
            try
            {
                var options = new ProgramOptions();
                var isValid = Parser.Default.ParseArguments(args, options);
                if (!isValid)
                {
                    Environment.ExitCode = 1;
                    Log.Error("Invalid arguments.");
                }
                else
                {
                    using (var container = AutofacContainerFactory.Create(options))
                    {
                        container.Resolve<ProgramRunner>().Run();
                    }
                    Log.Info("Program finished.");
                }
            }
            catch (Exception e)
            {
                Environment.ExitCode = 1;
                Log.Error("Program terminated.", e);
            }
        }
    }
}