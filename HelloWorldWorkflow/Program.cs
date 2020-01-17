using Microsoft.Extensions.DependencyInjection;
using System;
using WorkflowCore.Interface;
using WorkflowCore.Sample01.Steps;
using WorkflowCore.Interface;
using Dynamic.Core.Service;
using Microsoft.Extensions.Logging;

namespace HelloWorldWorkflow
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureServices();

            var host = IocUnity.Get<IWorkflowHost>();
            host.RegisterWorkflow<SimpleHelloWorldWorkflow>();
            host.Start();

            host.StartWorkflow("SimpleHelloWorldWorkflow");
            
            Console.ReadLine();
            Console.WriteLine("main ok!");
        }
        private static IServiceProvider ConfigureServices()
        {


            //setup dependency injection
            IServiceCollection services = IocUnity.GetServices(DateTime.Now.ToString("yyyyMMddHHmm"));
            services.AddLogging();
            services.AddWorkflow();
            //services.AddWorkflow(x => x.UseMongoDB(@"mongodb://localhost:27017", "workflow"));
            services.AddTransient<GoodbyeWorld>();

          //  var serviceProvider = services.BuildServiceProvider();

            //config logging
          //  var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
          //  loggerFactory.AddDebug();
            return null;
        }
    }
}
