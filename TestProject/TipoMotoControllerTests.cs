using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Sprint02.DTOs;

namespace TestProject
{
    public class TipoMotoControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TipoMotoControllerTests(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task TipoMoto_Create_ShouldReturnCreated()
        {
            var dto = new TipoMotoRequestDto
            {
                NomeTipo = "Mottu Sport 110i"
            };

            var response = await _client.PostAsJsonAsync("/api/v1/TipoMoto", dto);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
