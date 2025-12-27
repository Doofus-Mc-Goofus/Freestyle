using MediaStream.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace MediaStream
{
    public class Program
    {
        public static string serverVersion = "0.pre.1-pre";
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            _ = builder.Services.AddControllers();
            _ = builder.Services.AddRazorPages();
            _ = builder.Services.AddHttpContextAccessor();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            _ = builder.Services.AddEndpointsApiExplorer();
            _ = builder.Services.AddSwaggerGen();
            _ = builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
            });

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI();
            }
            _ = app.UseExceptionHandler("/Error");
            _ = app.UseHsts();
            _ = app.UseStatusCodePages();
            _ = app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(builder.Environment.ContentRootPath, "wwwroot\\css")),
                RequestPath = "/css"
            });
            _ = app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(builder.Environment.ContentRootPath, "wwwroot\\img")),
                RequestPath = "/img"
            });
            _ = app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(builder.Environment.ContentRootPath, "wwwroot\\js")),
                RequestPath = "/js"
            });
            _ = app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(builder.Environment.ContentRootPath, "wwwroot\\html")),
                RequestPath = "/html"
            });
            _ = app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 403)
                {
                    context.Request.Path = "/unauthorized";
                    await next();
                }
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/not_found";
                    await next();
                }
            });
            _ = app.UseRouting();

            _ = app.UseAuthorization();

            _ = app.MapControllers();

            _ = app.MapRazorPages();

            app.Run();
        }
    }
}
