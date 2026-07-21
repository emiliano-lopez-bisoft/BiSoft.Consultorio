namespace BiSoft.Consultorio.Api.Endpoints.Salas
{
    public static class SalasEndpointGroup
    {
        public static RouteGroupBuilder MapSalasEndpoints(this RouteGroupBuilder group)
        {
            var salasGroup = group.MapGroup("salas").WithTags("Salas");
            salasGroup.MapEndpoints();
            return salasGroup;
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarSalaEndpoint()
                 .MapRegistrarSalaEndpoint()
                 .MapActualizarSalaEndpoint()
                 .MapEliminarSalaEndpoint()
                 .MapConsultarTodasLasSalasEndpoint();
            return group;
        }
    }
}
