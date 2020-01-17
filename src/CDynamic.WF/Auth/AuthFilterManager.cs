using CDynamic.WFEngine.Aop;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using System.Linq;
using System.Threading.Tasks;
using Dynamic.Core.Log;
using CDynamic.WFEngine.Runtime;

namespace CDynamic.WFEngine.Auth
{
    /// <summary>
    /// 流程权限认证
    /// </summary>
    public class AuthFilterManager : ActivityFilterManager
    {
        public AuthFilterManager()
        {
        }
        public override void Excuteing(IStepExecutionContext context)
        {
            base.Excuteing(context);
        }
    }
}
