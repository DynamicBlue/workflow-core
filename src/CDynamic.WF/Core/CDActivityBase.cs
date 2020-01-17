using CDynamic.WFEngine.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CDynamic.WFEngine.Core
{
    public abstract class CDActivityBase<TParamter, TConfig> : StepBody, ICDActivity where TConfig : IActivityPluginConfig
    {
        public abstract bool IsEnable { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {

            ExecutionResult exeResult = Excute(context);
            //if (exeResult != null)
            //{
            //   return ExecutionResult.Next();
            //}

            return exeResult;
        }
        public abstract ExecutionResult Excute(IStepExecutionContext context);

        public abstract TParamter ConverterToParamter();

    }
}
