using Sprint02.Hateos;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.DTOs
{
    public class StatusMotoResponseDto
    {
        [SwaggerSchema("Identificador único do status da moto")]
        public int IdStatus { get; set; }

        [SwaggerSchema("Qual é o status da moto")]
        public string Status { get; set; }

        [SwaggerSchema("Data e hora em que o status foi registrado")]
        public DateTime Data { get; set; }

        [SwaggerSchema("Links de navegação HATEOAS relacionados ao status da moto")]
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
