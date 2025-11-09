using Sprint02.DTOs;

namespace Sprint02.repositoy
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto> FindByEmail(string email);
    }
}
