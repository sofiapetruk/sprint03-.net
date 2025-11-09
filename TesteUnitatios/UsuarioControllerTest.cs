using System.Net;
using System.Net.Http.Json; 
using Sprint02.Models;

namespace sprint4.IntegrationTests
{
    public class UsuarioControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public UsuarioControllerTests(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetUsuarios_DeveRetornarLista()
        {
            var response = await _client.GetAsync("/api/usuarios");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var usuarios = await response.Content.ReadFromJsonAsync<List<Usuario>>();

            Assert.NotNull(usuarios);
            Assert.True(usuarios.Count >= 0);
        }
    }
}
