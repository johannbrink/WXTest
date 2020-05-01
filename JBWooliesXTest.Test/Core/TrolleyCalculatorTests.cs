using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JBWooliesXTest.API.Controllers;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.HttpClientModel.Resource;
using JBWooliesXTest.Core.Model.TrolleyCalculator;
using JBWooliesXTest.Core.Model.TrolleyTotal;
using Moq;
using Xunit;

namespace JBWooliesXTest.Test.Core
{
    public class TrolleyCalculatorTests
    {
        [Fact]
        public async void Should_Calculate()
        {
            // Arrange
            var expectedResponse = new decimal(14);
            var request = CreateTrolleyTotalRequest();
            var sut = new TrolleyCalculator();

            // Act
            var response = await sut.Calculate(request);

            // Assert
            Assert.Equal(expectedResponse, response);
        }

        private static TrolleyTotalRequest CreateTrolleyTotalRequest()
        {
            return new TrolleyTotalRequest()
            {
                Quantities = new List<TrolleyTotalRequestQuantity>()
                {
                    new TrolleyTotalRequestQuantity() {Name = "1", Quantity = 3},
                    new TrolleyTotalRequestQuantity() {Name = "2", Quantity = 2}
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
                },
                Products = new List<TrolleyTotalRequestProduct>()
                {
                    new TrolleyTotalRequestProduct() {Name = "1", Price = 2},
                    new TrolleyTotalRequestProduct() {Name = "2", Price = 5}
                }
            };

        }
    }
}
