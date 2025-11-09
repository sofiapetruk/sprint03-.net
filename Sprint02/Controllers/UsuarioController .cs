using Microsoft.AspNetCore.Mvc;
using Sprint02.DTOs;
using Sprint02.Exceptions;
using Sprint02.Hateos;
using Sprint02.NovaPasta1;
using Sprint02.Service;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Sprint02.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [SwaggerRequestExample(typeof(UsuarioRequestDto), typeof(UsuarioRequestExample))]
        public async Task<ActionResult<UsuarioResponseDto>> Create([FromBody] UsuarioRequestDto dto)
        {
            try
            {
                var user = await _service.Save(dto);
                AddLinks(user);
                return CreatedAtAction(nameof(ReadById), new { id = user.IdUsuario }, user);
            }
            catch (ConflictException ex) 
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Ocorreu um erro interno inesperado ao processar a requisição." });
            }
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
        [SwaggerRequestExample(typeof(UsuarioRequestDto), typeof(UsuarioRequestExample))]
        public async Task<ActionResult<UsuarioResponseDto>> Update(
            [FromRoute, SwaggerParameter("Identificador único do usuário a ser atualizado")] int id,
            [FromBody] UsuarioRequestDto dto)
        {
            try
            {
                var status = await _service.GetById(id);

                AddLinks(status);
                return Ok(status);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (ConflictException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Ocorreu um erro interno inesperado ao processar a requisição." });
            }

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
            try
            {
                var user = await _service.GetById(id);

                AddLinks(user);
                return Ok(user);
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message 
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Ocorreu um erro interno inesperado ao processar a requisição." });
            }
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
