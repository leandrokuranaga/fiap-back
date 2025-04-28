using Fiap.Application.Games.Services;
using Fiap.Application.Promotions.Services;
using Fiap.Application.User.Services;
using Fiap.Application.Users.Services;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate;
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            #endregion

            #region Services
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<IPromotionsService, PromotionsService>();

            #endregion
        }

    }
}
