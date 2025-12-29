using Blockchain.Infrastructure.Persistence;
using Blockchain.TestInfrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blockchain.FunctionalTests;

public sealed class CustomWebApplicationFactory(PostgresFixture fixture) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {   
        builder.ConfigureServices(services =>
        {
            var descriptor = services
                .Single(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(fixture.ConnectionString));
        });
    }
}
