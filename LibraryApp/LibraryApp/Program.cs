using LibraryApp.Models.Abstract;
using LibraryApp.Models.Concrete;
using LibraryApp.Models.Repositories;
using LibraryApp.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LibraryDbContext>(options=>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  //dbcontext servisini elave edir

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LibraryDbContext>().AddDefaultTokenProviders();
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<IBookTypeRepository, BookTypeRepository>();  
            builder.Services.AddScoped<IBookRepository, BookRepository>();  
            builder.Services.AddScoped<IRentRepository, RentRepository>();  
            builder.Services.AddScoped<IRentOrderRepository, RentOrderRepository>();  
            builder.Services.AddScoped<IEmailSender, EmailSender>();

          

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

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}