using Microsoft.EntityFrameworkCore;
using Sprint02.Data;
using Sprint02.DTOs;
using Sprint02.Models;

namespace Sprint02.Service
{
    public class TipoMotoService : IService<TipoMotoResponseDto, TipoMotoRequestDto>
    {
        private readonly AppDbContext _appDbContext;

        private const string NOT_FOUND_MESSAGE = "Tipo de moto com esse ID não foi encontrado.";

        public TipoMotoService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<TipoMotoResponseDto> Save(TipoMotoRequestDto dto)
        {
            var entity = new TipoMoto
            {
                NomeTipo = dto.NomeTipo
            };

            _appDbContext.TipoMotos.Add(entity);
            await _appDbContext.SaveChangesAsync();

            return new TipoMotoResponseDto
            {
                IdTipo = entity.IdTipo,
                NomeTipo = entity.NomeTipo
            };
        }

        public async Task<IEnumerable<TipoMotoResponseDto>> GetAll(int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;

            return await _appDbContext.TipoMotos
                .AsNoTracking()
                .OrderBy(t => t.IdTipo)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TipoMotoResponseDto
                {
                    IdTipo = t.IdTipo,
                    NomeTipo = t.NomeTipo
                })
                .ToListAsync();
        }

        public async Task<TipoMotoResponseDto> GetById(int id)
        {
            var tipo = await _appDbContext.TipoMotos
                .AsNoTracking()
                .Where(t => t.IdTipo == id)
                .Select(t => new TipoMotoResponseDto
                {
                    IdTipo = t.IdTipo,
                    NomeTipo = t.NomeTipo
                })
                .FirstOrDefaultAsync();

            if (tipo == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);
            return tipo;
        }

        public async Task<TipoMotoResponseDto> Update(int id, TipoMotoRequestDto dto)
        {
            var entity = await _appDbContext.TipoMotos.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);

            entity.NomeTipo = dto.NomeTipo;

            await _appDbContext.SaveChangesAsync();

            return new TipoMotoResponseDto
            {
                IdTipo = entity.IdTipo,
                NomeTipo = entity.NomeTipo
            };
        }

        public async Task DeleteById(int id)
        {
            var entity = await _appDbContext.TipoMotos.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);

            _appDbContext.TipoMotos.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
