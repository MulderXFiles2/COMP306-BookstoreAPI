using BookstoreApi.Data;
using BookstoreApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace BookstoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

       
            var connectionString = builder.Configuration.GetConnectionString("BookstoreConnection")
                ?? throw new InvalidOperationException("Connection string 'BookstoreConnection' not found.");

            builder.Services.AddDbContext<BookstoreContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    sqlOptions =>
                    {
                        
                        sqlOptions.EnableRetryOnFailure();
                    }));

            
            builder.Services.AddScoped<IBookstoreRepository, BookstoreRepository>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

          
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            // React fallback for client-side routing
            app.MapFallback(async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(
                    Path.Combine(app.Environment.ContentRootPath, "bookstore-frontend", "build", "index.html")
                );
            });

            app.Run();
        }
    }
}
