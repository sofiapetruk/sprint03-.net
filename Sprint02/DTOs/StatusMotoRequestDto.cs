using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.DTOs
{
    public class StatusMotoRequestDto
    {
        [Required(ErrorMessage = "O status é obrigatório")]
        [StringLength(50, ErrorMessage = "O status deve ter no máximo 50 caracteres")]
        [SwaggerSchema("Descrição do status da moto (ex: Disponível, Em manutenção, Alugada)")]
        public string Status { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        [SwaggerSchema("Data e hora em que o status foi registrado")]
        public DateTime Data { get; set; }
    }
}
