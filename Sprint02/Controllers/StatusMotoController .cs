using Microsoft.AspNetCore.Mvc;
using Sprint02.DTOs;
using Sprint02.Hateos;
using Sprint02.Service;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusMotoController : ControllerBase
    {
        private readonly IService<StatusMotoResponseDto, StatusMotoRequestDto> _service;
        private readonly LinkGenerator _linkGenerator;

        public StatusMotoController(
            IService<StatusMotoResponseDto, StatusMotoRequestDto> service,
            LinkGenerator linkGenerator)
        {
            _service = service;
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar novo status de moto",
            Description = "Cadastra um novo status para as motos, informando descrição e data."
        )]
        [ProducesResponseType(typeof(StatusMotoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StatusMotoResponseDto>> Create([FromBody] StatusMotoRequestDto dto)
        {
            var created = await _service.Save(dto);
            AddLinks(created);
            return CreatedAtAction(nameof(ReadById), new { id = created.IdStatus }, created);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar status de motos",
            Description = "Retorna todos os status cadastrados, com suporte a paginação."
        )]
        [ProducesResponseType(typeof(IEnumerable<StatusMotoResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StatusMotoResponseDto>>> ReadAll(
            [FromQuery, SwaggerParameter("Número da página que deseja consultar (padrão = 1)")] int page = 1,
            [FromQuery, SwaggerParameter("Quantidade de registros por página (padrão = 10)")] int pageSize = 10)
        {
            var result = (await _service.GetAll(page, pageSize)).ToList();

            foreach (var status in result)
            {
                AddLinks(status);
            }

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(
            Summary = "Buscar status por ID",
            Description = "Retorna os detalhes de um status de moto específico pelo seu identificador único."
        )]
        [ProducesResponseType(typeof(StatusMotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusMotoResponseDto>> ReadById(
            [FromRoute, SwaggerParameter("Identificador único do status a ser consultado")] int id)
        {
            var status = await _service.GetById(id);
            AddLinks(status);
            return Ok(status);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(
            Summary = "Atualizar status de moto",
            Description = "Atualiza as informações de um status de moto existente."
        )]
        [ProducesResponseType(typeof(StatusMotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusMotoResponseDto>> Update(
            [FromRoute, SwaggerParameter("Identificador único do status a ser atualizado")] int id,
            [FromBody] StatusMotoRequestDto dto)
        {
            var updated = await _service.Update(id, dto);
            AddLinks(updated);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(
            Summary = "Deletar status de moto",
            Description = "Remove permanentemente um status de moto pelo seu identificador."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromRoute, SwaggerParameter("Identificador único do status a ser removido")] int id)
        {
            await _service.DeleteById(id);
            return NoContent();
        }

        private void AddLinks(StatusMotoResponseDto status)
        {
            status.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "StatusMoto", new { id = status.IdStatus }),
                "self",
                "GET"
            ));
            status.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(Update), "StatusMoto", new { id = status.IdStatus }),
                "update",
                "PUT"
            ));
            status.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(Delete), "StatusMoto", new { id = status.IdStatus }),
                "delete",
                "DELETE"
            ));
            status.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadAll), "StatusMoto"),
                "collection",
                "GET"
            ));
        }
    }
}
