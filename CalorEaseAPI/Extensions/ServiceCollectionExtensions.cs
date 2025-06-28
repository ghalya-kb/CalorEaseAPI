using Business.Localization;
using Business.Validation;
using DataAccess.DbContext.EntityFrameworkCore;
using Entities;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Business.Mapping;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;

namespace CalorEaseAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
        public static IServiceCollection AddLocalizationServices(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en", "tr" };
                options.SetDefaultCulture("en")
                       .AddSupportedCultures(supportedCultures)
                       .AddSupportedUICultures(supportedCultures);

                options.ApplyCurrentCultureToResponseHeaders = true;
            });

            return services;
        }
        public static IServiceCollection AddJwtAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwt = configuration.GetSection("Jwt");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]))
                };
            });

            return services;
        }
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            })
            .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<UserProfileDtoValidator>();

            return services;
        }
        public static IServiceCollection AddMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            return services;
        }
        public static IServiceCollection AddBusinessServicesDI(this IServiceCollection services)
        {
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IUserProfileService, UserProfileManager>();

            return services;
        }
        public static IServiceCollection AddDataAccessServicesDI(this IServiceCollection services)
        {
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();

            return services;
        }
    }
}
