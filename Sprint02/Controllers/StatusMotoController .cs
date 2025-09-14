using Microsoft.AspNetCore.Mvc;
using Sprint02.DTOs;
using Sprint02.Service;

namespace Sprint02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusMotoController : ControllerBase
    {
        private readonly IService<StatusMotoResponseDto, StatusMotoRequestDto> _service;

        public StatusMotoController(IService<StatusMotoResponseDto, StatusMotoRequestDto> service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(StatusMotoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StatusMotoResponseDto>> Create([FromBody] StatusMotoRequestDto dto)
        {
            var created = await _service.Save(dto);
            return CreatedAtAction(nameof(ReadById), new { id = created.IdStatus }, created);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StatusMotoResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StatusMotoResponseDto>>> ReadAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAll(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(StatusMotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusMotoResponseDto>> ReadById([FromRoute] int id)
        {
            try
            {
                var status = await _service.GetById(id);
                return Ok(status);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(StatusMotoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusMotoResponseDto>> Update([FromRoute] int id, [FromBody] StatusMotoRequestDto dto)
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
