using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using JBWooliesXTest.Core.Model.Request;

namespace JBWooliesXTest.Core.Model.TrolleyCalculator
{
    public class Special
    {
        [JsonPropertyName("Quantities")]
        public List<SpecialInventory>  Inventory { get; set; }
        public decimal Total { get; set; }

        public bool IsValidForTrolleyTotalRequest(TrolleyTotalRequest trolleyTotalRequest)
        {
            var anyTooLarge = false;  
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator - Foreach more readable than LINQ in this case
            foreach (var requestedItem in trolleyTotalRequest.RequestedItems)
            {
                if (!Inventory.Any(_ => _.Name.Equals(requestedItem.Name) && (_.Quantity > requestedItem.Quantity)))
                    continue;
                anyTooLarge = true;
                break;
            }
            return !anyTooLarge;
        }
    }
}
