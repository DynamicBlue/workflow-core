using System;
using WorkflowCore.Interface;

namespace CDynamic.WFEngine
{
    public interface IActivityWorkflow<TData> : IWorkflow<TData> where TData : new()
    {
        string Name { get; }
        string Des { get; }
    }
    public interface IActivityWorkflow : IActivityWorkflow<object>, IWorkflow
    {
    }
}
