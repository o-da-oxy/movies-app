using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoviesApp.Data;
using MoviesApp.Middleware;
using MoviesApp.Models;
using MoviesApp.Services;
using MoviesApp.Services.ActorServices;

namespace MoviesApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<MoviesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MoviesApp")));
            
            //добавляем идентификацию
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<MoviesContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            
            //Подключаем AutoMapper
            services.AddAutoMapper(typeof(Startup));
            
            //регистрируем сервисы
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IActorService, ActorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //настр конвейер http запрса
        {
            if (env.IsDevelopment())
            {
                //получить полный лог об ошибке, если запускаем веб приложение локально(из Development)
                app.UseRequestLog(); //мидлвэар!!!
                app.UseDeveloperExceptionPage();
            }

            //порядок ниже важен! определяет, как они выстроятся в пайплайне
            
            app.UseHttpsRedirection(); //чтобы было https
            app.UseStaticFiles(); //обработка статических файлов из папки wwwroot
            app.UseCookiePolicy();

            app.UseRouting(); //роутинг, чтоб работала маршрутизация

            app.UseAuthentication();
            app.UseAuthorization();
            
            
            IList<CultureInfo> supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("ru-RU")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //правило роутинга (маршрутизация)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id:int?}"); //сегменты
            });
        }
    }
}