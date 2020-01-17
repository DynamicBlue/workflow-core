using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CDynamic.WFEngine.Core
{
    public interface ICDActivity
    {
        bool IsEnable { get; set; }
        ExecutionResult Excute(IStepExecutionContext context);
    }
}
