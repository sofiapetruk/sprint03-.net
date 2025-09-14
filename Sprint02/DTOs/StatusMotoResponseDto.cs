using Sprint02.Hateos;

namespace Sprint02.DTOs
{
    public class StatusMotoResponseDto
    {
        public int IdStatus { get; set; }
        public string Status { get; set; }
        public DateTime Data { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
