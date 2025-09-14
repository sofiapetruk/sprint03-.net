using Sprint02.Hateos;

namespace Sprint02.DTOs
{
    public class TipoMotoResponseDto
    {
        public int IdTipo { get; set; }
        public string NomeTipo { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
