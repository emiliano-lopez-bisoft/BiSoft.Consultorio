namespace BiSoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class UsuariosEndpointGroup
    {
        public static RouteGroupBuilder MapUsuariosEndpoints(this RouteGroupBuilder group)
        {
            var usuariosGroup = group.MapGroup("usuarios").WithTags("Usuarios");
            usuariosGroup.MapEndpoints();
            return usuariosGroup;
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapActualizarPassword()
                 .MapActualizarUsuario()
                 .MapEliminarUsuario()
                 .MapRegistrarUsuario();
            return group;
        }
    }
}
