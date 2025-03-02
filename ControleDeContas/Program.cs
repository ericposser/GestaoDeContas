using ControleDeContas.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Contexto>
                (options => options.UseSqlServer("Data Source=localhost;Initial Catalog=ControleDeContas;Integrated Security=False;User ID=sa;Password=99278605eeE;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False"));
                //(options => options.UseSqlServer("Server=servidoreric.database.windows.net;Database=ControleDeContas;User Id=eric;Password=99278605eeE;"));

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
                pattern: "{controller=Produto}/{action=Index}/{id?}");

            app.Run();
        }
    }
}