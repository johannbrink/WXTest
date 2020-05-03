using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
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

        [Fact]
        public async void Should_Calculate_Complex()
        {
            // Arrange
            var expectedResponse = new decimal(110.21877083471924);
            var request = CreateComplexTrolleyTotalRequest();
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

        private static TrolleyTotalRequest CreateComplexTrolleyTotalRequest()
        {
            const string jsonString = "{\"Products\":[{\"Name\":\"Product 0\",\"Price\":4.82914699233563},{\"Name\":\"Product 1\",\"Price\":7.6251958811773},{\"Name\":\"Product 2\",\"Price\":14.0838007834199}],\"Specials\":[{\"Quantities\":[{\"Name\":\"Product 0\",\"Quantity\":7},{\"Name\":\"Product 1\",\"Quantity\":6},{\"Name\":\"Product 2\",\"Quantity\":7}],\"Total\":0.670200631550569},{\"Quantities\":[{\"Name\":\"Product 0\",\"Quantity\":2},{\"Name\":\"Product 1\",\"Quantity\":1},{\"Name\":\"Product 2\",\"Quantity\":0}],\"Total\":21.7536777658911},{\"Quantities\":[{\"Name\":\"Product 0\",\"Quantity\":0},{\"Name\":\"Product 1\",\"Quantity\":3},{\"Name\":\"Product 2\",\"Quantity\":0}],\"Total\":10.1602634653833},{\"Quantities\":[{\"Name\":\"Product 0\",\"Quantity\":5},{\"Name\":\"Product 1\",\"Quantity\":6},{\"Name\":\"Product 2\",\"Quantity\":8}],\"Total\":24.9184350974429},{\"Quantities\":[{\"Name\":\"Product 0\",\"Quantity\":5},{\"Name\":\"Product 1\",\"Quantity\":5},{\"Name\":\"Product 2\",\"Quantity\":4}],\"Total\":19.3128780411468}],\"Quantities\":[{\"Name\":\"Product 0\",\"Quantity\":8},{\"Name\":\"Product 1\",\"Quantity\":2},{\"Name\":\"Product 2\",\"Quantity\":4}]}";
            return JsonSerializer.Deserialize<TrolleyTotalRequest>(jsonString);
        }
    }
}
