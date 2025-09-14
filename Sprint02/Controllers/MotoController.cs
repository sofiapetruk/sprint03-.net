using Microsoft.AspNetCore.Mvc;
using Sprint02.DTOs;
using Sprint02.Hateos;
using Sprint02.Service;

namespace Sprint02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<MotoResponseDto>> Create([FromBody] MotoRequestDto dto)
        {
            var moto = await _service.Save(dto);

            var link = new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "Moto", new { id = moto.IdMoto }),
                "self",
                "GET"
            );
            moto.Links.Add(link);

            return CreatedAtAction(nameof(ReadById), new { id = moto.IdMoto }, moto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotoResponseDto>>> ReadAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var motos = (await _service.GetAll(page, pageSize)).ToList();

            foreach (var m in motos)
            {
                var link = new Link(
                    _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "Moto", new { id = m.IdMoto }),
                    "self",
                    "GET"
                );
                m.Links.Add(link);
            }

            return Ok(motos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotoResponseDto>> ReadById(int id)
        {
            var moto = await _service.GetById(id);

            var link = new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadAll), "Moto"),
                "Lista de motos",
                "GET"
            );
            moto.Links.Add(link);

            return Ok(moto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MotoResponseDto>> Update(int id, [FromBody] MotoRequestDto dto)
        {
            var moto = await _service.Update(id, dto);

            var link = new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "Moto", new { id = moto.IdMoto }),
                "self",
                "GET"
            );
            moto.Links.Add(link);

            return Ok(moto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteById(id);
            return NoContent();
        }
    }
}
