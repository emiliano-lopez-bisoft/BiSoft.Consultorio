using BiSoft.Consultorio.Api.Endpoints.Pacientes;

namespace BiSoft.Consultorio.Api.Endpoints.Security
{
    public static class SecurityEndpointGroup
    {
        public static RouteGroupBuilder MapSecurityEndpoints(this RouteGroupBuilder group)
        {
            var endpointGroup = group.MapGroup("auth").WithTags("Seguridad");
            endpointGroup.MapEndpoints();
            return endpointGroup;
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapLogin();
            return group;
        }
    }
}
