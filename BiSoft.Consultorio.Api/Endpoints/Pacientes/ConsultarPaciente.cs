using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class ConsultarPaciente
    {
        private const string ENDPOINT_NAME = "Consultar Paciente";

        public static RouteGroupBuilder MapConsultarPacienteEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{pacienteId}",
                    async (
                        [FromRoute] Guid pacienteId,
                        PacienteService pacienteService,
                        CancellationToken ct
                    ) =>
                    {
                        var paciente = await pacienteService.ConsultorPaciente(pacienteId);
                        return Results.Ok(paciente);
                    }
                )
                .Produces<ConsultarPacienteResponse>(StatusCodes.Status200OK)
                .WithDescription("Consulta la información de un paciente con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
