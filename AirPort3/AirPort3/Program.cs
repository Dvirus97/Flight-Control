
using AirPort.BL;
using AirPort.DB;
using AirPort.DB.Models;
using AirPort.SignalR;
using AirPort3.BL;
using AirPort3.DB.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AirPort {
    public class Program {

        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var connectionString = builder.Configuration.GetConnectionString("FlightHistory");
            var connectionStringFix = connectionString.Replace("{AppDir}", Environment.CurrentDirectory);

            builder.Services.AddDbContext<AirPortContext>(o =>
                o.UseSqlite(connectionStringFix), ServiceLifetime.Singleton);
            builder.Services.AddSingleton<IRepository<FlightDb>, FlightHistoryRepo>();
            builder.Services.AddSingleton<AirPortLogic>();
            builder.Services.AddSingleton<RouteProvider>();


            RunNodeAndReact p = new RunNodeAndReact();
            p.StartProcessSimulator();
            p.StartProcessReact();

            builder.Services.AddCors(o => {
                o.AddDefaultPolicy(b => {
                    b.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002", "http://localhost:3003")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });


            builder.Services.AddSignalR();


            var app = builder.Build();


            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<AirPortHub>("/flight");

            app.Run();
        }
    }
}