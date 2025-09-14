using Microsoft.EntityFrameworkCore;
using Sprint02.Data;
using Sprint02.DTOs;
using Sprint02.Models;

namespace Sprint02.Service
{
    public class MotoService : IService<MotoResponseDto, MotoRequestDto>
    {
        private readonly AppDbContext _appDbContext;

        private const string NOT_FOUND_MESSAGE = "Moto com esse ID não foi encontrada.";
        private const string STATUS_NOT_FOUND_MESSAGE = "Status com esse ID não foi encontrado.";
        private const string TIPO_NOT_FOUND_MESSAGE = "Tipo de moto com esse ID não foi encontrado.";

        public MotoService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<MotoResponseDto> Save(MotoRequestDto dto)
        {
            var status = await _appDbContext.StatusMotos.FindAsync(dto.IdStatus);
            if (status == null) throw new KeyNotFoundException(STATUS_NOT_FOUND_MESSAGE);

            var tipo = await _appDbContext.TipoMotos.FindAsync(dto.IdTipo);
            if (tipo == null) throw new KeyNotFoundException(TIPO_NOT_FOUND_MESSAGE);

            var entity = new Moto
            {
                NmChassi = dto.NmChassi,
                Placa = dto.Placa,
                Unidade = dto.Unidade,
                IdStatus = dto.IdStatus,
                IdTipo = dto.IdTipo,
                status = status,
                modelo = tipo
            };

            _appDbContext.Motos.Add(entity);
            await _appDbContext.SaveChangesAsync();

            return new MotoResponseDto
            {
                IdMoto = entity.IdMoto,
                NmChassi = entity.NmChassi,
                Placa = entity.Placa,
                Unidade = entity.Unidade,
                IdStatus = entity.IdStatus,
                IdTipo = entity.IdTipo
            };
        }

        public async Task<IEnumerable<MotoResponseDto>> GetAll(int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;

            return await _appDbContext.Motos
                .AsNoTracking()
                .OrderBy(m => m.IdMoto)
                .Select(m => new MotoResponseDto
                {
                    IdMoto = m.IdMoto,
                    NmChassi = m.NmChassi,
                    Placa = m.Placa,
                    Unidade = m.Unidade,
                    IdStatus = m.IdStatus,
                    IdTipo = m.IdTipo
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<MotoResponseDto> GetById(int id)
        {
            var moto = await _appDbContext.Motos
                .AsNoTracking()
                .Where(m => m.IdMoto == id)
                .Select(m => new MotoResponseDto
                {
                    IdMoto = m.IdMoto,
                    NmChassi = m.NmChassi,
                    Placa = m.Placa,
                    Unidade = m.Unidade,
                    IdStatus = m.IdStatus,
                    IdTipo = m.IdTipo
                })
                .FirstOrDefaultAsync();

            if (moto == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);
            return moto;
        }

        public async Task<MotoResponseDto> Update(int id, MotoRequestDto dto)
        {
            var entity = await _appDbContext.Motos.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);

            var status = await _appDbContext.StatusMotos.FindAsync(dto.IdStatus);
            if (status == null) throw new KeyNotFoundException(STATUS_NOT_FOUND_MESSAGE);

            var tipo = await _appDbContext.TipoMotos.FindAsync(dto.IdTipo);
            if (tipo == null) throw new KeyNotFoundException(TIPO_NOT_FOUND_MESSAGE);

            entity.NmChassi = dto.NmChassi;
            entity.Placa = dto.Placa;
            entity.Unidade = dto.Unidade;
            entity.IdStatus = dto.IdStatus;
            entity.IdTipo = dto.IdTipo;
            entity.status = status;
            entity.modelo = tipo;

            await _appDbContext.SaveChangesAsync();

            return new MotoResponseDto
            {
                IdMoto = entity.IdMoto,
                NmChassi = entity.NmChassi,
                Placa = entity.Placa,
                Unidade = entity.Unidade,
                IdStatus = entity.IdStatus,
                IdTipo = entity.IdTipo
            };
        }

        public async Task DeleteById(int id)
        {
            var entity = await _appDbContext.Motos.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);

            _appDbContext.Motos.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
