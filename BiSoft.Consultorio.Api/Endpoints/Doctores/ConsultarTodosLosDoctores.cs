using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Doctores
{
    public static class ConsultarTodosLosDoctores
    {
        public static RouteGroupBuilder MapConsultarTodosLosDoctoresEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("", async ([FromServices] DoctorService doctorService, CancellationToken ct) =>
            {
                var doctores = await doctorService.ConsultarTodosLosDoctores();
                return Results.Ok(doctores);
            })
            .Produces<List<ConsultarDoctorResponse>>(StatusCodes.Status200OK)
            .WithDescription("Obtiene la lista de todos los doctores activos en el sistema.")
            .WithSummary("Consultar Todos los Doctores")
            .WithName("ConsultarTodosLosDoctores");

            return group;
        }
    }
}
