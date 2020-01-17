using System;
using System.Collections.Generic;
using System.Text;

namespace CDynamic.WFEngine.Aop
{
    /// <summary>
    /// 执行活动全局过滤器（所有节点都会生效）
    /// </summary>
    public interface IGlobActivityFilter: IActivityFilter
    {
        string Name { get; set; }
        /// <summary>
        /// 过滤器id
        /// </summary>
        string Id { get; set; }
        string Des { get; set; }
        
    }
}
