using System;
using NUnit.Framework;
using MySql.ServiceInterface;
using MySql.ServiceModel;
using MySql.ServiceModel.Types;
using ServiceStack.Testing;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace MySql.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private readonly ServiceStackHost appHost;

        public UnitTests()
        {
            appHost = new AppHost();
            appHost.Init().Start("http://*:2337/");
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            //Drop table
            using (var db = appHost.Container.Resolve<IDbConnectionFactory>().OpenDbConnection())
            {
                db.DropTable<Customer>();
            }
            appHost.Dispose();
        }

        [Test]
        public void CanGetAllCustomers()
        {
            var service = appHost.Container.Resolve<CustomerService>();

            var response = service.Get(new GetCustomers());

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Results, Is.Not.Null);
            Assert.That(response.Results.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetCustomer()
        {
            var service = appHost.Container.Resolve<CustomerService>();

            var response = service.Get(new GetCustomer { Id = 1 });

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.Not.Null);
            Assert.That(response.Result.Name, Is.EqualTo("Hello"));
        }

        [Test]
        public void CanCreateCustomer()
        {
            var service = appHost.Container.Resolve<CustomerService>();

            var response = service.Post(new CreateCustomer { Name = "World" });
            Assert.That(response.Result, Is.Not.Null);
            Assert.That(response.Result.Id, Is.GreaterThan(0));
            Assert.That(response.Result.Name, Is.EqualTo("World"));

            var validateResponse = service.Get(new GetCustomer { Id = response.Result.Id });
            Assert.That(validateResponse.Result, Is.Not.Null);
            Assert.That(validateResponse.Result.Id, Is.EqualTo(response.Result.Id));
            Assert.That(validateResponse.Result.Name, Is.EqualTo(response.Result.Name));
        }

        [Test]
        public void CanUpdateCustomer()
        {
            var service = appHost.Container.Resolve<CustomerService>();

            var response = service.Get(new GetCustomers());
            var updateResponse = service.Put(new UpdateCustomer
            {
                Id = response.Results[0].Id,
                Name = "Updated Name"
            });

            Assert.That(updateResponse, Is.Not.Null);
            Assert.That(updateResponse.Result, Is.Not.Null);
            Assert.That(updateResponse.Result.Name, Is.EqualTo("Updated Name"));

            var validateResponse = service.Get(new GetCustomer
            {
                Id = response.Results[0].Id
            });

            Assert.That(validateResponse, Is.Not.Null);
            Assert.That(validateResponse.Result, Is.Not.Null);
            Assert.That(validateResponse.Result.Name, Is.EqualTo("Updated Name"));
        }

        [Test]
        public void CanDeleteCustomer()
        {
            var service = appHost.Container.Resolve<CustomerService>();

            var response = service.Post(new CreateCustomer { Name = "Delete me" });
            Assert.That(response.Result, Is.Not.Null);
            Assert.That(response.Result.Id, Is.GreaterThan(0));
            Assert.That(response.Result.Name, Is.EqualTo("Delete me"));

            var createValidate = service.Get(new GetCustomer { Id = response.Result.Id });
            Assert.That(createValidate.Result, Is.Not.Null);
            Assert.That(createValidate.Result.Id, Is.EqualTo(response.Result.Id));
            Assert.That(createValidate.Result.Name, Is.EqualTo("Delete me"));

            service.Delete(new DeleteCustomer { Id = response.Result.Id });

            bool notFound = false;
            try
            {
                service.Get(new GetCustomer { Id = response.Result.Id });
            }
            catch (HttpError e)
            {
                notFound = true;
                Assert.That(e.Status, Is.EqualTo(404));
            }
            Assert.That(notFound, Is.EqualTo(true));
        }
    }
}
