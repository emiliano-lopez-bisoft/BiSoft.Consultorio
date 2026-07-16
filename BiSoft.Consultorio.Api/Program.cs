using BiSoft.Consultorio.Api.Extensions;
using BiSoft.Consultorio.Api.Middlewares;
using Serilog;

namespace BiSoft.Consultorio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddApplicationServices(builder.Configuration);

                Log.Logger = new LoggerConfiguration()
                             .WriteTo.SQLite(
                                sqliteDbPath: "Logs/Logs.db",
                                tableName: "Logs",
                                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                             )
                             .CreateLogger();
                //To do Agregar serilog

                //OpenApi
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // CORS
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
                });


                // Add services to the container.
                builder.Services.AddAuthorization();

                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                builder.Services.AddOpenApi();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }
                app.UseMiddleware<ErrorHandlerMiddleware>();
                app.UseHttpsRedirection();

                app.UseAuthorization();


                //CRUD DOCTOR y PACIENTE
                app.MapEndpoints();

                //OpenApi
                app.UseSwagger();
                app.UseSwaggerUI();

                //Cors 
                app.UseCors("AllowAll");

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Aplication start-up failed {ex.Message}");
                Console.ReadKey();
                Environment.Exit(1);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
