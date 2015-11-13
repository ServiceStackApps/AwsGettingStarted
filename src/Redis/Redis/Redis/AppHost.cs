using Funq;
using ServiceStack;
using Redis.ServiceInterface;
using Redis.ServiceModel.Types;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Redis;

namespace Redis
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost() : base("AWS ElastiCache Example", typeof(MyServices).Assembly) { }

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

            if (AppSettings.GetString("Environment") == "Production")
            {
                container.Register<IRedisClientsManager>(c =>
                    new PooledRedisClientManager(
                        // Primary node from AWS (master)
                        AwsElastiCacheConfig.MasterNodes,
                        // Read replica nodes from AWS (slaves)
                        AwsElastiCacheConfig.SlaveNodes));

                container.Register<ICacheClient>(c =>
                    container.Resolve<IRedisClientsManager>().GetCacheClient());
            }
            else
            {
                container.Register<ICacheClient>(new MemoryCacheClient());
            }
        }
    }
}
