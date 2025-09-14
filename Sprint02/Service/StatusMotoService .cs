using Microsoft.EntityFrameworkCore;
using Sprint02.Data;
using Sprint02.DTOs;
using Sprint02.Models;

namespace Sprint02.Service
{
    public class StatusMotoService : IService<StatusMotoResponseDto, StatusMotoRequestDto>
    {
        private readonly AppDbContext _appDbContext;

        private const string NOT_FOUND_MESSAGE = "Status de moto com esse ID não foi encontrado.";

        public StatusMotoService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<StatusMotoResponseDto> Save(StatusMotoRequestDto dto)
        {
            var entity = new StatusMoto
            {
                Status = dto.Status,
                Data = dto.Data
            };

            _appDbContext.StatusMotos.Add(entity);
            await _appDbContext.SaveChangesAsync();

            return new StatusMotoResponseDto
            {
                IdStatus = entity.IdStatus,
                Status = entity.Status,
                Data = entity.Data
            };
        }

        public async Task<IEnumerable<StatusMotoResponseDto>> GetAll(int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;

            return await _appDbContext.StatusMotos
                .AsNoTracking()
                .OrderBy(s => s.IdStatus)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StatusMotoResponseDto
                {
                    IdStatus = s.IdStatus,
                    Status = s.Status,
                    Data = s.Data
                })
                .ToListAsync();
        }

        public async Task<StatusMotoResponseDto> GetById(int id)
        {
            var status = await _appDbContext.StatusMotos
                .AsNoTracking()
                .Where(s => s.IdStatus == id)
                .Select(s => new StatusMotoResponseDto
                {
                    IdStatus = s.IdStatus,
                    Status = s.Status,
                    Data = s.Data
                })
                .FirstOrDefaultAsync();

            if (status == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);
            return status;
        }

        public async Task<StatusMotoResponseDto> Update(int id, StatusMotoRequestDto dto)
        {
            var entity = await _appDbContext.StatusMotos.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);

            entity.Status = dto.Status;
            entity.Data = dto.Data;

            await _appDbContext.SaveChangesAsync();

            return new StatusMotoResponseDto
            {
                IdStatus = entity.IdStatus,
                Status = entity.Status,
                Data = entity.Data
            };
        }

        public async Task DeleteById(int id)
        {
            var entity = await _appDbContext.StatusMotos.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);

            _appDbContext.StatusMotos.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
