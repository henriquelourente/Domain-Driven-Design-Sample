using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SampleLibrary.IoC
{
    public static class ApplicationBuilderExtensions
    {
        private static IConsumerSubscriptions _consumerSubscriptions;

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app, IConsumerSubscriptions consumerSubscriptions)
        {
            _consumerSubscriptions = consumerSubscriptions;

            var life = app.ApplicationServices.GetService<IApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            _consumerSubscriptions.Subscribe();
        }

        private static void OnStopping()
        {
            _consumerSubscriptions.Unsubscribe();
        }
    }
}
