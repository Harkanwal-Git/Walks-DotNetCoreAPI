
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Walks.API.Data;
using Walks.API.Mappings;
using Walks.API.Repositories;

namespace WalksAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<WalksDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("WalksConnectionString")));

            builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
            builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfiles>());
           // builder.Services.AddAutoMapper(cfg => { },typeof(AutoMapperProfiles));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
