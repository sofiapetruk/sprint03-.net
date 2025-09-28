using Sprint02.Hateos;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.DTOs
{
    public class TipoMotoResponseDto
    {
        [SwaggerSchema("Identificador único do tipo de moto")]
        public int IdTipo { get; set; }

        [SwaggerSchema("Nome do tipo de moto")]
        public string NomeTipo { get; set; }

        [SwaggerSchema("Links de navegação HATEOAS relacionados ao tipo de moto")]
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
