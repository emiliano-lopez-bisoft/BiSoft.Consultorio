using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BiSoft.Consultorio.Api.Endpoints.Doctores
{
    public static class ConsultarDoctor
    {
        private const string ENDPOINT_NAME = "Consultar Doctor";
        public static RouteGroupBuilder MapConsultarDoctorEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                        var doctor = await doctorService.ConsultarDoctor(doctorId);
                        return Results.Ok(doctor);
                    }
                )
                .Produces<ConsultarDoctorResponse>(StatusCodes.Status200OK)
                .WithDescription("Consulta la información de un doctor con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
