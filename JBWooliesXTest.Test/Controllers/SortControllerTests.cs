using System.Collections.Generic;
using System.Linq;
using JBWooliesXTest.API.Controllers;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.HttpClientModel.Resource;
using Moq;
using Xunit;

namespace JBWooliesXTest.Test.Controllers
{
    public class SortControllerTests
    {
        [Theory]
        [InlineData("High", "Product B")]
        [InlineData("Low", "Product A")]
        [InlineData("Ascending", "Product A")]
        [InlineData("Descending", "Product B")]
        [InlineData("Recommended", "Product B")]
        public void Get_Returns_For_Valid_Options(string sortOption, string expectedFirstName)
        {
            // Arrange
            var productList = new List<ProductDto>()
            {
                new ProductDto() {Name = "Product A", Quantity = 1, Price = 1},
                new ProductDto() {Name = "Product B", Quantity = 1, Price = 2}
            };
            var shopperHistoryDtoList= new List<ShopperHistoryDto>()
            {
                new ShopperHistoryDto()
                {
                    CustomerId = 0,
                    Products = new List<ShopperHistoryProductDto>()
                    {
                        new ShopperHistoryProductDto() {Name = "Product A", Quantity = 10},
                        new ShopperHistoryProductDto() {Name = "Product B", Quantity = 20}
                    }
                }
            };
            var resourceServiceClient = new Mock<IResourceServiceHttpClient>();
            resourceServiceClient
                .Setup(x => x.GetProducts())
                .ReturnsAsync(productList);
            resourceServiceClient
                .Setup(x => x.GetShopperHistory())
                .ReturnsAsync(shopperHistoryDtoList);

            var controller = new SortController(resourceServiceClient.Object);

            // Act
            var result = controller.Get(sortOption);

            //Assert
            Assert.False(result.IsFaulted);
            Assert.Equal(2, result.Result.Count());
            Assert.Equal(expectedFirstName, result.Result.First().Name);
            Assert.Equal(1, result.Result.First().Quantity);
            //Assert.Equal(1, result.Result.First().Price);
            //Assert.Equal(0, result.Result.First().Popularity);
        }

        [Fact]
        public void Get_Faults_For_Invalid_Option()
        {
            // Arrange
            var resourceServiceClient = new Mock<IResourceServiceHttpClient>();
            resourceServiceClient
                .Setup(x => x.GetProducts())
                .ReturnsAsync(new List<ProductDto>());

            var controller = new SortController(resourceServiceClient.Object);

            // Act
            var result = controller.Get("bla");

            //Assert
            Assert.True(result.IsFaulted);
        }
    }
}
