using BiSoft.Consultorio.Api.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Doctores
{
    public static class RegistrarDoctor
    {
        private const string ENDPOINT_NAME = "Registrar Doctor";
        public static RouteGroupBuilder MapRegistrarDoctorEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("",
                    async (
                        [FromBody] RegistrarDoctorRequest request,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                     
                        var doctor = await doctorService.RegistrarDoctor(request.Nombre, request.Especialidad);
                        return Results.Created($"api/doctores/{doctor.Id}", doctor);
                    }
                )
                .Produces<RegistrarDoctorResponse>(StatusCodes.Status201Created)
                .WithDescription("Registra un nuevo doctor en el sistema.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
