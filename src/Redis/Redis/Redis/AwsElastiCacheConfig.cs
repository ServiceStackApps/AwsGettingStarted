using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Configuration;

namespace Redis
{
    public class AwsElastiCacheConfig
    {
        public static string[] MasterNodes
        {
            get
            {
                var masterNodes = ConfigUtils.GetNullableAppSetting("MasterNodes")
                   ?? Environment.GetEnvironmentVariable("MasterNodes");

                if (string.IsNullOrEmpty(masterNodes))
                    throw new ArgumentException("MasterNodes must be defined in App.config or Environment Variable");

                return masterNodes.Split(',');
            }
        }

        public static string[] SlaveNodes
        {
            get
            {
                var masterNodes = ConfigUtils.GetNullableAppSetting("SlaveNodes")
                   ?? Environment.GetEnvironmentVariable("SlaveNodes");

                if (string.IsNullOrEmpty(masterNodes))
                    throw new ArgumentException("SlaveNodes must be defined in App.config or Environment Variable");

                return masterNodes.Split(',');
            }
        }
    }
}
