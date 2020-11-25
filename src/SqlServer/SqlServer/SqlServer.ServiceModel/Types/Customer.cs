using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace SqlServer.ServiceModel.Types
{
    [PostCreateTable("INSERT INTO Customer (Name) VALUES ('Foo');" +
                     "INSERT INTO Customer (Name) VALUES ('Bar');")]
    public class Customer
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
