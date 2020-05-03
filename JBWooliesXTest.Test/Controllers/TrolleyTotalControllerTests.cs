using System.Collections.Generic;
using JBWooliesXTest.API.Controllers;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.Model.Request;
using JBWooliesXTest.Core.Model.TrolleyCalculator;
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
                 RequestedItems= new List<RequestedItem>()
                {
                    new RequestedItem() {Name = "1", Quantity = 3},
                    new RequestedItem() {Name = "2", Quantity = 2}
                },
                Products = new List<Product>()
                {
                    new Product() {Name = "1", Price = 2},
                    new Product() {Name = "2", Price = 5}
                },
                Specials = new List<Special>()
                {
                    new Special()
                    {
                        Inventory = new List<SpecialInventory>()
                        {
                            new SpecialInventory() {Name = "1", Quantity = 3},
                            new SpecialInventory() {Name = "2", Quantity = 0}
                        },
                        Total = 5
                    },
                    new Special()
                    {
                        Inventory = new List<SpecialInventory>()
                        {
                            new SpecialInventory() {Name = "1", Quantity = 1},
                            new SpecialInventory() {Name = "2", Quantity = 2}
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
            var resourceServiceClient = new Mock<ITrolleyCalculator>();
            resourceServiceClient
                .Setup(x => x.Calculate(It.IsAny<TrolleyTotalRequest>()))
                .ReturnsAsync(expectedTotal);

            var controller = new TrolleyTotalController(resourceServiceClient.Object);

            // Act
            var result = await controller.Post(_trolleyTotalRequest);

            //Assert
            Assert.Equal(expectedTotal, result);
        }

    }
}
