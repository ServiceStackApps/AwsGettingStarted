using System;
using ServiceStack.Configuration;

namespace SqlServer
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
