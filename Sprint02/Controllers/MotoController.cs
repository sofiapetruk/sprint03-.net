using Microsoft.AspNetCore.Mvc;
using Sprint02.DTOs;
using Sprint02.Hateos;
using Sprint02.Service;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MotoController : Controller
    {
        private readonly IService<MotoResponseDto, MotoRequestDto> _service;
        private readonly LinkGenerator _linkGenerator;

        public MotoController(IService<MotoResponseDto, MotoRequestDto> service, LinkGenerator linkGenerator)
        {
            _service = service;
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar uma nova mota",
            Description = "Cadastra uma nova moto informando chassi, placa, unidade, status e tipo"
            )]
        [ProducesResponseType(typeof(MotoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MotoResponseDto>> Create([FromBody] MotoRequestDto dto)
        {
            var moto = await _service.Save(dto);

            AddLinks(moto);

            return CreatedAtAction(nameof(ReadById), new { id = moto.IdMoto }, moto);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar todas as motos",
            Description = "Retorna todas as motos cadastradas, com suporte a paginação"
            )]
        [ProducesResponseType(typeof(IEnumerable<MotoResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MotoResponseDto>>> ReadAll(
           [FromQuery, SwaggerParameter("Número da página que deseja consultar (padrão = 1)")] int page = 1,
           [FromQuery, SwaggerParameter("Quantidade de registros por página (padrão = 10)")] int pageSize = 10)
        {
            var motos = (await _service.GetAll(page, pageSize)).ToList();

            foreach (var m in motos)
            {
                AddLinks(m);
            }

            return Ok(motos);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(
            Summary = "Buscar moto por ID",
            Description = "Retorna os detalhes de uma moto específica pelo seu identificador único"
            )]
        [ProducesResponseType(typeof(MotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoResponseDto>> ReadById([FromRoute, SwaggerParameter("Identificador único da moto a ser buscada")] int id)
        {
            var moto = await _service.GetById(id);

            AddLinks(moto);

            return Ok(moto);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(
            Summary = "Atualizar moto",
            Description = "Atualiza as informações de uma moto existente"
        )]
        [ProducesResponseType(typeof(MotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoResponseDto>> Update([FromRoute] int id, [FromBody] MotoRequestDto dto)
        {
            var moto = await _service.Update(id, dto);

            AddLinks(moto);

            return Ok(moto);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(
            Summary = "Deletar moto",
            Description = "Exclui permanentemente uma moto do sistema pelo seu identificador"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteById(id);
            return NoContent();
        }

        private void AddLinks(MotoResponseDto moto)
        {
            moto.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "Moto", new { id = moto.IdMoto }),
                "self",
                "GET"
            ));
            moto.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(Update), "Moto", new { id = moto.IdMoto }),
                "update",
                "PUT"
            ));
            moto.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(Delete), "Moto", new { id = moto.IdMoto }),
                "delete",
                "DELETE"
            ));
            moto.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadAll), "Moto"),
                "collection",
                "GET"
            ));
        }
    }
}
