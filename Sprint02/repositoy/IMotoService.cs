using Sprint02.DTOs;
using Sprint02.Enuns;

namespace Sprint02.repositoy
{
    public interface IMotoService
    {
        Task<IEnumerable<MotoResponseDto>> GetByStatus(StatusEnum status);
    }
}
