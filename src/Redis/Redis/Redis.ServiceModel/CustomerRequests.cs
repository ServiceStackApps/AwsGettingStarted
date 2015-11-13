using Redis.ServiceModel.Types;
using ServiceStack;

namespace Redis.ServiceModel
{
    [Route("/customers/{Id}", Verbs = "PUT")]
    public class UpdateCustomer : IReturn<UpdateCustomerResponse>
    {
        public long Id { get; set; }
        public string Name { get; set; }    
    }

    public class UpdateCustomerResponse
    {
        public Customer Result { get; set; }
    }

    [Route("/customers/{Id}", Verbs = "GET")]
    public class GetCustomer : IReturn<GetCustomerResponse>
    {
        public long Id { get; set; }
    }

    public class GetCustomerResponse
    {
        public Customer Result { get; set; }
    }
}
