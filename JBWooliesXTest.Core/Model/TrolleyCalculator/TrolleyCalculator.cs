using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.Model.Request;
using JBWooliesXTest.Core.Model.TrolleyTotal;

namespace JBWooliesXTest.Core.Model.TrolleyCalculator
{
    public class TrolleyCalculator : ITrolleyCalculator
    {
        public async Task<decimal> Calculate(TrolleyTotalRequest trolleyTotalRequest)
        {
            return await Task.Run(() =>
            {
                var scenarios = trolleyTotalRequest.Specials.Select(special => new Scenario()
                {
                    Special = special,
                    Total = 0,
                    RequestedItems = trolleyTotalRequest.RequestedItems.Select(RequestedItem.Copy).ToList()
                }).ToList();
                var products = trolleyTotalRequest.Products;

                foreach (var scenario in scenarios)
                {
                    //Apply Special to Scenario
                    scenario.Total += scenario.Special.Total;
                    foreach (var specialInventory in scenario.Special.Inventory)
                    {
                        var matchedItem = scenario.RequestedItems.FirstOrDefault(_ => _.Name.Equals(specialInventory.Name));
                        if (matchedItem != null) matchedItem.QuantityFilled += specialInventory.Quantity;
                    }

                    //Fill remaining
                    foreach (var requestedItem in scenario.RequestedItems)
                    {
                        var quantityNeeded = requestedItem.Quantity - requestedItem.QuantityFilled;
                        if (quantityNeeded > 0)
                        {
                            var normalProduct = products.FirstOrDefault(_ =>
                                _.Name.Equals(requestedItem.Name));
                            if (normalProduct == null) continue;
                            scenario.Total += new decimal(quantityNeeded) * normalProduct.Price;
                        }
                    }

                }



                return scenarios.OrderBy(_ => _.Total).First().Total;
            });
        }

        private class Scenario
        {
            public decimal Total { get; set; }
            public Special Special { get; set; }
            public List<RequestedItem> RequestedItems { get; set; }
        }
    }
}
