using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
