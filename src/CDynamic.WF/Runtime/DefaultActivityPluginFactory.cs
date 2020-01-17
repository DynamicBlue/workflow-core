using CDynamic.WFEngine.Core;
using CDynamic.WFEngine.Interfaces;
using Dynamic.Core.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDynamic.WFEngine.Runtime
{
    internal class DefaultActivityPluginFactory : IActivityPluginFactory
    {
        private ILogger _logger = LoggerManager.GetLogger("DefaultActivityPluginFactory");

        public ICDActivity GetActivity(string cdActivityId)
        {
            throw new NotImplementedException();
        }

        public ICDActivity GetActivityInstance(Type acAtivityType)
        {
            throw new NotImplementedException();
        }

        public IList<ICDActivity> GetActivityList(string cdActivityPath)
        {
            throw new NotImplementedException();
        }

        public void Load(string cdActivityPath)
        {
            throw new NotImplementedException();
        }

        public void Load(IList<ICDActivity> cdActivityList)
        {
            throw new NotImplementedException();
        }
    }
}
