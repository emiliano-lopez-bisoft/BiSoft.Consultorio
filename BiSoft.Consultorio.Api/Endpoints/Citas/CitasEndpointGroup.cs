using BiSoft.Consultorio.Api.Endpoints.Pacientes;
using System.Runtime.CompilerServices;

namespace BiSoft.Consultorio.Api.Endpoints.Citas
{
    public static class CitasEndpointGroup
    {
        public static RouteGroupBuilder MapCitasEndpoints(this RouteGroupBuilder group)
        {
            var citasGroup = group.MapGroup("citas").WithTags("Citas");
            citasGroup.MapEndpoints();
            return group;
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarCitaEndpoint()
                 .MapRegistrarCitaEndpoint()
                 .MapEliminarCitaEndpoint()
                 .MapReagendarCitaEndpoint();
            return group;
        }
    }
}
