using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Configuration;

namespace Memcached
{
    public class AwsElastiCacheConfig
    {
        public static string[] MemcachedNodes
        {
            get
            {
                var masterNodes = ConfigUtils.GetNullableAppSetting("MemcachedNodes")
                   ?? Environment.GetEnvironmentVariable("MemcachedNodes");

                if (string.IsNullOrEmpty(masterNodes))
                    throw new ArgumentException("MemcachedNodes must be defined in App.config or Environment Variable");

                return masterNodes.Split(',');
            }
        }
    }
}
