using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Sprint02.Models
{
    [Table("T_MOTO")]
    public class Moto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMoto { get; set; }

        [Required]
        [Column("nm_chassi")]
        public string NmChassi { get; set; }

        [Required]
        [Column("placa")]
        public string Placa { get; set; }

        [Required]
        [Column("unidade")]
        public string Unidade { get; set; }

        [ForeignKey("StatusMoto")]
        public int IdStatus { get; set; }
        public StatusMoto status { get; set; }

        [ForeignKey("TipoMoto")]
        public int IdTipo { get; set; }
        public TipoMoto modelo { get; set; }

        public Moto() { }

        public Moto(string nmChassi, string placa, string unidade, int idStatus, int idTipo)
        {
            NmChassi = nmChassi;
            Placa = placa;
            Unidade = unidade;
            IdStatus = idStatus;
            IdTipo = idTipo;
        }
    }
}
