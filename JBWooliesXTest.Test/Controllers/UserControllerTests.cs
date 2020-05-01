using JBWooliesXTest.API.Controllers;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace JBWooliesXTest.Test.Controllers
{
    public class ResourceServiceHttpHttpClientTests
    {
        [Fact]
        public void Get_Returns_Configured_Token()
        {
            // Arrange
            const string expectedToken = "expectedToken";
            const string expectedName = "Johann Brink";
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection
                .Setup(x => x.Value)
                .Returns(expectedToken);
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration
                .Setup(x => x.GetSection("ResourceServiceHttpHttpClient:Token"))
                .Returns(mockConfSection.Object);

            var controller = new UserController(mockConfiguration.Object);

            // Act
            var result = controller.Get();

            //Assert
            Assert.Equal(expectedToken, result.Token);
            Assert.Equal(expectedName, result.Name);
        }
    }
}
