using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Sprint02.DTOs;
using Sprint02.Enuns;
using Xunit;

namespace sprint4.IntegrationTests
{
    public class StatusMotoControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public StatusMotoControllerTests(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetById_DeveRetornarStatusMotoExistente()
        {
            var novoStatus = new StatusMotoRequestDto
            {
                Status = StatusEnum.ATIVO
            };

            var postResponse = await _client.PostAsJsonAsync("/api/status-moto", novoStatus);
            postResponse.EnsureSuccessStatusCode();

            var created = await postResponse.Content.ReadFromJsonAsync<StatusMotoResponseDto>();
            Assert.NotNull(created);
            Assert.True(created.IdStatus > 0);

            var getResponse = await _client.GetAsync($"/api/status-moto/{created.IdStatus}");

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var status = await getResponse.Content.ReadFromJsonAsync<StatusMotoResponseDto>();
            Assert.NotNull(status);
            Assert.Equal(created.IdStatus, status.IdStatus);
            Assert.Equal(StatusEnum.ATIVO.ToString(), status.Status); 
        }
    }
}
