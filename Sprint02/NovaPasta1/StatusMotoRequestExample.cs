using Sprint02.DTOs;
using Sprint02.Enuns;
using Swashbuckle.AspNetCore.Filters;

namespace Sprint02.NovaPasta1 
{
    public class StatusMotoRequestExample : IExamplesProvider<StatusMotoRequestDto>
    {
        public StatusMotoRequestDto GetExamples()
        {
            return new StatusMotoRequestDto
            {
                Status = StatusEnum.ALUGADA,

                Data = DateTime.Now
            };
        }
    }
}