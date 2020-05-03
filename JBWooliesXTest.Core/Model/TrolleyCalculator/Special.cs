using System;
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
            foreach (var requestedItem in trolleyTotalRequest.RequestedItems)
            {
                if (Inventory.Any(_ => _.Name.Equals(requestedItem.Name) && (_.Quantity > requestedItem.Quantity)))
                {
                    anyTooLarge = true;
                    break;
                }
            }
            return !anyTooLarge;
        }
    }
}
