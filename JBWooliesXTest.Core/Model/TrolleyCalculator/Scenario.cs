using System.Collections.Generic;
using System.Linq;
using JBWooliesXTest.Core.Model.Request;

namespace JBWooliesXTest.Core.Model.TrolleyCalculator
{
    public class Scenario
    {
        public decimal Total { get; set; }
        public Special Special { get; set; }
        public List<RequestedItem> RequestedItems { get; set; }

        public static List<Scenario> CreateList(TrolleyTotalRequest trolleyTotalRequest)
        {
            var scenarios = trolleyTotalRequest.Specials
                .Where(_ => _.IsValidForTrolleyTotalRequest(trolleyTotalRequest)).Select(special => new Scenario()
                {
                    Special = special,
                    RequestedItems = trolleyTotalRequest.RequestedItems.Select(RequestedItem.Copy).ToList()
                }).ToList();
            return scenarios;
        }

        public static void FillScenarioFromProducts(Scenario scenario, List<Product> products)
        {
            foreach (var requestedItem in scenario.RequestedItems)
            {
                var quantityNeeded = requestedItem.Quantity - requestedItem.QuantityFilled;
                if (quantityNeeded <= 0) continue;
                var normalProduct = products.FirstOrDefault(_ => _.Name.Equals(requestedItem.Name));
                if (normalProduct == null) continue;
                requestedItem.QuantityFilled += quantityNeeded;
                scenario.Total += new decimal(quantityNeeded) * normalProduct.Price;
            }
        }
    }
}
