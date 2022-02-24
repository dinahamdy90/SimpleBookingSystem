
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleBookingSystem.Infrastructure;
using System;

namespace SimpleBookingSystem.IntegrationTest
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public WebApplicationFactory<TStartup> ConfigureTest()
        {
            return this.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                {
                    var databaseName = Guid.NewGuid().ToString();

                    var descriptorDB =
                    new ServiceDescriptor(
                        typeof(SimpleBookingContext),
                        p => new SimpleBookingContext(new DbContextOptionsBuilder<SimpleBookingContext>().UseInMemoryDatabase(databaseName).Options),
                            ServiceLifetime.Singleton);
                    services.Replace(descriptorDB);

                    using var scope = services.BuildServiceProvider().CreateScope();
                    var db = scope
                        .ServiceProvider
                        .GetRequiredService<SimpleBookingContext>();

                    if (db.Database.EnsureCreated())
                    {
                        db.InitializeTestDatabase();
                    }
                })
            );
        }
    }
}