using System.Threading;
using Memcached.ServiceModel;
using Memcached.ServiceModel.Types;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Memcached.ServiceInterface
{
    public class CustomerService : Service
    {
        private static string CacheKey = "customer_details_{0}";

        public object Get(GetCustomer request)
        {
            return this.Request.ToOptimizedResultUsingCache(this.Cache,
                CacheKey.Fmt(request.Id),
                () =>
                {
                    Thread.Sleep(500); //Long request
                    return new GetCustomerResponse
                    {
                        Result = this.Db.LoadSingleById<Customer>(request.Id)
                    };
                });
        }

        public object Put(UpdateCustomer request)
        {
            var customer = this.Db.LoadSingleById<Customer>(request.Id);
            customer = customer.PopulateWith(request.ConvertTo<Customer>());
            this.Db.Update(customer);
            //Invalidate customer details cache
            this.Cache.ClearCaches(CacheKey.Fmt(request.Id));
            return new UpdateCustomerResponse()
            {
                Result = customer
            };
        }
    }
}
