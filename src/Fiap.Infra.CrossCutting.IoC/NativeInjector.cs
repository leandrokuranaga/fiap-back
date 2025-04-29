using Fiap.Application.Auth.Models;
using Fiap.Application.Auth.Services;
using Fiap.Application.Games.Services;
using Fiap.Application.Promotions.Services;
using Fiap.Application.User.Services;
using Fiap.Application.Users.Services;
using Fiap.Domain.Game;
using Fiap.Domain.Promotion;
using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate;
using Fiap.Infra.Data;
using Fiap.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddScoped<IAuthService, AuthService>();

            #endregion
        }

        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration["JwtSettings:SecretKey"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings))
                };
            });

            services.AddAuthorization();
        }

    }
}
