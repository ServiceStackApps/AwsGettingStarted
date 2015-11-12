using MySql.ServiceModel;
using MySql.ServiceModel.Types;
using ServiceStack;
using ServiceStack.OrmLite;

namespace MySql.ServiceInterface
{
    public class CustomerService : Service
    {
        public GetCustomersResponse Get(GetCustomers request)
        {
            return new GetCustomersResponse { Results = Db.Select<Customer>() };
        }

        public GetCustomerResponse Get(GetCustomer request)
        {
            var customer = Db.SingleById<Customer>(request.Id);
            if (customer == null)
                throw HttpError.NotFound("Customer not found");

            return new GetCustomerResponse
            {
                Result = Db.SingleById<Customer>(request.Id)
            };
        }

        public CreateCustomerResponse Post(CreateCustomer request)
        {
            var customer = new Customer { Name = request.Name };
            Db.Save(customer);
            return new CreateCustomerResponse
            {
                Result = customer
            };
        }

        public UpdateCustomerResponse Put(UpdateCustomer request)
        {
            var customer = Db.SingleById<Customer>(request.Id);
            if (customer == null)
                throw HttpError.NotFound("Customer '{0}' does not exist".Fmt(request.Id));

            customer.Name = request.Name;
            Db.Update(customer);

            return new UpdateCustomerResponse
            {
                Result = customer
            };
        }

        public void Delete(DeleteCustomer request)
        {
            Db.DeleteById<Customer>(request.Id);
        }
    }
}
