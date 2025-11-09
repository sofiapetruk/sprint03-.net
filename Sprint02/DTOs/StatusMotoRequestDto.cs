using System.ComponentModel.DataAnnotations;
using Sprint02.Enuns;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.DTOs
{
    public class StatusMotoRequestDto
    {
        [Required(ErrorMessage = "O status é obrigatório")]
        [SwaggerSchema("Descrição do status da moto (ex: Disponível, Em manutenção, Alugada)")]
        public StatusEnum Status { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        [SwaggerSchema("Data e hora em que o status foi registrado")]
        public DateTime Data { get; set; }
    }
}
