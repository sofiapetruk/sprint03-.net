using Microsoft.AspNetCore.Mvc;
using Sprint02.DTOs;
using Sprint02.Exceptions;
using Sprint02.Hateos;
using Sprint02.Service;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [SwaggerOperation(Summary = "Criar novo status de moto")]
        [ProducesResponseType(typeof(StatusMotoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<StatusMotoResponseDto>> Create([FromBody] StatusMotoRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Erro de validação: O valor do campo 'status' não corresponde a um Status válido."
                });
            }

            try
            {
                var created = await _service.Save(dto);

                AddLinks(created);
                return CreatedAtAction(nameof(ReadById), new { id = created.IdStatus }, created);
            }
            catch (ConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Ocorreu um erro interno inesperado ao processar a requisição." });
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listar status de motos")]
        [ProducesResponseType(typeof(IEnumerable<StatusMotoResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StatusMotoResponseDto>>> ReadAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = (await _service.GetAll(page, pageSize)).ToList();

            foreach (var status in result)
            {
                AddLinks(status);
            }

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Buscar status por ID")]
        [ProducesResponseType(typeof(StatusMotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusMotoResponseDto>> ReadById(
            [FromRoute] int id)
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Ocorreu um erro interno inesperado ao processar a requisição." });
            }
        }


        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deletar status de moto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromRoute] int id)
        {
            try
            {
                await _service.DeleteById(id);
                return NoContent();
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

        private void AddLinks(StatusMotoResponseDto status)
        {
            status.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "StatusMoto", new { id = status.IdStatus }),
                "self",
                "GET"
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