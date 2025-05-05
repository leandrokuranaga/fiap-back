using Fiap.Application.Games.Services;
using Fiap.Application.Library.Services;
using Fiap.Application.Promotions.Services;
using Fiap.Application.Users.Services;
using Fiap.Domain.Game;
using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.Promotion;
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
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            #endregion

            #region Services
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<IPromotionsService, PromotionsService>();
            services.AddScoped<ILibraryService, LibraryService>();

            #endregion
        }

    }
}
