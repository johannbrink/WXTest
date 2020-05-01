using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JBWooliesXTest.API.Services;
using JBWooliesXTest.Core.HttpClientModel.Resource;
using JBWooliesXTest.Core.Model.TrolleyTotal;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace JBWooliesXTest.Test.Services
{
    public class ResourceServiceHttpHttpClientTests
    {
        private readonly IOptions<ResourceServiceHttpClientOptions> _mockOptions;

        public ResourceServiceHttpHttpClientTests()
        {
            var mockOptions = new Mock<IOptions<ResourceServiceHttpClientOptions>>();
            mockOptions
                .Setup(_ => _.Value)
                .Returns(new ResourceServiceHttpClientOptions()
                {
                    TrolleyCalculatorPath = It.IsAny<string>(),
                    ShopperHistoryPath = It.IsAny<string>(),
                    ProductPath = It.IsAny<string>(),
                    Token = It.IsAny<string>()
                });
            _mockOptions = mockOptions.Object;
        }

        [Fact]
        public async void GetProducts_Serialize_Deserialize()
        {
            // Arrange
            var httpClient = FakeHttpClient.Create_WithContent("[{\"name\":\"a\",\"price\":0}]");

            var sut = new ResourceServiceHttpHttpClient(httpClient, _mockOptions);

            // Act
            var result = await sut.GetProducts();

            //Assert
            var products = result.ToList();
            Assert.True(products.Count.Equals(1));
            Assert.Equal("a", products.First().Name);
        }

        [Fact]
        public async void GetShopperHistory_Serialize_Deserialize()
        {
            // Arrange
            var expectedShopperHistoryDto = new ShopperHistoryDto()
            {
                CustomerId = 123,
                Products = new List<ShopperHistoryProductDto>()
                {
                    new ShopperHistoryProductDto()
                    {
                        Name = "a",
                        Price = 1,
                        Quantity = 2
                    }
                }
            };
            var httpClient = FakeHttpClient.Create_WithContent("[{\"customerId\": 123,\"products\": [{\"name\": \"a\",\"price\": 1,\"quantity\": 2}]}]");

            var sut = new ResourceServiceHttpHttpClient(httpClient, _mockOptions);

            // Act
            var result = await sut.GetShopperHistory();

            //Assert
            var shopperHistory = result.ToList();
            Assert.True(shopperHistory.Count.Equals(1));
            Assert.Equal(expectedShopperHistoryDto.CustomerId, shopperHistory.First().CustomerId);
            Assert.True(shopperHistory.First().Products.Count.Equals(1));
            Assert.Equal(expectedShopperHistoryDto.Products.First().Name, shopperHistory.First().Products.First().Name);
            Assert.Equal(expectedShopperHistoryDto.Products.First().Price, shopperHistory.First().Products.First().Price);
            Assert.Equal(expectedShopperHistoryDto.Products.First().Quantity, shopperHistory.First().Products.First().Quantity);
        }

        [Fact]
        public async void TrolleyCalculator_Serialize_Deserialize()
        {
            // Arrange
            var expectedResult = new decimal(1);
            var httpClient = FakeHttpClient.Create_WithContent(expectedResult.ToString(CultureInfo.InvariantCulture));
            var trolleyTotalRequest = new TrolleyTotalRequest()
            {
                Quantities = new List<TrolleyTotalRequestQuantity>()
                {
                    new TrolleyTotalRequestQuantity()
                },
                Specials = new List<TrolleyTotalRequestSpecial>()
                {
                    new TrolleyTotalRequestSpecial()
                    {
                        Quantities = new List<TrolleyTotalRequestSpecialQuantity>()
                        {
                            new TrolleyTotalRequestSpecialQuantity()
                        }
                    }
                },
                Products = new List<TrolleyTotalRequestProduct>()
                {
                    new TrolleyTotalRequestProduct()
                }
            };
            var sut = new ResourceServiceHttpHttpClient(httpClient, _mockOptions);

            // Act
            var result = await sut.TrolleyCalculator(trolleyTotalRequest);

            //Assert
            Assert.Equal(expectedResult, result);
        }

    }
}
