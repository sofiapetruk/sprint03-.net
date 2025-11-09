using Sprint02.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Sprint02.NovaPasta1 
{
    public class TipoMotoRequestExample : IExamplesProvider<TipoMotoRequestDto>
    {
        public TipoMotoRequestDto GetExamples()
        {
            return new TipoMotoRequestDto
            {
                NomeTipo = "Mottu Sport ESD 2025"
            };
        }
    }
}