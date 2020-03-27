using CDynamic.WFEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CDynamic.WFEngine.Core
{
    public interface ICDActivity: IRefactorCD
    {
        bool IsEnable { get; set; }
        ExecutionResult Excute(IStepExecutionContext context);
    }
}
