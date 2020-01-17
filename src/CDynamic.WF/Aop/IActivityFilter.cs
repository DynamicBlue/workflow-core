using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace CDynamic.WFEngine.Aop
{
    public delegate void FilterErrorHandler(Exception ex);

    public interface IActivityFilter
    {
       
        int Priority { get; }
        /// <summary>
        /// 是否启用该过滤器
        /// </summary>
        bool IsEnable { get; set; }
        /// <summary>
        /// Excute执行前过滤执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        void Excuteing(IStepExecutionContext context);
        /// <summary>
        /// Excute执行后过滤执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        void Excuted(IStepExecutionContext context);

        void ExcuteError(Exception ex);

       
    }
}
