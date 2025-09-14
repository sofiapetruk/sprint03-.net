using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint02.Models
{
    [Table("T_TIPO_MOTO")]
    public class TipoMoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipo { get; set; }

        [Required]
        [Column("nome_tipo")]
        public String NomeTipo { get; set; }

        public ICollection<Moto> Motos { get; set; }

        public TipoMoto() {}

        public TipoMoto(int idTipo, string nomeTipo)
        {
            IdTipo = idTipo;
            NomeTipo = nomeTipo;
            Motos = new List<Moto>(); 
        }
    }
}
