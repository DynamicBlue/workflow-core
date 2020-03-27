using Dynamic.Core.Service;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using Dynamic.Core.Log;
using System.Reflection;
using System.Linq;
using System.Threading;

using System.Threading.Tasks;


using Dynamic.Core.Extensions;
using Dynamic.Core.Auxiliary;
using CDynamic.WFEngine.Interfaces;

namespace CDynamic.WFEngine.Extions
{
    public static class PluginExtions<TR> where T: IRefactorCD
    {
        private static ILogger _logger = LoggerManager.GetLogger("PluginExtions");
        private static readonly object _objLock = new object();
        public static void InitExcute(IPlugin plugin, ApiContext context)
        {
            if (plugin != null)
            {
                plugin.Init(context);
            }
        }
        private static void InitPluginItem<T>(T item, string pluginPath,ApiContext context = null)
        {
            try
            {

                if (builder != null)
                {
                    lock (_objLock)
                    {//必须线程安全才能执行
                        builder.AddApplicationPart(item.GetType().Assembly).AddControllersAsServices();
                    }
                }
                if (context == null)
                {
                    context = new ApiContext();
                }
                context.SetCurrentPlugConfigPath(ConfigHelper.GetConfigNativeSuperPath(item.PluginID));

                if (!Directory.Exists(context.CurrentPlugConfigPath))
                {
                    Directory.CreateDirectory(context.CurrentPlugConfigPath);
                }
                item.InitExcute(context);


                IOHelper.WriteLine(string.Format("【{2}】初始化插件=》{1}【{0}】", item.PluginID, item.PluginName, item.InitPriority), ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                IOHelper.WriteLine(string.Format("{0}插件执行Init方法执行失败！-【{1}】", item.PluginName, item.PluginID), ConsoleColor.Red);
                _logger.Error(ex.ToString());
            }
        }
        public static void InitExcute<T>(this IDictionary<string, IPlugin> plugins, string pluginPath, IMvcBuilder builder = null, ApiContext context = null)
        {

            if (plugins != null)
            {
                IOHelper.WriteLine($"\r\n#####识别到{plugins.Count}个插件，开始初始化加载插件#####\r\n", ConsoleColor.White);

                Task.Factory.StartNew(() =>
                {
                    var systemPlugins = (plugins.Where(f => f.Value.InitPriority > 0)).OrderByDescending(f => f.Value.InitPriority);
                    foreach (var systemItem in systemPlugins)
                    {
                        InitPluginItem(systemItem.Value, pluginPath, builder);
                    }
                }).ContinueWith((obj) =>
                {

                    var logicPluginList = plugins.Where(f => f.Value.InitPriority <= 0);
                    foreach (var localPluginItem in logicPluginList)
                    {

                        Task.Factory.StartNew(() =>
                        {
                            InitPluginItem(localPluginItem.Value, pluginPath, builder);
                        }).Wait();

                    }
                }).Wait();
                IOHelper.WriteLine($"\r\n#####{plugins.Count}个插件初始化完成,等待调用！#####\r\n", ConsoleColor.White);
            }
        }
        public static void InitExcute(this IList<IPlugin> plugins, ApiContext context)
        {
            if (plugins != null)
            {
                foreach (var item in plugins)
                {
                    item.InitExcute(context);
                }
            }
        }
        public static T GetAttrValue<T>(this IPlugin plugin) where T : Attribute
        {
            return plugin.GetType().GetCustomAttributes(typeof(T), false)[0] as T;
        }
        public static T GetAttrValue<T>(this Type pluginType) where T : Attribute
        {
            return pluginType.GetCustomAttributes(typeof(T), false)[0] as T;
        }
        public static IPlugin LoadDynamicWebApiAttribute(this IPlugin pluginObj)
        {
            //后续优化改用反射对应加载同名属性

            var attrObj = GetAttrValue<DynamicWebApiAttribute>(pluginObj);
            // pluginObj.PluginID = attrObj.PluginID;
            pluginObj.PluginName = attrObj.PluginName;
            pluginObj.Description = attrObj.Description;
            pluginObj.Order = attrObj.Order;
            return pluginObj;
        }
        public static IPlugin LoadDynamicWebApiAttribute(this IPlugin pluginObj, DynamicWebApiAttribute attrObj)
        {
            //后续优化改用反射对应加载同名属性

            //  pluginObj.PluginID = attrObj.PluginID;
            pluginObj.PluginName = attrObj.PluginName;
            pluginObj.Description = attrObj.Description;
            pluginObj.Order = attrObj.Order;
            return pluginObj;
        }

        public static void InitWebApiPlug(this IServiceCollection services, IMvcBuilder builder, string pluginPath)
        {
            ConfigHelper._PlugPath = pluginPath;
            IPluginFactory plugFactory = new DefaultWebApiPluginFactory(builder);

            Task.Factory.StartNew(() =>
            {
                plugFactory.Load(pluginPath);

            }).ContinueWith((obj) =>
            {
                PluginManager.InitExcute(pluginPath, builder);

            }).ContinueWith((obj) =>
            {
                IocUnity.AddSingleton<SessionManager>(new SessionManager(IocUnity.Get<ICache>()));
            }).Wait();
        }
        /// <summary>
        /// 手动加载插件，解决IL无法调试功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        /// <param name="plugins"></param>
        /// <param name="pluginPath"></param>
        public static void InitWebApiPlug(this IServiceCollection services, IMvcBuilder builder, IList<IPlugin> plugins, string pluginPath)
        {
            ConfigHelper._PlugPath = pluginPath;
            IPluginFactory plugFactory = new DefaultWebApiPluginFactory(builder);

            Task.Factory.StartNew(() =>
            {
                var dllPluginList = plugFactory.GetPluginList(pluginPath);
                if (plugins != null)
                {
                    plugins.Foreach((item) =>
                    {
                        if (item != null && !dllPluginList.Any(f => f.PluginID == item.PluginID))
                        {
                            dllPluginList.Add(item);
                        }
                    });
                }
                plugFactory.Load(dllPluginList);

            }).ContinueWith((obj) =>
            {
                PluginManager.InitExcute(pluginPath, builder);

            }).ContinueWith((obj) =>
            {
                IocUnity.AddSingleton<SessionManager>(new SessionManager(IocUnity.Get<ICache>()));
            }).Wait();
        }
      
    }
}
