using Sprint02.Hateos;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.DTOs
{
    public class MotoResponseDto
    {
        [SwaggerSchema("Identificador único da moto")]
        public int IdMoto { get; set; }

        [SwaggerSchema("Número do chassi da moto")]
        public string NmChassi { get; set; }

        [SwaggerSchema("Placa da moto")]
        public string Placa { get; set; }

        [SwaggerSchema("Unidade ou filial onde a moto está vinculada")]
        public string Unidade { get; set; }

        [SwaggerSchema("Identificador do status atual da moto")]
        public int IdStatus { get; set; }

        [SwaggerSchema("Identificador do tipo da moto")]
        public int IdTipo { get; set; }

        [SwaggerSchema("Links de navegação HATEOAS relacionados à moto")]
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
