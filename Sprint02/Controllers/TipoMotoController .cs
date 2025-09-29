using Microsoft.AspNetCore.Mvc;
using Sprint02.DTOs;
using Sprint02.Hateos;
using Sprint02.Service;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.Controllers
{
    [ApiController]
    [Route("api/tipo-moto")]
    public class TipoMotoController : ControllerBase
    {
        private readonly IService<TipoMotoResponseDto, TipoMotoRequestDto> _service;
        private readonly LinkGenerator _linkGenerator;

        public TipoMotoController(
            IService<TipoMotoResponseDto, TipoMotoRequestDto> service,
            LinkGenerator linkGenerator)
        {
            _service = service;
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo tipo de moto",
            Description = "Cadastra um novo tipo de moto informando o nome dela."
        )]
        [ProducesResponseType(typeof(TipoMotoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoMotoResponseDto>> Create([FromBody] TipoMotoRequestDto dto)
        {
            var created = await _service.Save(dto);
            AddLinks(created);
            return CreatedAtAction(nameof(ReadById), new { id = created.IdTipo }, created);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar tipos de moto",
            Description = "Retorna todos os tipos de moto cadastrados, com suporte a paginação."
        )]
        [ProducesResponseType(typeof(IEnumerable<TipoMotoResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TipoMotoResponseDto>>> ReadAll(
            [FromQuery, SwaggerParameter("Número da página que deseja consultar (padrão = 1)")] int page = 1,
            [FromQuery, SwaggerParameter("Quantidade de registros por página (padrão = 10)")] int pageSize = 10)
        {
            var result = (await _service.GetAll(page, pageSize)).ToList();

            foreach (var tipo in result)
            {
                AddLinks(tipo);
            }

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(
            Summary = "Buscar tipo de moto por ID",
            Description = "Retorna os detalhes de um tipo de moto específico pelo seu identificador único."
        )]
        [ProducesResponseType(typeof(TipoMotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoMotoResponseDto>> ReadById(
            [FromRoute, SwaggerParameter("Identificador único do tipo de moto a ser consultado")] int id)
        {
            var tipo = await _service.GetById(id);
            AddLinks(tipo);
            return Ok(tipo);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(
            Summary = "Atualizar tipo de moto",
            Description = "Atualiza as informações de um tipo de moto existente."
        )]
        [ProducesResponseType(typeof(TipoMotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoMotoResponseDto>> Update(
            [FromRoute, SwaggerParameter("Identificador único do tipo de moto a ser atualizado")] int id,
            [FromBody] TipoMotoRequestDto dto)
        {
            var updated = await _service.Update(id, dto);
            AddLinks(updated);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(
            Summary = "Deletar tipo de moto",
            Description = "Remove permanentemente um tipo de moto pelo seu identificador."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromRoute, SwaggerParameter("Identificador único do tipo de moto a ser removido")] int id)
        {
            await _service.DeleteById(id);
            return NoContent();
        }

        private void AddLinks(TipoMotoResponseDto tipo)
        {
            tipo.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "TipoMoto", new { id = tipo.IdTipo }),
                "self",
                "GET"
            ));
            tipo.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(Update), "TipoMoto", new { id = tipo.IdTipo }),
                "update",
                "PUT"
            ));
            tipo.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(Delete), "TipoMoto", new { id = tipo.IdTipo }),
                "delete",
                "DELETE"
            ));
            tipo.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadAll), "TipoMoto"),
                "collection",
                "GET"
            ));
        }
    }
}
