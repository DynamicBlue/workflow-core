using CDynamic.WFEngine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDynamic.WFEngine.Interfaces
{
    interface IActivityPluginFactory
    {
        ICDActivity GetActivity(string cdActivityId);
        void Load(string cdActivityPath);
        IList<ICDActivity> GetActivityList(string cdActivityPath);
        void Load(IList<ICDActivity> cdActivityList);
        ICDActivity GetActivityInstance(Type acAtivityType);
    }
}
