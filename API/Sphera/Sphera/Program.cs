
using Microsoft.EntityFrameworkCore;
using Sphera.Repository;

namespace Sphera
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Ability to change repository
            RegisterEntityFrameworkRepository(builder);
            //RegisterDapperRepository(builder);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "Default",
                                  policy =>
                                  {
                                      policy
                                      .AllowAnyMethod()
                                      .AllowAnyOrigin()
                                      .AllowAnyHeader();
                                  });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("Default");

            app.MapControllers();

            app.Run();
        }

        private static void RegisterEntityFrameworkRepository(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContextPool<Repository.EntityFramework.AdventureWorksContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksConnectionString")));

            builder.Services.AddScoped<Repository.EntityFramework.AdventureWorksContext>();
            builder.Services.AddScoped<ISalesRepository, Repository.EntityFramework.SalesRepository>();
        }

        private static void RegisterDapperRepository(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISalesRepository, Repository.Dapper.SalesRepository>();
        }
    }
}
