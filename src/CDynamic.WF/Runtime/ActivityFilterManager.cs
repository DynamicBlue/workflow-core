using CDynamic.WFEngine.Aop;
using Dynamic.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCore.Interface;

namespace CDynamic.WFEngine.Runtime
{
    
    public class ActivityFilterManager
    {
        private ILogger _logger = LoggerManager.GetLogger("ActivityFilterManager");
        protected static readonly List<IActivityFilter> _GlobActivityFiltersList = new List<IActivityFilter>();
        protected static readonly object _lockObj = new object();
       
        public bool IsEnable { get; set; }

        public int Priority => 0;

        public virtual void Excuted(IStepExecutionContext context)
        {
            var filterListSort = _GlobActivityFiltersList.Where(f => f.IsEnable).OrderByDescending(f => f.Priority);
            foreach (var filterItem in filterListSort)
            {
                try
                {
                    filterItem.Excuted(context);
                }
                catch (Exception ex)
                {
                    this.ExcuteError(ex);
                    filterItem.ExcuteError(ex);
                }
            }
        }
        public virtual void Excuteing(IStepExecutionContext context)
        {
            var filterListSort = _GlobActivityFiltersList.Where(f => f.IsEnable).OrderByDescending(f => f.Priority);
            foreach (var filterItem in filterListSort)
            {
                try
                {
                    filterItem.Excuteing(context);
                }
                catch (Exception ex)
                {
                    this.ExcuteError(ex);
                    filterItem.ExcuteError(ex);
                }
            }
        }

        public void ExcuteError(Exception ex)
        {
            _logger.Error(ex.ToString());
        }
    }
}
