using Funq;
using ServiceStack;
using MySql.ServiceInterface;
using MySql.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace MySql
{
    public class AppHost : AppSelfHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost()
            : base("MySql", typeof(MyServices).Assembly)
        {

        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(
                AwsRdsConfig.ConnectionString, MySqlDialect.Provider));

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                if (db.CreateTableIfNotExists<Customer>())
                {
                    //Add seed data
                    db.Insert(new Customer { Name = "Hello" });
                    db.Insert(new Customer { Name = "World" });
                }
            }
        }
    }
}
