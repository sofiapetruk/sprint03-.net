using Sprint02.Hateos;

namespace Sprint02.DTOs
{
    public class MotoResponseDto
    {

        public int IdMoto { get; set; }
        public string NmChassi { get; set; }
        public string Placa { get; set; }
        public string Unidade { get; set; }
        public int IdStatus { get; set; }
        public int IdTipo { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
