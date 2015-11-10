using ServiceStack.DataAnnotations;

namespace Postgres.ServiceModel.Types
{
    public class Customer
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
