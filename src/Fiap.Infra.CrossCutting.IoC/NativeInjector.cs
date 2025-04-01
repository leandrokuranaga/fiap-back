using Fiap.Application.Contact.Services;
using Fiap.Domain.ContactAggregate;
using Fiap.Domain.SeedWork;
using Fiap.Infra.CrossCutting.Http.ViaCEP.Services;
using Fiap.Infra.Data;
using Fiap.Infra.Data.Repositories;
using Fiap.Infra.Utils.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fiap.Infra.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void AddLocalHttpClients(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<AppSettings>(configuration.GetSection("URLBrasilAPI"));

            services.AddHttpClient<IBrasilAPIService, BrasilAPIService>((provider, client) =>
            {
                var settings = provider.GetRequiredService<IOptions<AppSettings>>().Value;

                if (string.IsNullOrEmpty(settings.URLBrasilAPI))
                {
                    throw new InvalidOperationException();
                }

                client.BaseAddress = new Uri(settings.URLBrasilAPI.TrimEnd('/'));
                client.Timeout = TimeSpan.FromSeconds(30);

                Console.WriteLine($"BaseAddress configurado para: {client.BaseAddress}");
            });
        }

        public static void AddLocalServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotification, Notification>();

            #region Repositories
            services.AddScoped<IContactRepository, ContactRepository>();
            #endregion

            #region Services
            services.AddScoped<IContactService, ContactService>();

            #endregion
        }

    }
}
