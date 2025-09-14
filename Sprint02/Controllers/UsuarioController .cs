using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Sprint02.DTOs;
using Sprint02.Hateos;
using Sprint02.Service;

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
        public async Task<ActionResult<UsuarioResponseDto>> Create([FromBody] UsuarioRequestDto dto)
        {
            var user = await _service.Save(dto);

            user.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "Usuario", new { id = user.IdUsuario }),
                "self",
                "GET"
            ));

            return CreatedAtAction(nameof(ReadById), new { id = user.IdUsuario }, user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> ReadAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var users = (await _service.GetAll(page, pageSize)).ToList();

            foreach (var u in users)
            {
                u.Links.Add(new Link(
                    _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "Usuario", new { id = u.IdUsuario }),
                    "self",
                    "GET"
                ));
            }

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioResponseDto>> ReadById([FromRoute] int id)
        {
            var user = await _service.GetById(id);

            user.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadAll), "Usuario"),
                "collection",
                "GET"
            ));

            return Ok(user);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UsuarioResponseDto>> Update([FromRoute] int id, [FromBody] UsuarioRequestDto dto)
        {
            var user = await _service.Update(id, dto);

            user.Links.Add(new Link(
                _linkGenerator.GetUriByAction(HttpContext, nameof(ReadById), "Usuario", new { id = user.IdUsuario }),
                "self",
                "GET"
            ));

            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteById(id);
            return NoContent();
        }
    }
}
