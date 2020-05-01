using System.Collections.Generic;
using JBWooliesXTest.API.Controllers;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.Model.TrolleyTotal;
using Moq;
using Xunit;

namespace JBWooliesXTest.Test.Controllers
{
    public class TrolleyTotalControllerTests
    {
        private readonly TrolleyTotalRequest _trolleyTotalRequest;

        public TrolleyTotalControllerTests()
        {
            _trolleyTotalRequest = new TrolleyTotalRequest()
            {
                Quantities = new List<TrolleyTotalRequestQuantity>()
                {
                    new TrolleyTotalRequestQuantity() {Name = "1", Quantity = 3},
                    new TrolleyTotalRequestQuantity() {Name = "2", Quantity = 2}
                },
                Products = new List<TrolleyTotalRequestProduct>()
                {
                    new TrolleyTotalRequestProduct() {Name = "1", Price = 2},
                    new TrolleyTotalRequestProduct() {Name = "2", Price = 5}
                },
                Specials = new List<TrolleyTotalRequestSpecial>()
                {
                    new TrolleyTotalRequestSpecial()
                    {
                        Quantities = new List<TrolleyTotalRequestSpecialQuantity>()
                        {
                            new TrolleyTotalRequestSpecialQuantity() {Name = "1", Quantity = 3},
                            new TrolleyTotalRequestSpecialQuantity() {Name = "2", Quantity = 0}
                        },
                        Total = 5
                    },
                    new TrolleyTotalRequestSpecial()
                    {
                        Quantities = new List<TrolleyTotalRequestSpecialQuantity>()
                        {
                            new TrolleyTotalRequestSpecialQuantity() {Name = "1", Quantity = 1},
                            new TrolleyTotalRequestSpecialQuantity() {Name = "2", Quantity = 2}
                        },
                        Total = 10
                    }
                }
            };

        }

        [Fact]
        public async void Post_Returns_Expected_Total()
        {
            // Arrange
            var expectedTotal = new decimal(14);
            var resourceServiceClient = new Mock<IResourceServiceHttpClient>();
            resourceServiceClient
                .Setup(x => x.TrolleyCalculator(It.IsAny<TrolleyTotalRequest>()))
                .ReturnsAsync(expectedTotal);

            var controller = new TrolleyTotalController(resourceServiceClient.Object);

            // Act
            var result = await controller.Post(_trolleyTotalRequest);

            //Assert
            Assert.Equal(expectedTotal, result);
        }

    }
}
