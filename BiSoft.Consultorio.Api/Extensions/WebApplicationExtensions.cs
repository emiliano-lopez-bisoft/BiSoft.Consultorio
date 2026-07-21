using BiSoft.Consultorio.Api.Endpoints.Citas;
using BiSoft.Consultorio.Api.Endpoints.Doctores;
using BiSoft.Consultorio.Api.Endpoints.Pacientes;
using BiSoft.Consultorio.Api.Endpoints.Salas;
using Microsoft.OpenApi;

namespace BiSoft.Consultorio.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            var apiEndpoints = app.MapGroup("api").RequireRateLimiting(Program.RATE_LIMITER_POLICY_NAME); ;
            apiEndpoints.MapDoctoresEndpoints();
            apiEndpoints.MapPacientesEndpoints();
            apiEndpoints.MapSalasEndpoints();
            apiEndpoints.MapCitasEndpoints();
            apiEndpoints.AddOpenApi();
            return app;
        }
        private static RouteGroupBuilder AddOpenApi(this RouteGroupBuilder group)
        {
            return group.AddOpenApiOperationTransformer((options, context, ct) =>
            {
                options.Responses ??= new OpenApiResponses();
                options.Responses["400"] = new OpenApiResponse { Description = "Solicitud incorrecta" };
                options.Responses["404"] = new OpenApiResponse { Description = "No encontrado" };
                options.Responses["500"] = new OpenApiResponse { Description = "Error interno del servidor" };
                return Task.CompletedTask;
            });
        }
    }
}
