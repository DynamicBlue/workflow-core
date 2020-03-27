using System;
using System.Collections.Generic;
using System.Text;

namespace CDynamic.WFEngine
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DynamicWFAAttribute:Attribute
    {
        //  public string PluginID { get; set; }

        public string PluginName { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        /// <summary>
        /// 插件开发作者，根据这个定位开发人员
        /// </summary>
        public string Author { get; set; }

        public DynamicWFAAttribute(string pluginName, string author = "CDynamic-Developer", string description = null, int order = 0)
        {
            this.Author = author;
            //  this.PluginID = pluginID;
            this.PluginName = pluginName;
            this.Description = description;
            this.Order = order;
        }
    }
}
