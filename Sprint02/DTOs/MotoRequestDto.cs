using System.ComponentModel.DataAnnotations;

namespace Sprint02.DTOs
{
    public class MotoRequestDto
    {
        [Required(ErrorMessage = "O chassi é obrigatório")]
        [StringLength(50, ErrorMessage = "O chassi deve ter no máximo 50 caracteres")]
        public string NmChassi { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória")]
        [RegularExpression(@"^([A-Za-z]{3}-\d{4}|[A-Za-z]{3}\d[A-Za-z]\d{2})$",
            ErrorMessage = "Placa inválida (use ABC-1234 ou ABC1D23)")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "A unidade é obrigatória")]
        [StringLength(100, ErrorMessage = "A unidade deve ter no máximo 100 caracteres")]
        public string Unidade { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um IdStatus válido")]
        public int IdStatus { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um IdTipo válido")]
        public int IdTipo { get; set; }
    }
}
