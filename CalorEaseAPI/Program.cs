
using Business.Abstract;
using Business.Concrete;
using Business.Localization;
using Business.Mapping;
using Business.Validation;
using CalorEaseAPI.Middlewares;
using DataAccess.DbContext.EntityFrameworkCore;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using CalorEaseAPI.Extensions;


namespace CalorEaseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContextServices(builder.Configuration);

            builder.Services.AddIdentityServices();

            builder.Services.AddLocalizationServices();

            builder.Services.AddJwtAuthenticationServices(builder.Configuration);

            builder.Services.AddValidationServices();

            builder.Services.AddMapperServices();

            builder.Services.AddBusinessServicesDI();

            builder.Services.AddDataAccessServicesDI();

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            app.MapControllers();
            app.Run();
        }
    }
}
