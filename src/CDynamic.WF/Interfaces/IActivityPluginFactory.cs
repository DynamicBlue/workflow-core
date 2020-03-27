using CDynamic.WFEngine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDynamic.WFEngine.Interfaces
{
    interface IActivityPluginFactory
    {
        ICDActivity GetActivity(string cdActivityId);
        IList<ICDActivity> GetActivityList(string cdActivityDirPath);
        void LoadList(string cdActivityDirPath);
        void LoadList(IList<ICDActivity> cdActivityList);
        void Load(ICDActivity cdActivityList);
        ICDActivity GetActivityInstance(Type acAtivityType);
    }
}
