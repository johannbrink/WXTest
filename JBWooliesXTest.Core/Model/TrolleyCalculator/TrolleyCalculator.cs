using System;
using System.Linq;
using System.Threading.Tasks;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.Model.Request;

namespace JBWooliesXTest.Core.Model.TrolleyCalculator
{
    public class TrolleyCalculator : ITrolleyCalculator
    {
        public async Task<decimal> Calculate(TrolleyTotalRequest trolleyTotalRequest)
        {
            return await Task.Run(() =>
            {
                var scenarios = Scenario.CreateScenarioListFromValidSpecials(trolleyTotalRequest);
                var products = trolleyTotalRequest.Products;

                foreach (var scenario in scenarios)
                {
                    //Apply Special to Scenario
                    scenario.Total += scenario.Special?.Total ?? 0;
                    if (scenario.Special != null)
                    {
                        foreach (var specialInventory in scenario.Special.Inventory)
                        {
                            var matchedItem =
                                scenario.RequestedItems.FirstOrDefault(_ => _.Name.Equals(specialInventory.Name));
                            if (matchedItem != null) matchedItem.QuantityFilled += specialInventory.Quantity;
                        }
                    }

                    //Fill remaining
                    Scenario.FillScenarioFromProducts(scenario, products);

                }

                var returnData = scenarios.Where(m => !m.RequestedItems.Any(n => n.Incomplete));

                return Math.Round(returnData.OrderBy(_ => _.Total).First().Total, 12);
            });
        }
    }
}
