using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Doctores
{
    public static class EliminarDoctor
    {
        private const string ENDPOINT_NAME = "Eliminar Doctor";

        public static RouteGroupBuilder MapEliminarDoctorEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                        await doctorService.EliminarDoctor(doctorId);
                        return Results.NoContent();
                    }
                )
                .Produces<EliminarDoctorResponse>(StatusCodes.Status204NoContent)
                .WithDescription("Elimina un doctor con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
