using Funq;
using ServiceStack;
using Memcached.ServiceInterface;
using Memcached.ServiceModel.Types;
using ServiceStack.Caching;
using ServiceStack.Caching.Memcached;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Redis;

namespace Memcached
{
    public class AppHost : AppSelfHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost()
            : base("Memcached", typeof(MyServices).Assembly)
        {

        }

        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(
            ":memory:", SqliteDialect.Provider));

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                long customerId = 0;
                long employeeId = 0;
                if (db.CreateTableIfNotExists<Customer>())
                {
                    //Add seed data
                    customerId = db.Insert(new Customer { Name = "John" });
                }
                if (db.CreateTableIfNotExists<Employee>())
                {
                    employeeId = db.Insert(new Employee { FirstName = "Jane", LastName = "Doe", Title = "Manager" });
                }
                if (db.CreateTableIfNotExists<Order>())
                {
                    db.Insert(new Order { CustomerId = customerId, EmployeeId = employeeId, ProductName = "Shoes" });
                    db.Insert(new Order { CustomerId = customerId, EmployeeId = employeeId, ProductName = "Belt" });
                    db.Insert(new Order { CustomerId = customerId, EmployeeId = employeeId, ProductName = "Shirt" });
                    db.Insert(new Order { CustomerId = customerId, EmployeeId = employeeId, ProductName = "Soap" });
                    db.Insert(new Order { CustomerId = customerId, EmployeeId = employeeId, ProductName = "Tie" });
                }
            }

            //AWS ElastiCache servers are NOT accessible from outside AWS
            //use MemoryCacheClient locally
            if (AppSettings.GetString("Environment") == "Production")
            {
                container.Register<ICacheClient>(c => new MemcachedClientCache(
                    AwsElastiCacheConfig.MemcachedNodes));
            }
            else
            {
                container.Register<ICacheClient>(new MemoryCacheClient());
            }
        }
    }
}
