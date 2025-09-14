﻿using Microsoft.EntityFrameworkCore;
using Sprint02.Data;
using Sprint02.DTOs;
using Sprint02.Models;

namespace Sprint02.Service
{
    public class UsuarioService : IService<UsuarioResponseDto, UsuarioRequestDto>
    {
        private readonly AppDbContext _appDbContext;

        private const string NOT_FOUND_MESSAGE = "Usuário com esse ID não foi encontrado.";

        public UsuarioService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<UsuarioResponseDto> Save(UsuarioRequestDto dto)
        {
            var entity = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha 
            };

            _appDbContext.Usuarios.Add(entity);
            await _appDbContext.SaveChangesAsync();

            return new UsuarioResponseDto
            {
                IdUsuario = entity.IdUsuario,
                Nome = entity.Nome,
                Email = entity.Email
            };
        }

        public async Task<IEnumerable<UsuarioResponseDto>> GetAll(int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;

            return await _appDbContext.Usuarios
                .AsNoTracking()
                .OrderBy(u => u.IdUsuario)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UsuarioResponseDto
                {
                    IdUsuario = u.IdUsuario,
                    Nome = u.Nome,
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<UsuarioResponseDto> GetById(int id)
        {
            var usuario = await _appDbContext.Usuarios
                .AsNoTracking()
                .Where(u => u.IdUsuario == id)
                .Select(u => new UsuarioResponseDto
                {
                    IdUsuario = u.IdUsuario,
                    Nome = u.Nome,
                    Email = u.Email
                })
                .FirstOrDefaultAsync();

            if (usuario == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);
            return usuario;
        }

        public async Task<UsuarioResponseDto> Update(int id, UsuarioRequestDto dto)
        {
            var entity = await _appDbContext.Usuarios.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);

            entity.Nome = dto.Nome;
            entity.Email = dto.Email;
            entity.Senha = dto.Senha;

            await _appDbContext.SaveChangesAsync();

            return new UsuarioResponseDto
            {
                IdUsuario = entity.IdUsuario,
                Nome = entity.Nome,
                Email = entity.Email
            };
        }

        public async Task DeleteById(int id)
        {
            var entity = await _appDbContext.Usuarios.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException(NOT_FOUND_MESSAGE);

            _appDbContext.Usuarios.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
