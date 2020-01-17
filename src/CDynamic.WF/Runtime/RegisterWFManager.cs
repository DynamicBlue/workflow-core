using System;
using System.Collections.Generic;
using System.Text;

namespace CDynamic.WFEngine.Runtime
{
    public  class RegisterWFManager
    {
        public string WFPluginPath { get;protected  set; }
        public RegisterWFManager(string wfpluginPath)
        {
            this.WFPluginPath = wfpluginPath;
        }
    }
}
