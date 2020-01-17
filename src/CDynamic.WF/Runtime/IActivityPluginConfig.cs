using Dynamic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDynamic.WFEngine.Runtime
{
    public interface IActivityPluginConfig
    {

        Task<object> GetConfig(ApiContext context, bool replaceParameter = true);

        Task<bool> SaveConfig(object cfg);

        object GetDefaultConfig(ApiContext context);

        Task<ResultModel> ConfigChanged(ApiContext context, object newConfig);

        Task RefreshConfig(ApiContext context);
    }
    public abstract class ActivityPluginConfigBase : IActivityPluginConfig
    {
        public Task<ResultModel> ConfigChanged(ApiContext context, object newConfig)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetConfig(ApiContext context, bool replaceParameter = true)
        {
            throw new NotImplementedException();
        }

        public object GetDefaultConfig(ApiContext context)
        {
            throw new NotImplementedException();
        }

        public Task RefreshConfig(ApiContext context)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveConfig(object cfg)
        {
            throw new NotImplementedException();
        }
    }
    public class ApiContext
    {
    }
}
