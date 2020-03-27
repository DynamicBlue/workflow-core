using CDynamic.WFEngine.Core;
using CDynamic.WFEngine.Interfaces;
using Dynamic.Core.Auxiliary;
using Dynamic.Core.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CDynamic.WFEngine.Runtime
{
    internal class DefaultActivityPluginFactory : IActivityPluginFactory
    {
        private ILogger _logger = LoggerManager.GetLogger("DefaultActivityPluginFactory");
        public static readonly int _AssemblyMaxPluginNum = 20;

        public ICDActivity GetActivity(string cdActivityId)
        {
            throw new NotImplementedException();
        }

        public ICDActivity GetActivityInstance(Type acAtivityType)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 创建插件实体，供应用加载
        /// </summary>
        /// <param name="plugdllPath"></param>
        /// <returns></returns>
        public IList<ICDActivity> CreatePlugList(string plugdllPath)
        {
            try
            {
                byte[] buffer = System.IO.File.ReadAllBytes(plugdllPath);
                //if (string.IsNullOrEmpty(PlugDllDir))
                //{
                //    PlugDllDir = Path.GetDirectoryName(plugdllPath);
                //    PluginManager._PluginDirPath = PlugDllDir;
                //}
                Assembly assembly = Assembly.LoadFrom(plugdllPath);
                var plugTypeList = assembly.GetPluginList();//ReflectionHelper.GetTypeFromAssembly(assembly, typeof(IPlugin), typeof(DynamicWebApiAttribute), null);
                if (plugTypeList != null && plugTypeList.Count > _AssemblyMaxPluginNum)
                {
                    DynamicWFAAttribute dwa = plugTypeList.FirstOrDefault().GetAttrValue<DynamicWFAAttribute>();
                    string author = "*";
                    if (dwa != null && dwa.Author != null)
                    {
                        author = dwa.Author;
                    }
                    throw new OverflowException($"警告{assembly.FullName}中活动组件异常溢出！[超过了框架设定单元代码沉余量{_AssemblyMaxPluginNum}，请按框架约束，规范编写代码！作者{author}，已记录云KPI考核]");
                }
                IList<ICDActivity> plugList = new List<ICDActivity>();
                if (plugTypeList != null)
                {
                    foreach (var item in plugTypeList)
                    {
                        var plugItem = CreatePlug(item);
                        if (plugItem != null)
                        {
                            plugList.Add(plugItem);
                        }
                    }
                }
                return plugList;
            }
            catch (Exception ex)
            {
                var fileName = Path.GetFileName(plugdllPath);
                IOHelper.WriteLine(string.Format("初始化{0}插件异常【{1}】", fileName, ex.Message), ConsoleColor.Red);
                _logger.Error(ex.ToString());
                return null;
            }
        }
        public IList<ICDActivity> GetActivityList(string cdActivityDirPath)
        {
            IList<ICDActivity> pluginList = new List<ICDActivity>();
            DirectoryInfo pluginSdir = new DirectoryInfo(cdActivityDirPath);
            var pluginInfoList = pluginSdir.GetFiles("*.dll");
            foreach (var pluginDllFileInfo in pluginInfoList)
            {
                var pluginListPart = CreatePlugList(pluginDllFileInfo.FullName);

                if (pluginListPart != null)
                {
                    foreach (var item in pluginListPart)
                    {
                        pluginList.Add(item);
                    }
                }
            }
            return pluginList;
        }
        public void LoadList(string cdActivityDirPath)
        {
            throw new NotImplementedException();
        }
        public void LoadList(IList<ICDActivity> cdActivityList)
        {
            throw new NotImplementedException();
        }
        public void Load(ICDActivity cdActivityList)
        {
            throw new NotImplementedException();
        }

    }
}
