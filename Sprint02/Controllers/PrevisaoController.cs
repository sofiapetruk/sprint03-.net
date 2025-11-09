using Microsoft.AspNetCore.Mvc;
using Sprint02.NovaPasta;

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
