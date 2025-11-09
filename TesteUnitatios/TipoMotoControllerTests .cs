using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Sprint02.DTOs;
using Xunit;

namespace sprint4.IntegrationTests
{
    public class TipoMotoControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TipoMotoControllerTests(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "DELETE /api/tipo-moto/{id} deve remover um tipo de moto existente e retornar 204")]
        public async Task Delete_DeveRemoverTipoMotoExistente()
        {
            var novoTipo = new TipoMotoRequestDto
            {
                NomeTipo = "Street"
            };

            var postResponse = await _client.PostAsJsonAsync("/api/tipo-moto", novoTipo);
            postResponse.EnsureSuccessStatusCode();

            var created = await postResponse.Content.ReadFromJsonAsync<TipoMotoResponseDto>();
            Assert.NotNull(created);
            Assert.True(created.IdTipo > 0);

            var deleteResponse = await _client.DeleteAsync($"/api/tipo-moto/{created.IdTipo}");

            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Fact(DisplayName = "DELETE /api/tipo-moto/{id} deve retornar 404 se o tipo não existir")]
        public async Task Delete_DeveRetornar404_QuandoTipoNaoExiste()
        {
            var response = await _client.DeleteAsync("/api/tipo-moto/99999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
