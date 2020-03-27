using CDynamic.WFEngine.Interfaces;
using Dynamic.Core.Log;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CDynamic.WFEngine.Refactor
{
    public class PluginRefactor<TR> where TR: IRefactorCD
    {
        private static ILogger _logger = LoggerManager.GetLogger("PluginRefactor");
        /// <summary>
        /// 获取应用程序域重的插件
        /// </summary>
        /// <typeparam name="ATTR"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public IList<Type> GetPluginList<ATTR>(Assembly assembly) where ATTR : Attribute
        {
            IList<Type> rtnList = new List<Type>();
            try
            {
                var typelist = assembly.GetTypes();
                if (typelist != null)
                {
                    foreach (var item in typelist)
                    {
                        var isIplugin = typeof(TR).IsAssignableFrom(item);
                        var attr = item.GetCustomAttribute<ATTR>();
                        if (isIplugin && attr != null)
                        {
                            rtnList.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            return rtnList;
        }
    }
}
