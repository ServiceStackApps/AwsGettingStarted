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
                var connString = ConfigUtils.GetNullableAppSetting("ConnectionString")
                   ?? Environment.GetEnvironmentVariable("ConnectionString");

                if (string.IsNullOrEmpty(connString))
                    throw new ArgumentException("ConnectionString must be defined in App.config or Environment Variable");

                return connString;
            }
        }
    }
}
