using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace HelloWorldWorkflow.StepBody
{
    public class HelloWorldStepBody: IStepBody
    {
      
        public async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Console.WriteLine("Hello world");
            return ExecutionResult.Next();
        }
    }
}
