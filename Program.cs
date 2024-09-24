using st10269378.Services;
using Microsoft.Extensions.DependencyInjection;

// main program for the web application
namespace st10269378
{
    public class Program
    {
        // main entry point for the web application
        public static void Main(string[] args)
        {
            // creates a new web application builder
            var builder = WebApplication.CreateBuilder(args);

            // adds controllers with views to the services container
            builder.Services.AddControllersWithViews();

            // registers custom services as singletons
            builder.Services.AddSingleton<BlobService>();
            builder.Services.AddSingleton<TableService>();
            builder.Services.AddSingleton<QueueService>();
            builder.Services.AddSingleton<FileService>();

            // builds the web application
            var app = builder.Build();

            // configures the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                // uses exception handling for non-development environments
                app.UseExceptionHandler("/Home/Error");
                // uses HSTS for HTTPS security
                app.UseHsts();
            }

            // enables HTTPS redirection
            app.UseHttpsRedirection();
            // enables static file support
            app.UseStaticFiles();

            // enables routing support
            app.UseRouting();

            // enables authorization support
            app.UseAuthorization();

            // maps the default controller route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // runs the web application
            app.Run();
        }
    }
}