using System.Diagnostics;
using NUnit.Framework;
using Memcached.ServiceModel;
using ServiceStack;

namespace Memcached.Tests
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
            appHost.Dispose();
        }

        [Test]
        public void TestCanGetCustomer()
        {
            JsonServiceClient client = new JsonServiceClient("http://localhost:2337/");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var response = client.Get(new GetCustomer { Id = 1 });
            stopwatch.Stop();
            var nonCacheTime = stopwatch.ElapsedTicks;
            stopwatch.Reset();
            stopwatch.Start();
            var cachedResponse = client.Get(new GetCustomer { Id = 1 });
            stopwatch.Stop();
            var cachedTime = stopwatch.ElapsedTicks;

            Assert.That(response.Result, Is.Not.Null);
            Assert.That(response.Result.Orders.Count, Is.EqualTo(5));

            Assert.That(cachedResponse.Result, Is.Not.Null);
            Assert.That(cachedResponse.Result.Orders.Count, Is.EqualTo(5));

            Assert.That(cachedTime, Is.LessThan(nonCacheTime));
        }

        [Test]
        public void TestCanUpdateCustomer()
        {
            JsonServiceClient client = new JsonServiceClient("http://localhost:2337/");
            //Force cache
            client.Get(new GetCustomer { Id = 1 });
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var cachedResponse = client.Get(new GetCustomer { Id = 1 });
            stopwatch.Stop();
            var cachedTime = stopwatch.ElapsedTicks;
            stopwatch.Reset();

            client.Put(new UpdateCustomer { Id = 1, Name = "Johno" });

            stopwatch.Start();
            var nonCachedResponse = client.Get(new GetCustomer { Id = 1 });
            stopwatch.Stop();
            var nonCacheTime = stopwatch.ElapsedTicks;

            Assert.That(cachedResponse.Result, Is.Not.Null);
            Assert.That(cachedResponse.Result.Orders.Count, Is.EqualTo(5));

            Assert.That(nonCachedResponse.Result, Is.Not.Null);
            Assert.That(nonCachedResponse.Result.Orders.Count, Is.EqualTo(5));
            Assert.That(nonCachedResponse.Result.Name, Is.EqualTo("Johno"));

            Assert.That(cachedTime, Is.LessThan(nonCacheTime));
        }
    }
}
