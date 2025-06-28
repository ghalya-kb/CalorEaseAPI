using CalorEaseAPI.Middlewares;
using Microsoft.Extensions.Options;
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
