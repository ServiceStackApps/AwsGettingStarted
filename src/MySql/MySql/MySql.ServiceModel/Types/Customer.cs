using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace MySql.ServiceModel.Types
{
    public class Customer
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
