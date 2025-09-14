using System.ComponentModel.DataAnnotations;

namespace Sprint02.DTOs
{
    public class StatusMotoRequestDto
    {
        [Required(ErrorMessage = "O status é obrigatório")]
        [StringLength(50, ErrorMessage = "O status deve ter no máximo 50 caracteres")]
        public string Status { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Data { get; set; }
    }
}
