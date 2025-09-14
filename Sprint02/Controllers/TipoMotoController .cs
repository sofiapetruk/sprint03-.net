using Microsoft.AspNetCore.Mvc;
using Sprint02.DTOs;
using Sprint02.Service;

namespace Sprint02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMotoController : ControllerBase
    {
        private readonly IService<TipoMotoResponseDto, TipoMotoRequestDto> _service;

        public TipoMotoController(IService<TipoMotoResponseDto, TipoMotoRequestDto> service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TipoMotoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoMotoResponseDto>> Create([FromBody] TipoMotoRequestDto dto)
        {
            var created = await _service.Save(dto);
            return CreatedAtAction(nameof(ReadById), new { id = created.IdTipo }, created);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TipoMotoResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TipoMotoResponseDto>>> ReadAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAll(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TipoMotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoMotoResponseDto>> ReadById([FromRoute] int id)
        {
            try
            {
                var tipo = await _service.GetById(id);
                return Ok(tipo);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(TipoMotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoMotoResponseDto>> Update([FromRoute] int id, [FromBody] TipoMotoRequestDto dto)
        {
            try
            {
                var updated = await _service.Update(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _service.DeleteById(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
