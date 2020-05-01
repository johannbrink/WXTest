using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JBWooliesXTest.API.Controllers;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.HttpClientModel.Resource;
using JBWooliesXTest.Core.Model.Request;
using JBWooliesXTest.Core.Model.TrolleyCalculator;
using JBWooliesXTest.Core.Model.TrolleyTotal;
using Moq;
using Xunit;

namespace JBWooliesXTest.Test.Core
{
    public class TrolleyCalculatorTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void Should_Calculate(bool leaveOutAProduct)
        {
            // Arrange
            var expectedResponse = new decimal(14);
            var request = CreateTrolleyTotalRequest(leaveOutAProduct);
            var sut = new TrolleyCalculator();

            // Act
            var response = await sut.Calculate(request);

            // Assert
            Assert.Equal(expectedResponse, response);
        }

        private static TrolleyTotalRequest CreateTrolleyTotalRequest(bool leaveOutAProduct)
        {
            return new TrolleyTotalRequest()
            {
                RequestedItems = new List<RequestedItem>()
                {
                    new RequestedItem() {Name = "1", Quantity = 3},
                    new RequestedItem() {Name = "2", Quantity = 2}
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
                },
                Products = leaveOutAProduct
                    ? new List<Product>()
                    {
                        new Product() {Name = "1", Price = 2}
                    }
                    : new List<Product>()
                    {
                        new Product() {Name = "1", Price = 2},
                        new Product() {Name = "2", Price = 5}
                    }
            };

        }
    }
}
