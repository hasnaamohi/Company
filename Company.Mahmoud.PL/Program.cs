using Company.DAL.Models;
using Company.Mahmoud.DAL.Data.Context;
using Company.Mahmoud.PL.Controllers;
using Company.Mahmoud.PLL.Interfaces;
using Company.Mahmoud.PLL.Repositry;
using Company.PL.Mapping;
using Company.PL.Services;
using Company.PLL;
using Company.PLL.Interfaces;
using Company.PLL.Repositry;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Company.Mahmoud.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
           
           // builder.Services.AddScoped<IDepartmentRepositry, DepartmentRepositry>();
           // builder.Services.AddScoped<IEmployeeRepositry, EmployeeRepositry>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<CompanyDbContextcs>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));

            });
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddScoped<IScopedService, ScopedService>(); //per reqeust
            builder.Services.AddTransient<ITransentService,TransentService>(); //per operation
            builder.Services.AddSingleton<ISingeltonService, SingletonService>();  //per app
            builder.Services.AddIdentity<AppUsers, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContextcs>();
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
                
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
