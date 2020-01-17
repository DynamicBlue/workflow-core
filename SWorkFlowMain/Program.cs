using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace SWorkFlowMain
{
    class Program
    {
        static void Main(string[] args)
        {
        
            Console.WriteLine("Hello World!");
            
        }
        public class MyWorkflow : IWorkflow
        {
            public string Id => throw new NotImplementedException();

            public int Version => throw new NotImplementedException();

            public void Build(IWorkflowBuilder<MyData> builder)
            {
                builder
                    .StartWith<CreateCustomer>()
                    .Then<PushToSalesforce>()
                        .OnError(WorkflowErrorHandling.Retry, TimeSpan.FromMinutes(10))
                    .Then<PushToERP>()
                        .OnError(WorkflowErrorHandling.Retry, TimeSpan.FromMinutes(10));
            }

            public void Build(IWorkflowBuilder<object> builder)
            {
                throw new NotImplementedException();
            }
        }
    }
}
