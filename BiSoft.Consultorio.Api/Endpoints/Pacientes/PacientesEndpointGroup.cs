namespace BiSoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class PacientesEndpointGroup
    {
        public static RouteGroupBuilder MapPacientesEndpoints(this RouteGroupBuilder group)
        {
            var pacientesGroup = group.MapGroup("pacientes").WithTags("Pacientes");
            pacientesGroup.MapEndpoints();
            return pacientesGroup;
        }

        public static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarPacienteEndpoint()
                 .MapRegistrarPacienteEndpoint()
                 .MapActualizarPacienteEndpoint()
                 .MapEliminarPacienteEndpoint();
            return group;
        }
    }
}
