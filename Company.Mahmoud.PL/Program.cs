using Company.Mahmoud.DAL.Data.Context;
using Company.Mahmoud.PL.Controllers;
using Company.Mahmoud.PLL.Interfaces;
using Company.Mahmoud.PLL.Repositry;
using Company.PLL.Interfaces;
using Company.PLL.Repositry;
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
           
            builder.Services.AddScoped<IDepartmentRepositry, DepartmentRepositry>();
            builder.Services.AddScoped<IEmployeeRepositry, EmployeeRepositry>();
            builder.Services.AddDbContext<CompanyDbContextcs>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
