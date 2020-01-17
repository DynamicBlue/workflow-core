using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Sample01.Steps
{
    public class GoodbyeWorld : StepBody
    {

        private ILogger _logger;

        public GoodbyeWorld(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GoodbyeWorld>();
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Goodbye world");
            _logger.LogInformation("Hi there!");
            var result= ExecutionResult.Next();
            var resultJson = Dynamic.Core.Runtime.SerializationUtility.ObjectToJson(result);
            var contextJson = Dynamic.Core.Runtime.SerializationUtility.ObjectToJson(context);
            return result;
        }
    }
}
