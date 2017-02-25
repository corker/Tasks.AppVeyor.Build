using System.Net.Http;
using Autofac;
using Tasks.AppVeyor.Build.Services;

namespace Tasks.AppVeyor.Build.Configuration
{
    public static class AutofacContainerFactory
    {
        public static IContainer Create(ProgramOptions options)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(ProgramConfigurationFactory.Create(options)).AsImplementedInterfaces();
            builder.RegisterType<ProgramRunner>().SingleInstance();

            builder.RegisterType<HttpClientFactory>().SingleInstance();
            builder.Register(CreateHttpClient).SingleInstance();

            builder.RegisterType<UtcDateTimeProvider>().AsImplementedInterfaces();
            builder.RegisterType<BuildStarter>().AsImplementedInterfaces();
            builder.RegisterType<BuildResourceProvider>().AsImplementedInterfaces();
            builder.RegisterType<ExecutedBuildResourceWaiter>().AsImplementedInterfaces();
            builder.RegisterType<ProjectBuilder>().AsImplementedInterfaces();

            return builder.Build();
        }

        private static HttpClient CreateHttpClient(IComponentContext context)
        {
            return context.Resolve<HttpClientFactory>().Create();
        }
    }
}