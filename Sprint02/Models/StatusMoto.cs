using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sprint02.Models
{
    [Table("T_STATUS_MOTO")]
    public class StatusMoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdStatus { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; }

        [Required]
        [Column("data")]
        public DateTime Data { get; set; }  

        public ICollection<Moto> Motos { get; set; }

        public StatusMoto() {}
        public StatusMoto(int idStatus, string status, DateTime data)
        {
            IdStatus = idStatus;
            Status = status;
            Data = data;
            Motos = new List<Moto>();
        }
    }
}
