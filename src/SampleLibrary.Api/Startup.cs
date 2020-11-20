using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SampleLibrary.IoC;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SampleLibrary.Api
{
    public class Startup
    {
        private IConsumerSubscriptions _consumerSubscriptions;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sample Library ",
                    Version = "v1",
                });
            });

            services.AddControllers();
            services.AddDbContext(Configuration);
            services.Register();

            var serviceProvider = services.BuildServiceProvider();

            _consumerSubscriptions = (IConsumerSubscriptions)serviceProvider.GetRequiredService(typeof(IConsumerSubscriptions));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleLibrary");
                c.DocExpansion(DocExpansion.None);
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRabbitListener(_consumerSubscriptions);
        }
    }
}
