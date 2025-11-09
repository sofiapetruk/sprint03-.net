using Sprint02.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Sprint02.NovaPasta1 
{
    public class UsuarioRequestExample : IExamplesProvider<UsuarioRequestDto>
    {
        public UsuarioRequestDto GetExamples()
        {
            return new UsuarioRequestDto
            {
                Nome = "Alice Santos",

                Email = "alice.santos@dominio.com",

                Senha = "SenhaSegura123"
            };
        }
    }
}