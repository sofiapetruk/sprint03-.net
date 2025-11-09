using Sprint02.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Sprint02.NovaPasta1
{
    public class MotoRequestExample : IExamplesProvider<MotoRequestDto>
    {
        public MotoRequestDto GetExamples()
        {
            return new MotoRequestDto
            {
                NmChassi = "AB1234567890CDEF",
                Placa = "ufm-0620",
                Unidade = "Unidade de Osaco",
                IdStatus = 1,
                IdTipo = 2
            };
        }
    }
}