using Microsoft.AspNetCore.Mvc;
using Sprint02.NovaPasta;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Sprint02.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PrevisaoController : ControllerBase
    {
        private readonly TreinamentoService _treinamento;

        public PrevisaoController(TreinamentoService treinamentoService)
        {
            _treinamento = treinamentoService;
        }

        [HttpPost]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Realiza previsão de moto recomendada",
            Description = "Utiliza um modelo de Machine Learning para prever qual moto (CÓD. 0 ou CÓD. 1) é mais adequada com base nos dados fornecidos."
        )]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PreverMoto([FromBody] DadosMotos dados)
        {
            var resultadoPrevisao = _treinamento.Predict(dados);

            string motoRecomendada = resultadoPrevisao.PrevisaoMoto ?
                "Mottu Sport ESD 2025 (CÓD. 1)" :
                "Mottu Sport 110i (CÓD. 0)";

            return Ok(new
            {
                ProbabilidadeESD2025 = Math.Round(resultadoPrevisao.Probabilidade, 4),

                DecisaoModelo = resultadoPrevisao.PrevisaoMoto,

                mensagem = $"Moto prevista: {motoRecomendada}"
            });
        }
    }
}
