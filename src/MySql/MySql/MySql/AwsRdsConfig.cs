using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Configuration;

namespace MySql
{
    public class AwsRdsConfig
    {
        public static string ConnectionString
        {
            get
            {
                var accessKey = ConfigUtils.GetNullableAppSetting("ConnectionString")
                   ?? Environment.GetEnvironmentVariable("ConnectionString");

                if (string.IsNullOrEmpty(accessKey))
                    throw new ArgumentException("ConnectionString must be defined in App.config or Environment Variable");

                return accessKey;
            }
        }
    }
}
