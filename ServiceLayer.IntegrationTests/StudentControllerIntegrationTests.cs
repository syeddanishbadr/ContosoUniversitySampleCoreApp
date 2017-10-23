using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ServiceLayer.IntegrationTests
{
    public class StudentControllerIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;


        public StudentControllerIntegrationTests()
        {
            // Get configuration.

            var configuration = 
                new ConfigurationBuilder()
                    .SetBasePath(Path.GetFullPath(@"../../../../ServiceLayer/"))
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

            //Arrange
            _server = new TestServer(new WebHostBuilder()
           .UseStartup<Startup>()
           .UseConfiguration(configuration));

            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnStudentCollection()
        {
            //Act
            var response = await _client.GetAsync("/api/student");
            response.EnsureSuccessStatusCode();

            var responseObject = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal("Hello", responseObject);
        }
    }
}
