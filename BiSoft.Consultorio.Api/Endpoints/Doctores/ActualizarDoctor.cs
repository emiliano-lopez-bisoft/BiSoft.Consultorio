using BiSoft.Consultorio.Api.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Doctores
{
    public static class ActualizarDoctor
    {
        private const string ENDPOINT_NAME = "Actualizar Doctor";

        public static RouteGroupBuilder MapActualizarDoctorEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        [FromBody] ActualizarDoctorRequest request,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {

                        var doctor = await doctorService.ActualizarDoctor(doctorId, request.Nombre, request.Especialidad);
                        return Results.Ok(doctor);
                    }
                )
                .Produces<ActualizarDoctorResponse>(StatusCodes.Status200OK)
                .WithDescription("Actualiza la información de un doctor con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
