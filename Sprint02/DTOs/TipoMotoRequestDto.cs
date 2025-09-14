using System.ComponentModel.DataAnnotations;

namespace Sprint02.DTOs
{
    public class TipoMotoRequestDto
    {
        [Required(ErrorMessage = "O nome do tipo é obrigatório")]
        [StringLength(60, ErrorMessage = "O nome do tipo deve ter no máximo 60 caracteres")]
        public string NomeTipo { get; set; }
    }
}
