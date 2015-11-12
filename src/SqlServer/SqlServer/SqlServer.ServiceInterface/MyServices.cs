using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using SqlServer.ServiceModel;

namespace SqlServer.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        }
    }
}