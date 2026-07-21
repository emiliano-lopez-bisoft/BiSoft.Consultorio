namespace BiSoft.Consultorio.Api.Endpoints.Doctores
{
    public static class DoctoresEndpointGroup
    {
        public static RouteGroupBuilder MapDoctoresEndpoints(this RouteGroupBuilder group) 
        { 
            var doctoresGroup = group.MapGroup("doctores").WithTags("Doctores");
            doctoresGroup.MapEndpoints();
            return doctoresGroup;
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarDoctorEndpoint()
                 .MapRegistrarDoctorEndpoint()
                 .MapActualizarDoctorEndpoint()
                 .MapEliminarDoctorEndpoint()
                 .MapConsultarTodosLosDoctoresEndpoint();
            return group;
        }
    }
}
