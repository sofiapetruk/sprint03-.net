using Sprint02.Hateos;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.DTOs
{
    public class UsuarioResponseDto
    {
        [SwaggerSchema("Identificador único do usuário")]
        public long IdUsuario { get; set; }

        [SwaggerSchema("Nome completo do usuário")]
        public string Nome { get; set; }

        [SwaggerSchema("Email do usuário")]
        public string Email { get; set; }

        [SwaggerSchema("Links de navegação HATEOAS relacionados ao usuário")]
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
