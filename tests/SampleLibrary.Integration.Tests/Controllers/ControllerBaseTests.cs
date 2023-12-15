using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleLibrary.Api;
using SampleLibrary.Infra.Data.Context;
using System;
using System.Linq;
using System.Net.Http;

namespace SampleLibrary.Integration.Tests.Controllers
{
    public abstract class ControllerBaseTests
    {
        protected readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;
        private const string connectionString = "Data Source =.;database=SampleLibraryTests;Trusted_Connection=True;";

        public ControllerBaseTests()
        {
            var _webApplicationFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SampleLibraryContext>));
                    services.Remove(descriptor);
                    services.AddDbContext<SampleLibraryContext>(options => { options.UseSqlServer(connectionString); });
                });
            });

            _serviceProvider = _webApplicationFactory.Services;
            _httpClient = _webApplicationFactory.CreateClient();
        }

        public SampleLibraryContext GetContext()
        {
            return _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<SampleLibraryContext>();
        }

        protected void StartDatabase()
        {
            var sampleLibraryContext = GetContext();
            sampleLibraryContext.Database.Migrate();
        }

        protected virtual void SeedData(params object[] data)
        {
            var sampleLibraryContext = GetContext();
            sampleLibraryContext.AddRange(data);
            sampleLibraryContext.SaveChanges();
        }

        protected void ResetDatabase()
        {
            var sampleLibraryContext = GetContext();
            sampleLibraryContext.Database.ExecuteSqlRaw("delete book");
            sampleLibraryContext.Database.ExecuteSqlRaw("delete publisher");
            sampleLibraryContext.Database.ExecuteSqlRaw("delete author");
        }
    }
}
