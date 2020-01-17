using Dynamic.Core.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace CDynamic.WFEngine.Runtime
{
    public class WorkflowRuntime
    {
        public WorkflowRuntime()
        {
            InitEngine();
        }
        private static readonly object _lockObj = new object();

        public IWorkflowHost _Host { get; private set; }
        protected IWorkflowHost InitEngine()
        {
            lock (_lockObj)
            {
                IServiceCollection services = IocUnity.GetServices(DateTime.Now.ToString("yyyyMMddHHmm"));
                services.AddLogging();
                services.AddWorkflow();
                var host = IocUnity.Get<IWorkflowHost>();
                this._Host = host;
                return host;
            }
        }
        public void AutoRegisterWF(string activePluginRootPath)
        { 
           
        }
        public void RegisterWF<T>(T t) where T : IActivityWorkflow, new()
        {
            this._Host.RegisterWorkflow<T>();
        }
        #region 开启流程
        /// <summary>
        /// 开始一条流程
        /// </summary>
        /// <param name="workflowId">流程id</param>
        /// <param name="data">流程中的源头数据</param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public Task<string> StartWF(string workflowId, object data = null, string reference = null)
        {
            return this._Host.StartWorkflow(workflowId, data, reference);
        }
        public Task<string> StartWF(string workflowId, int? version, object data = null, string reference = null)
        {
            return this._Host.StartWorkflow(workflowId, version, data, reference);
        }
        public Task<string> StartWF<TData>(string workflowId, TData data = null, string reference = null) where TData : class, new()
        {
            return this._Host.StartWorkflow<TData>(workflowId, data, reference);
        }
        public Task<string> StartWF<TData>(string workflowId, int? version, TData data = null, string reference = null) where TData : class, new()
        {
            return this._Host.StartWorkflow<TData>(workflowId, version, data, reference);
        }
        #endregion

    }
}
