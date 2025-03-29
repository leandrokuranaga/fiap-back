using Fiap.Application.Contact.Services;
using Fiap.Domain.ContactAggregate;
using Fiap.Domain.SeedWork;
using Fiap.Infra.Data;
using Fiap.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Infra.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void AddLocalHttpClients(this IServiceCollection services, IConfiguration configuration) { }

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
