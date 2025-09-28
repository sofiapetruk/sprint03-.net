using Microsoft.AspNetCore.Mvc;
using Sprint02.DTOs;
using Sprint02.Hateos;
using Sprint02.Service;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IService<UsuarioResponseDto, UsuarioRequestDto> _service;
        private readonly LinkGenerator _linkGenerator;

        public UsuarioController(
            IService<UsuarioResponseDto, UsuarioRequestDto> service,
            LinkGenerator linkGenerator)
        {
            _service = service;
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo usuário",
            Description = "Cadastra um novo usuário no sistema informando nome, email e senha."
        )]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioResponseDto>> Create([FromBody] UsuarioRequestDto dto)
        {
            var user = await _service.Save(dto);
            AddLinks(user);
            return CreatedAtAction(nameof(ReadById), new { id = user.IdUsuario }, user);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar todos os usuários",
            Description = "Retorna todos os usuários cadastrados, com suporte a paginação."
        )]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> ReadAll(
            [FromQuery, SwaggerParameter("Número da página que deseja consultar (padrão = 1)")] int page = 1,
            [FromQuery, SwaggerParameter("Quantidade de registros por página (padrão = 10)")] int pageSize = 10)
        {
            var users = (await _service.GetAll(page, pageSize)).ToList();

            foreach (var u in users)
            {
                AddLinks(u);
            }

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(
            Summary = "Buscar usuário por ID",
            Description = "Retorna os detalhes de um usuário específico pelo seu identificador único."
        )]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioResponseDto>> ReadById(
            [FromRoute, SwaggerParameter("Identificador único do usuário a ser consultado")] int id)
        {
            var user = await _service.GetById(id);
            AddLinks(user);
            return Ok(user);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(
            Summary = "Atualizar usuário",
            Description = "Atualiza as informações de um usuário existente."
        )]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioResponseDto>> Update(
            [FromRoute, SwaggerParameter("Identificador único do usuário a ser atualizado")] int id,
            [FromBody] UsuarioRequestDto dto)
        {
            var user = await _service.Update(id, dto);
            AddLinks(user);
            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(
            Summary = "Deletar usuário",
            Description = "Remove permanentemente um usuário do sistema pelo seu identificador único."
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(
            [FromRoute, SwaggerParameter("Identificador único do usuário a ser removido")] int id)
        {
            await _service.DeleteById(id);
            return NoContent();
        }

        private void AddLinks(UsuarioResponseDto user)
        {
            user.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "Usuario", new { id = user.IdUsuario }),
                "self",
                "GET"
            ));
            user.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(Update), "Usuario", new { id = user.IdUsuario }),
                "update",
                "PUT"
            ));
            user.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(Delete), "Usuario", new { id = user.IdUsuario }),
                "delete",
                "DELETE"
            ));
            user.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadAll), "Usuario"),
                "collection",
                "GET"
            ));
        }
    }
}
