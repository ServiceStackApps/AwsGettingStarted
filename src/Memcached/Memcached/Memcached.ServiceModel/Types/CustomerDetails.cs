using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace Memcached.ServiceModel.Types
{
    public class Customer
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Name { get; set; }

        [Reference]
        public List<Order> Orders { get; set; }
    }

    public class Employee
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
    }

    public class Order
    {
        [AutoIncrement]
        public long Id { get; set; }
        [References(typeof(Customer))]
        public long CustomerId { get; set; }
        [References(typeof(Employee))]
        public long EmployeeId { get; set; }

        public string ProductName { get; set; }
    }
}
