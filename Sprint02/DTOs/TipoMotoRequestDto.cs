using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.DTOs
{
    public class TipoMotoRequestDto
    {
        [Required(ErrorMessage = "O nome do tipo é obrigatório")]
        [StringLength(60, ErrorMessage = "O nome do tipo deve ter no máximo 60 caracteres")]
        [SwaggerSchema("Nome do tipo de moto")]
        public string NomeTipo { get; set; }
    }
}
