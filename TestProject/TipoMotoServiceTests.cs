using Microsoft.EntityFrameworkCore;
using Sprint02.Data;
using Sprint02.Service;
using Sprint02.DTOs;
using System.Threading.Tasks;
using Sprint02.Models;

namespace TestProject
{
    public class TipoMotoServiceTests
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public TipoMotoServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestTipoMotoDB" + Guid.NewGuid()) 
                .Options;
        }

        [Fact]
        public async Task Save_ShouldSaveTipoMoto()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var service = new TipoMotoService(context);

                var dto = new TipoMotoRequestDto { NomeTipo = "Mottu Sport ESD 2025" };

                var result = await service.Save(dto);

                Assert.NotNull(result);
                Assert.Equal("Mottu Sport ESD 2025", result.NomeTipo);

                var entitySaved = await context.TipoMotos.FindAsync(result.IdTipo);
                Assert.NotNull(entitySaved);
                Assert.Equal("Mottu Sport ESD 2025", entitySaved.NomeTipo);
            }
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllTipoMotos()
        {
            const int totalMotosInseridas = 5;
            string nomeDoPrimeiroItem = "Mottu Sport 110i 1";

            using (var context = new AppDbContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted(); 
                context.Database.EnsureCreated();

                for (int i = 1; i <= totalMotosInseridas; i++)
                {
                    context.TipoMotos.Add(new TipoMoto { NomeTipo = $"Mottu Sport 110i {i}" });
                }
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(_dbContextOptions))
            {
                var service = new TipoMotoService(context);

                var result = await service.GetAll(page: 1, pageSize: 50);

                Assert.NotNull(result);
                Assert.Equal(totalMotosInseridas, result.Count());

                Assert.Contains(result, t => t.NomeTipo == nomeDoPrimeiroItem);
            }
        }
    }
}