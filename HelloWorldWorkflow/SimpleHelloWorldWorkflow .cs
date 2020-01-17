using HelloWorldWorkflow.StepBody;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Sample01.Steps;

namespace HelloWorldWorkflow
{
    public class SimpleHelloWorldWorkflow: IWorkflow
    {
        public string Id => "SimpleHelloWorldWorkflow";

        public int Version => 2;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<HelloWorldStepBody>()
                .Then<GoodbyeWorld>();
        }
    }
}
