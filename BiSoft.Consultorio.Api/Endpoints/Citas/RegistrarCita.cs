using BiSoft.Consultorio.Api.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Citas
{
    public static class RegistrarCita
    {
        private const string ENDPOINT_NAME = "Registrar Cita";
        public static RouteGroupBuilder MapRegistrarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("",
                    async (
                        [FromBody] RegistrarCitaRequest request,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var cita = await citaService.RegistrarCita(
                            request.Fecha,
                            request.PacienteId,
                            request.DoctorId,
                            request.SalaId,
                            request.Motivo);

                        return Results.Created($"api/citas/{cita.Id}", cita);
                    }
                )
                .Produces<RegistrarCitaResponse>(StatusCodes.Status201Created)
                .WithDescription("Registra una nueva cita en el sistema validando empalmes y horarios.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
