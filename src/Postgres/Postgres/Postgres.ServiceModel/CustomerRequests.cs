using System.Collections.Generic;
using Postgres.ServiceModel.Types;
using ServiceStack;

namespace Postgres.ServiceModel
{
    public class DeleteCustomer : IReturnVoid
    {
        public int Id { get; set; }
    }

    public class CreateCustomer : IReturn<CreateCustomerResponse>
    {
        public string Name { get; set; }
    }

    public class CreateCustomerResponse
    {
        public Customer Result { get; set; }
    }

    public class GetCustomer : IReturn<GetCustomerResponse>
    {
        public int Id { get; set; }
    }

    public class GetCustomerResponse
    {
        public Customer Result { get; set; }
    }

    public class UpdateCustomer : IReturn<UpdateCustomerResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateCustomerResponse
    {
        public Customer Result { get; set; }
    }

    public class GetCustomersResponse
    {
        public List<Customer> Results { get; set; }
    }

    public class GetCustomers : IReturn<GetCustomersResponse>
    {
    }
}
