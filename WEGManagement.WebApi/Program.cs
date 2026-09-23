
using System.Text.Json.Serialization;
using WEGManagement.Domain.Enums;
using WEGManagement.Domain.Repositories;
using WEGManagement.Infrastructure.Persistance;
using WEGManagement.Infrastructure.Repositories;

namespace WEGManagement.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
            builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
            builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
            builder.Services.AddScoped<ICondoFeeRepository, CondoFeeRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IRepairOrderRepository, RepairOrderRepository>();

            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddSqlServer<WegDbContext>(connectionString);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<ApartmentStatus>());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<PaymentStatus>());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<RepairOrderStatus>());
                options.JsonSerializerOptions.ReferenceHandler =
                    ReferenceHandler.IgnoreCycles;
            }); ;
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
